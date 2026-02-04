Imports System.Drawing.Printing
Imports System.IO
Imports Core.Config
Imports Core.Enums
Imports Core.Logging
Imports Core.Models.Sale
Imports Core.Models.Sale.Invoice

Imports Core.Services
Imports Newtonsoft.Json
Imports QRCoder
Imports Ui.Helpers
Imports Ui.Repo

Public Class Sales
    Private _settingsManager As SettingsManager
    Private _integrator As VSCUIntegrator
    Private _logger As Logger
    Private _conn As String
    ' Receipt rendering / printing state
    Private receiptLines As New List(Of String)()
    Private fullReceiptBitmap As Bitmap = Nothing
    Private currentPrintY As Integer = 0 ' for thermal-style printing (vertical offset)

    Public Sub New(connectionString As String)
        InitializeComponent()
        _conn = connectionString
        ' runtime preview picturebox and configure panel
        CreateReceiptPreviewControl()

        ' Initialize settings manager
        _settingsManager = New SettingsManager(_conn)
        ' Initialize logger
        Dim repo As New LogRepository(_conn)
        _logger = New Logger(repo)
        ' Build IntegratorSettings from DB
        Dim baseUrlTask = _settingsManager.GetSettingAsync("base_url")
        Dim pinTask = _settingsManager.GetSettingAsync("pin")
        Dim branchIdTask = _settingsManager.GetSettingAsync("branch_id")
        Dim deviceSerialTask = _settingsManager.GetSettingAsync("device_serial")
        Dim timeoutTask = _settingsManager.GetSettingAsync("timeout")
        Task.WhenAll(baseUrlTask, pinTask, branchIdTask, deviceSerialTask, timeoutTask).ContinueWith(Sub(t)
                                                                                                         Dim settings As New IntegratorSettings With {
                                                                                                                .BaseUrl = baseUrlTask.Result,
                                                                                                                .Pin = pinTask.Result,
                                                                                                                .BranchId = branchIdTask.Result,
                                                                                                                .DeviceSerial = deviceSerialTask.Result,
                                                                                                                .Timeout = If(Integer.TryParse(timeoutTask.Result, Nothing), CInt(timeoutTask.Result), 30)
                                                                                                             }
                                                                                                         _integrator = New VSCUIntegrator(settings, _logger)
                                                                                                     End Sub)
    End Sub

    Private Sub CreateReceiptPreviewControl()
        picReceiptPreview = New PictureBox() With {
        .Dock = DockStyle.Top,
        .SizeMode = PictureBoxSizeMode.AutoSize,
        .BorderStyle = BorderStyle.None
    }
        pnlReceipt.AutoScroll = True
        pnlReceipt.Controls.Add(picReceiptPreview)
    End Sub
    ' =========================
    ' Send sales and show preview
    ' =========================
    Private Async Sub ButtonSendSales_Click(sender As Object, e As EventArgs) Handles BtnSendSales.Click
        Dim capturedEx As Exception = Nothing
        Try
            Loader.Visible = True
            Loader.Text = "Preparing invoice..."
            BtnSendSales.Enabled = False
            BtnSendSales.Text = "Sending..."
            ' Load settings
            Dim tin = Await _settingsManager.GetSettingAsync("pin")
            Dim bhfId = Await _settingsManager.GetSettingAsync("branch_id")
            Dim qr_base_url = Await _settingsManager.GetSettingAsync("qr_code_base_url")
            ' Load invoice master & details from DB (example uses InvoiceNo textbox)
            Dim invoiceNo As String = txtInvoiceNo.Text.Trim()
            If String.IsNullOrEmpty(invoiceNo) Then
                CustomAlert.ShowAlert(Me, "Enter invoice number first.", "Warning", CustomAlert.AlertType.Warning, CustomAlert.ButtonType.OK)
                Return
            End If
            Dim invoiceRepo = New SalesRepository(_conn)
            Dim master = Await invoiceRepo.LoadMasterAsync(invoiceNo) ' iInvoice object from db
            Dim details = Await invoiceRepo.LoadDetailsAsync(invoiceNo) ' list of rows from dbca
            If master Is Nothing OrElse details Is Nothing OrElse details.Count = 0 Then
                CustomAlert.ShowAlert(Me, "Invoice or details not found.", "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
                Return
            End If
            ' === Initialize sums per tax type ===
            Dim taxblAmtA As Decimal = 0D
            Dim taxblAmtB As Decimal = 0D
            Dim taxblAmtC As Decimal = 0D
            Dim taxblAmtD As Decimal = 0D
            Dim taxblAmtE As Decimal = 0D

            Dim taxAmtA As Decimal = 0D
            Dim taxAmtB As Decimal = 0D
            Dim taxAmtC As Decimal = 0D
            Dim taxAmtD As Decimal = 0D
            Dim taxAmtE As Decimal = 0D

            ' Loop through details and sum per tax type
            For Each d In details
                Select Case d.VATCode.ToUpper() ' assuming VATCode = "A", "B", "C", "D", "E"
                    Case "A"
                        taxblAmtA += d.NetAmount
                        taxAmtA += d.VATAmount
                    Case "B"
                        taxblAmtB += d.NetAmount
                        taxAmtB += d.VATAmount
                    Case "C"
                        taxblAmtC += d.NetAmount
                        taxAmtC += d.VATAmount
                    Case "D"
                        taxblAmtD += d.NetAmount
                        taxAmtD += d.VATAmount
                    Case "E"
                        taxblAmtE += d.NetAmount
                        taxAmtE += d.VATAmount
                End Select
            Next

            ' === Calculate total taxable, total VAT, and total amount ===
            Dim totTaxblAmt As Decimal = taxblAmtA + taxblAmtB + taxblAmtC + taxblAmtD + taxblAmtE
            Dim totTaxAmt As Decimal = taxAmtA + taxAmtB + taxAmtC + taxAmtD + taxAmtE
            Dim totAmt As Decimal = details.Sum(Function(d) d.Amount)

            ' === Optional: calculate rate per type if needed ===
            Dim taxRtA As Decimal = If(taxblAmtA > 0, Math.Round(taxAmtA / taxblAmtA * 100, 2), 0)
            Dim taxRtB As Decimal = If(taxblAmtB > 0, Math.Round(taxAmtB / taxblAmtB * 100, 2), 0)
            Dim taxRtC As Decimal = If(taxblAmtC > 0, Math.Round(taxAmtC / taxblAmtC * 100, 2), 0)
            Dim taxRtD As Decimal = If(taxblAmtD > 0, Math.Round(taxAmtD / taxblAmtD * 100, 2), 0)
            Dim taxRtE As Decimal = If(taxblAmtE > 0, Math.Round(taxAmtE / taxblAmtE * 100, 2), 0)

            Dim salesDate As DateTime = If(master.InvoiceDate.HasValue, master.InvoiceDate.Value, DateTime.Now)

            Dim now As DateTime = DateTime.Now
            Dim cfmDate As DateTime

            ' Difference in days
            Dim diffDays As Integer = CInt((now - salesDate).TotalDays)

            If diffDays > 7 Then
                ' Bring cfmDt closer to salesDt (within 7 days window)
                cfmDate = salesDate.AddDays(7)

                ' Optional: give it a reasonable time (not midnight)
                cfmDate = New DateTime(cfmDate.Year, cfmDate.Month, cfmDate.Day, 10, 0, 0)
            Else
                ' Use real-time confirmation date
                cfmDate = now
            End If

            ' Build SalesRequest payload
            Dim digitsOnly As String = New String(master.InvoiceNo.Where(Function(c) Char.IsDigit(c)).ToArray())
            Dim req As New SalesRequest With {
                .tin = tin,
                .bhfId = bhfId,
                .trdInvcNo = If(String.IsNullOrEmpty(digitsOnly), 0, CLng(digitsOnly)),
                .invcNo = If(String.IsNullOrEmpty(digitsOnly), 0, CLng(digitsOnly)),
                .orgInvcNo = 0,
                .custTin = If(String.IsNullOrEmpty(master.CustomerPinNo), "00000000000", master.CustomerPinNo),
                .custNm = If(String.IsNullOrEmpty(master.CustomerName), "Walk In customer", master.CustomerName),
                .salesTyCd = "N",
                .rcptTyCd = "S",
                .pmtTyCd = "01",
                .salesSttsCd = "02",
                .cfmDt = cfmDate.ToString("yyyyMMddHHmmss"),
                .salesDt = salesDate.ToString("yyyyMMdd"),
                .totItemCnt = details.Count,
                .taxblAmtA = taxblAmtA,
                .taxblAmtB = taxblAmtB,
                .taxblAmtC = taxblAmtC,
                .taxblAmtD = taxblAmtD,
                .taxblAmtE = taxblAmtE,
                .taxRtA = taxRtA,
                .taxRtB = taxRtB,
                .taxRtC = taxRtC,
                .taxRtD = taxRtD,
                .taxRtE = taxRtE,
                .taxAmtA = taxAmtA,
                .taxAmtB = taxAmtB,
                .taxAmtC = taxAmtC,
                .taxAmtD = taxAmtD,
                .taxAmtE = taxAmtE,
                .totTaxblAmt = totTaxblAmt,
                .totTaxAmt = totTaxAmt,
                .totAmt = totAmt,
                .prchrAcptcYn = "N",
                .remark = If(String.IsNullOrEmpty(master.Remark), Nothing, master.Remark),
                .regrId = master.SalesmanCode,
                .regrNm = master.PreparedBy,
                .modrId = master.SalesmanCode,
                .modrNm = master.PreparedBy
            }
            ' Build receipt object
            req.receipt = New With {
                .custTin = If(String.IsNullOrEmpty(master.CustomerPinNo), "00000000000", master.CustomerPinNo),
                .custMblNo = If(String.IsNullOrEmpty(master.CustomerPhone), Nothing, master.CustomerPhone),
                .rptNo = digitsOnly,
                .trdeNm = If(String.IsNullOrEmpty(master.PartyName), "", master.PartyName),
                .adrs = "",
                .topMsg = "Thank you",
                .btmMsg = "Welcome",
                .prchrAcptcYn = "N"
            }
            ' itemList mapping
            req.itemList = New List(Of Object)
            Dim seq As Integer = 1
            Dim pkgValue As Decimal
            For Each d In details

                If d.SupplierPacking > 0 Then
                    pkgValue = d.Qty / d.SupplierPacking
                    ' If fractional, switch to alternate unit
                    If pkgValue < 1D AndAlso Not String.IsNullOrEmpty(d.AlternetUnit) Then
                        pkgValue = d.Qty
                    Else
                        pkgValue = Math.Round(pkgValue, 2)
                    End If
                Else
                    pkgValue = 1D
                End If
                req.itemList.Add(New With {
                    .itemSeq = seq,
                    .itemCd = d.ItemClsCode,
                    .itemClsCd = d.ItemCode,
                    .itemNm = d.Description,
                    .pkgUnitCd = d.Pack,
                    .pkg = pkgValue,
                    .qtyUnitCd = If(String.IsNullOrEmpty(d.QUCODE), d.Pack, d.QUCODE),
                    .qty = If(String.IsNullOrEmpty(d.QUCODE), d.Qty, Math.Round(d.QUAMT * d.Qty, 2)),
                    .prc = d.Price,
                    .splyAmt = d.Amount,
                    .dcRt = d.Disc,
                    .dcAmt = d.DiscAmount,
                    .taxTyCd = d.VATCode,
                    .taxblAmt = d.Amount,
                    .taxAmt = d.VATAmount,
                    .totAmt = d.NetAmount
                })
                seq += 1
            Next
            Loader.Text = "Sending invoice to tax authority..."
            ' Serialize payload
            Dim jsonPayload = JsonConvert.SerializeObject(req, Formatting.None, New JsonSerializerSettings With {
                                                          .NullValueHandling = NullValueHandling.Ignore})
            ' send to VSCU through integrator helper
            Dim resp As SalesResponse = Await _integrator.SendSalesAsync(req)
            ' fallback: call VSCU directly with HttpClient if needed
            If resp Is Nothing OrElse resp.resultCd <> "000" Then
                CustomAlert.ShowAlert(Me, "Error sending invoice: " & If(resp?.resultMsg, "No response"), "Error", CustomAlert.AlertType.Error,
                                      CustomAlert.ButtonType.OK)
                Return
            End If
            ' Persist response in DB (update invoicemaster)
            master.CU_Inv_No = resp.data.rcptNo
            master.CU_Serial_No = resp.data.sdcId
            master.CU_Datetime = ParseVSDCDate(resp.data.vsdcRcptPbctDate)
            'Generate qr data
            Dim qrUrl As String = qr_base_url & tin & bhfId & resp.data.rcptSign
            master.QR_Code = qrUrl
            ' Save updated master
            Await invoiceRepo.UpdateMasterAsync(master)
            ' Build and show the thermal roll preview (and prepare printing state)
            BuildReceiptLines(master, details, resp)
            RenderFullReceiptBitmap(master)
            CustomAlert.ShowAlert(Me, "Invoice sent successfully. Receipt No: " & resp.data.rcptNo.ToString(), "Success",
                                  CustomAlert.AlertType.Success, CustomAlert.ButtonType.OK)
        Catch ex As Exception
            capturedEx = ex
            CustomAlert.ShowAlert(Me, "Error sending invoice: " & ex.Message, "Error", CustomAlert.AlertType.Error, CustomAlert.ButtonType.OK)
        Finally
            Loader.Visible = False
            BtnSendSales.Enabled = True
            BtnSendSales.Text = "Send Invoice"
        End Try
        If capturedEx IsNot Nothing Then
            Dim fullError As String =
                $"Message: {capturedEx.Message}{Environment.NewLine}" &
                $"StackTrace: {capturedEx.StackTrace}{Environment.NewLine}" &
                If(capturedEx.InnerException IsNot Nothing, $"InnerException: {capturedEx.InnerException.Message}{Environment.NewLine}{capturedEx.InnerException.StackTrace}",
                "InnerException: None")
            Await _logger.LogAsync(LogLevel.Error, "Error sending invoice", fullError)
        End If
    End Sub
    ' =========================
    ' Build receipt textual lines (for clarity & optional debugging This is just a sample)
    ' =========================
    Private Sub BuildReceiptLines(master As InvoiceMaster, details As List(Of InvoiceDetail), resp As SalesResponse)
        receiptLines.Clear()
        Dim dt As DateTime = ParseVSDCDate(resp.data.vsdcRcptPbctDate)

        receiptLines.Add("       GATEPARK-KENDUBAY")
        receiptLines.Add("   P.O.BOX : 256-40301, KENDUBAY - KENYA")
        receiptLines.Add("       TEL : 0750-564099")
        receiptLines.Add("       PIN #: A004195331")
        receiptLines.Add("---------------------------------------")
        receiptLines.Add($"PAYBILL NO: 522533    A/C NO: 5754786")
        receiptLines.Add("=======================================")

        receiptLines.Add($"Till No : 2   Cash Sale #: {master.SrNo}")
        receiptLines.Add($"M/S     : {master.PartyName}")
        receiptLines.Add($"Address : ")
        receiptLines.Add($"Tel     : ")
        receiptLines.Add($"PIN     : ")
        receiptLines.Add($"Date & Time : {dt:yyyy-MM-dd HH:mm}")
        receiptLines.Add("---------------------------------------")

        receiptLines.Add("ITEM                 QTY   PRICE   AMOUNT")
        receiptLines.Add("---------------------------------------")
        For Each d In details
            Dim itemName = If(d.Description.Length > 18, d.Description.Substring(0, 18), d.Description.PadRight(18))
            Dim qty = d.Qty.ToString().PadLeft(3)
            Dim price = d.Price.ToString("F2").PadLeft(6)
            Dim amt = d.Amount.ToString("F2").PadLeft(7)
            receiptLines.Add($"{itemName} {qty} {price} {amt}")
        Next
        receiptLines.Add("---------------------------------------")

        receiptLines.Add($"TOTAL    : {master.TotalAmount:N2}")
        receiptLines.Add($"DISCOUNT : {master.Discount:N2}")
        receiptLines.Add($"CASH     : {master.TotalAmount:N2}")
        receiptLines.Add($"CHANGE   : {0.00:N2}")
        receiptLines.Add("---------------------------------------")
        receiptLines.Add($"TOTAL ITEMS : {details.Count}")
        receiptLines.Add($"TOTAL QTY   : {details.Sum(Function(x) x.Qty)}")
        receiptLines.Add($"TOTAL WEIGHT: {master.TotatWeight:N2}")
        receiptLines.Add("---------------------------------------")

        receiptLines.Add("CODE   VATABLE AMT   VAT AMT   TOTAL")
        receiptLines.Add($"A      {master.VATAmount:N2}        {master.VATAmount:N2}    {master.TotalAmount:N2}")
        receiptLines.Add($"E      0.00           0.00       0.00")
        receiptLines.Add($"Z      0.00           0.00       0.00")
        receiptLines.Add("VAT CODE: (A)=VATABLE, (E)=EXEMPT, (Z)=ZERO RATED")
        receiptLines.Add("PRICES INCLUSIVE OF VAT WHERE APPLICABLE")
        receiptLines.Add("---------------------------------------")

        receiptLines.Add($"YOU WERE SERVED BY : {master.PreparedBy}")
        receiptLines.Add("GOODS ONCE SOLD CANNOT BE ACCEPTED BACK FOR REFUND OR ANY OTHER REASON")
        receiptLines.Add("---------------------------------------")
        receiptLines.Add("Controller Unit Info")
        receiptLines.Add($"CU SN: {resp.data.sdcId}")
        receiptLines.Add($"CU INV: {resp.data.rcptNo}")
        receiptLines.Add("---------------------------------------")
        receiptLines.Add("Thank You --- Come Again")
        receiptLines.Add($"{dt:yyyy-MM-dd HH:mm}")
    End Sub

    ' =========================
    ' Render the full receipt into one tall bitmap (thermal roll)
    ' QR code is drawn next to "CU SN" line in Controller Unit Info section
    ' =========================
    Private Sub RenderFullReceiptBitmap(master As InvoiceMaster)
        ' defensive
        If receiptLines Is Nothing OrElse receiptLines.Count = 0 Then
            Return
        End If
        ' configuration
        Dim font As New Font("Consolas", 10)
        Dim lineHeight As Integer = CInt(Math.Ceiling(font.GetHeight()) + 6) ' spacing
        Dim paddingTop As Integer = 10
        Dim paddingLeft As Integer = 10

        ' width: based on panel width (allow some margin)
        Dim width As Integer = Math.Max(300, pnlReceipt.ClientSize.Width - 40)

        ' compute necessary height dynamically based on actual measured text
        Dim totalLinesHeight As Integer = 0
        Using tempBmp As New Bitmap(1, 1)
            Using g As Graphics = Graphics.FromImage(tempBmp)
                For Each line In receiptLines
                    totalLinesHeight += CInt(g.MeasureString(line, font).Height) + 6
                Next
            End Using
        End Using

        Dim qrExtra As Integer = If(String.IsNullOrEmpty(master.QR_Code), 0, 120) ' extra space for QR
        Dim estimatedHeight As Integer = paddingTop + totalLinesHeight + qrExtra + 50

        ' cleanup previous bitmap
        If fullReceiptBitmap IsNot Nothing Then
            fullReceiptBitmap.Dispose()
            fullReceiptBitmap = Nothing
        End If

        fullReceiptBitmap = New Bitmap(width, estimatedHeight)
        Using g As Graphics = Graphics.FromImage(fullReceiptBitmap)
            g.Clear(Color.White)
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit

            Dim y As Integer = paddingTop

            ' create brush for text
            Using brush As New SolidBrush(Color.Black)
                ' prepare QR bitmap if available
                Dim qrBmp As Bitmap = Nothing
                Dim qrSize As Integer = 100 ' adjust as needed
                If Not String.IsNullOrEmpty(master.QR_Code) Then
                    Try
                        Dim generator As New QRCodeGenerator()
                        Dim data = generator.CreateQrCode(master.QR_Code, QRCodeGenerator.ECCLevel.Q)
                        Dim qrCode As New QRCode(data)
                        qrBmp = qrCode.GetGraphic(20) ' adjust pixel density
                    Catch
                        ' ignore qr errors
                    End Try
                End If

                ' draw receipt lines
                For i As Integer = 0 To receiptLines.Count - 1
                    Dim line As String = receiptLines(i)

                    ' detect CU SN line to draw QR inline
                    If line.StartsWith("CU SN:") AndAlso qrBmp IsNot Nothing Then
                        ' draw CU SN text
                        g.DrawString(line, font, brush, paddingLeft, y)
                        ' draw QR code next to text
                        Dim qrX As Integer = paddingLeft + 200 ' adjust horizontal offset
                        Dim qrY As Integer = y - 5 ' slight vertical alignment tweak
                        g.DrawImage(qrBmp, qrX, qrY, qrSize, qrSize)
                        ' advance Y by the taller of lineHeight or QR height
                        y += Math.Max(lineHeight, qrSize)
                    Else
                        ' normal text
                        g.DrawString(line, font, brush, paddingLeft, y)
                        y += lineHeight
                    End If
                Next
            End Using
        End Using

        ' assign to picturebox for scrollable preview
        picReceiptPreview.Image = fullReceiptBitmap
        picReceiptPreview.Width = fullReceiptBitmap.Width
        picReceiptPreview.Height = fullReceiptBitmap.Height

        ' ensure panel's scrollbars appear
        pnlReceipt.AutoScroll = True

    End Sub

    ' =========================
    ' Print preview & printing - thermal roll style
    ' PrintDocument will print vertical slices of fullReceiptBitmap until exhausted.
    ' =========================
    Private Sub btnPrintPreview_Click(sender As Object, e As EventArgs) Handles BtnPrintPreview.Click
        If fullReceiptBitmap Is Nothing Then
            MessageBox.Show("No receipt to preview. Send invoice first.", "Print Preview", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        currentPrintY = 0
        printPreviewDlg.Document = printDoc
        printPreviewDlg.Width = 900
        printPreviewDlg.Height = 700
        Try
            printPreviewDlg.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Could not show print preview: " & ex.Message)
        End Try
    End Sub

    ' Direct print button (optional) - will show PrintDialog then print
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        ' If you add a real print button in the designer, hook it to this handler.
        If fullReceiptBitmap Is Nothing Then
            MessageBox.Show("No receipt to print. Send invoice first.")
            Return
        End If
        Dim dlg As New PrintDialog()
        dlg.Document = printDoc
        If dlg.ShowDialog() = DialogResult.OK Then
            currentPrintY = 0
            printDoc.Print()
        End If
    End Sub
    Private Sub printDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDoc.PrintPage
        If fullReceiptBitmap Is Nothing Then
            e.HasMorePages = False
            Return
        End If
        ' determine printable area
        Dim pageWidth As Integer = e.MarginBounds.Width
        Dim pageHeight As Integer = e.MarginBounds.Height
        ' scale horizontally to fit page width while keeping aspect ratio
        Dim scale As Single = pageWidth / CSng(fullReceiptBitmap.Width)
        If scale <= 0 Then scale = 1.0F
        Dim sliceHeightSource As Integer = CInt(Math.Ceiling(pageHeight / scale)) ' source slice height in bitmap coordinates
        ' ensure we don't overflow the source bitmap
        If currentPrintY + sliceHeightSource > fullReceiptBitmap.Height Then
            sliceHeightSource = fullReceiptBitmap.Height - currentPrintY
        End If
        ' draw the slice
        Using sliceBmp As New Bitmap(fullReceiptBitmap.Width, sliceHeightSource)
            Using gSlice As Graphics = Graphics.FromImage(sliceBmp)
                gSlice.DrawImage(fullReceiptBitmap, New Rectangle(0, 0, sliceBmp.Width, sliceBmp.Height),
                                 New Rectangle(0, currentPrintY, sliceBmp.Width, sliceHeightSource),
                                 GraphicsUnit.Pixel)
            End Using
            ' Draw scaled slice onto page
            e.Graphics.DrawImage(sliceBmp, e.MarginBounds.Left, e.MarginBounds.Top, pageWidth, CInt(sliceHeightSource * scale))
        End Using
        ' advance
        currentPrintY += sliceHeightSource
        ' more pages?
        If currentPrintY < fullReceiptBitmap.Height Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
            currentPrintY = 0 ' reset for next print job
        End If
    End Sub

    ' =========================
    ' Utilities
    ' =========================
    Private Function GenerateQrBase64(content As String) As String
        If String.IsNullOrEmpty(content) Then Return Nothing
        Using qrGen As New QRCodeGenerator()
            Dim data = qrGen.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q)
            Using qrCode = New QRCode(data)
                Using bmp = qrCode.GetGraphic(20) ' 20 pixels per module
                    Using ms As New MemoryStream()
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                        Dim bytes = ms.ToArray()
                        Return Convert.ToBase64String(bytes)
                    End Using
                End Using
            End Using
        End Using
    End Function

    Private Function ParseVSDCDate(value As String) As Date?
        If String.IsNullOrWhiteSpace(value) OrElse value.Length <> 14 Then
            Return Nothing
        End If
        Dim result As DateTime
        If DateTime.TryParseExact(value, "yyyyMMddHHmmss", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None,
                              result) Then
            Return result
        End If
        Return Nothing
    End Function
End Class