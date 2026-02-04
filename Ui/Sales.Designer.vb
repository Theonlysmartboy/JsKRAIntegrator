<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Sales
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.BtnSendSales = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.BtnPrintPreview = New System.Windows.Forms.Button()
        Me.pnlReceipt = New System.Windows.Forms.Panel()
        Me.picReceiptPreview = New System.Windows.Forms.PictureBox()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.Loader = New JsToolBox.Loaders.DualRingLoader()
        Me.pnlReceipt.SuspendLayout()
        CType(Me.picReceiptPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnSendSales
        '
        Me.BtnSendSales.Location = New System.Drawing.Point(420, 12)
        Me.BtnSendSales.Name = "BtnSendSales"
        Me.BtnSendSales.Size = New System.Drawing.Size(75, 23)
        Me.BtnSendSales.TabIndex = 0
        Me.BtnSendSales.Text = "Send"
        Me.BtnSendSales.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Invoice Number:"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(103, 12)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(262, 20)
        Me.txtInvoiceNo.TabIndex = 2
        '
        'BtnPrintPreview
        '
        Me.BtnPrintPreview.Location = New System.Drawing.Point(515, 12)
        Me.BtnPrintPreview.Name = "BtnPrintPreview"
        Me.BtnPrintPreview.Size = New System.Drawing.Size(120, 23)
        Me.BtnPrintPreview.TabIndex = 0
        Me.BtnPrintPreview.Text = "Preview Print"
        '
        'pnlReceipt
        '
        Me.pnlReceipt.AutoSize = True
        Me.pnlReceipt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlReceipt.Controls.Add(Me.picReceiptPreview)
        Me.pnlReceipt.Location = New System.Drawing.Point(15, 50)
        Me.pnlReceipt.Name = "pnlReceipt"
        Me.pnlReceipt.Size = New System.Drawing.Size(760, 520)
        Me.pnlReceipt.TabIndex = 1
        '
        'picReceiptPreview
        '
        Me.picReceiptPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picReceiptPreview.Dock = System.Windows.Forms.DockStyle.Top
        Me.picReceiptPreview.Location = New System.Drawing.Point(0, 0)
        Me.picReceiptPreview.Name = "picReceiptPreview"
        Me.picReceiptPreview.Size = New System.Drawing.Size(758, 505)
        Me.picReceiptPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picReceiptPreview.TabIndex = 1
        Me.picReceiptPreview.TabStop = False
        '
        'BtnPrint
        '
        Me.BtnPrint.Location = New System.Drawing.Point(641, 12)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(120, 23)
        Me.BtnPrint.TabIndex = 3
        Me.BtnPrint.Text = "Print"
        '
        'Loader
        '
        Me.Loader.ArcLength = 220
        Me.Loader.InnerRingColor = System.Drawing.Color.Turquoise
        Me.Loader.LoaderColor = System.Drawing.Color.DodgerBlue
        Me.Loader.Location = New System.Drawing.Point(363, 254)
        Me.Loader.Name = "Loader"
        Me.Loader.OuterRingColor = System.Drawing.Color.SkyBlue
        Me.Loader.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Loader.RingThickness = 4
        Me.Loader.Size = New System.Drawing.Size(75, 75)
        Me.Loader.Speed = 100
        Me.Loader.TabIndex = 7
        Me.Loader.Text = "Loading"
        Me.Loader.Visible = False
        '
        'Sales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 582)
        Me.Controls.Add(Me.Loader)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.BtnPrintPreview)
        Me.Controls.Add(Me.pnlReceipt)
        Me.Controls.Add(Me.txtInvoiceNo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnSendSales)
        Me.Name = "Sales"
        Me.Text = "Sales"
        Me.pnlReceipt.ResumeLayout(False)
        Me.pnlReceipt.PerformLayout()
        CType(Me.picReceiptPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSendSales As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtInvoiceNo As TextBox
    Friend WithEvents pnlReceipt As Panel
    Friend WithEvents BtnPrintPreview As Button
    Private WithEvents printDoc As New Printing.PrintDocument()
    Private printPreviewDlg As New PrintPreviewDialog()
    Friend WithEvents BtnPrint As Button
    Friend WithEvents picReceiptPreview As PictureBox
    Friend WithEvents Loader As JsToolBox.Loaders.DualRingLoader
End Class