Imports Core.Models.Sale.Invoice
Imports MySql.Data.MySqlClient

Namespace Repo.ProductRepo

    Public Class SalesRepository
        Private ReadOnly _connString As String

        Public Sub New(connString As String)
            _connString = connString
        End Sub

        ' ===========================================================
        '  LOAD INVOICE MASTER
        ' ===========================================================
        Public Async Function LoadMasterAsync(invoiceNo As String) As Task(Of InvoiceMaster)
            Using conn As New MySqlConnection(_connString)
                Await conn.OpenAsync()

                Dim sql As String = "SELECT InvoiceNo, InvoiceDate, DeliveryNoteNo, LPONo, PartyName, salesmanmaster.SalesmanCode,
                                        salesmanmaster.SalesmanName, Pricelist, Remark, SubTotal, VATAmount, Discount, TotalAmount, TotalWeight,
                                        PreparedBy, AuthorizedBy, QR_Code, CU_Inv_No, CU_Serial_No, CU_Datetime, invoicemaster.SrNo,
                                        customermaster.CustomerCode, customermaster.CustomerName, customermaster.PinNo As CustomerPinNo,
                                        customermaster.VATNo As CustomerVATNo, customermaster.Address1 As CustomerAddress1, customermaster.City
                                        As CustomerCity, customermaster.Country As CustomerCountry, customermaster.Phone As CustomerPhone FROM
                                        invoicemaster INNER JOIN salesmanmaster ON invoicemaster.SalesmanCode = salesmanmaster.SalesmanCode
                                        INNER JOIN customermaster ON invoicemaster.CustomerCode = customermaster.CustomerCode WHERE
                                        InvoiceNo = @invoiceNo LIMIT 1"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@invoiceNo", invoiceNo)

                    Using r = Await cmd.ExecuteReaderAsync()
                        If Await r.ReadAsync() Then

                            Dim m As New InvoiceMaster With {
                                .InvoiceNo = r("InvoiceNo").ToString(),
                                .InvoiceDate = Convert.ToDateTime(r("InvoiceDate")),
                                .DeliveryNoteNo = r("DeliveryNoteNo").ToString(),
                                .LPONo = r("LPONo").ToString(),
                                .CustomerCode = r("CustomerCode").ToString(),
                                .CustomerPinNo = r("CustomerPinNo").ToString(),
                                .CustomerName = r("CustomerName").ToString(),
                                .CustomerVATNo = r("CustomerVATNo").ToString(),
                                .CustomerAddress1 = r("CustomerAddress1").ToString(),
                                .CustomerCity = r("CustomerCity").ToString(),
                                .CustomerCountry = r("CustomerCountry").ToString(),
                                .CustomerPhone = r("CustomerPhone").ToString(),
                                .PartyName = r("PartyName").ToString(),
                                .SalesmanCode = r("SalesmanCode").ToString(),
                                .SalesmanName = r("SalesmanName").ToString(),
                                .Pricelist = r("Pricelist").ToString(),
                                .Remark = r("Remark").ToString(),
                                .SubTotal = Convert.ToDecimal(r("SubTotal")),
                                .VATAmount = Convert.ToDecimal(r("VATAmount")),
                                .Discount = Convert.ToDecimal(r("Discount")),
                                .TotalAmount = Convert.ToDecimal(r("TotalAmount")),
                                .TotatWeight = Convert.ToDecimal(r("TotalWeight")),
                                .PreparedBy = r("PreparedBy").ToString(),
                                .AuthorizedBy = r("AuthorizedBy").ToString(),
                                .QR_Code = r("QR_Code").ToString(),
                                .CU_Inv_No = r("CU_Inv_No").ToString(),
                                .CU_Serial_No = r("CU_Serial_No").ToString(),
                                .CU_Datetime = If(IsDBNull(r("CU_Datetime")), Nothing, r("CU_Datetime")),
                                .SrNo = r("SrNo").ToString()
                            }
                            Return m
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function


        ' ===========================================================
        '  LOAD INVOICE DETAILS
        ' ===========================================================
        Public Async Function LoadDetailsAsync(invoiceNo As String) As Task(Of List(Of InvoiceDetail))
            Dim list As New List(Of InvoiceDetail)

            Using conn As New MySqlConnection(_connString)
                Await conn.OpenAsync()

                Dim sql As String = "SELECT productmaster.ProductName, productmaster.ProductUnit,productmaster.isAlternetUnit,
                                    productmaster.AlternetUnit, productmaster.SupplierPacking, invoicedetails.* FROM invoicedetails 
                                    INNER JOIN productmaster ON invoicedetails.ProductCode = productmaster.ProductCode
                                    WHERE invoicedetails.InvoiceNo = @invoiceNo ORDER BY SrNo ASC"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@invoiceNo", invoiceNo)

                    Using r = Await cmd.ExecuteReaderAsync()
                        While Await r.ReadAsync()
                            list.Add(New InvoiceDetail With {
                            .InvoiceNo = r("InvoiceNo").ToString(),
                            .SrNo = Convert.ToInt32(r("SrNo")),
                            .ProductCode = r("ProductCode").ToString(),
                            .ItemCode = r("ItemCode").ToString(),
                            .Description = r("ProductName").ToString(),
                            .Pack = r("Pack").ToString(),
                            .QUCODE = r("QUCODE").ToString(),
                            .QUAMT = If(IsDBNull(r("QUAMT")), 0D, Convert.ToDecimal(r("QUAMT"))),
                            .SupplierPacking = Convert.ToDecimal(r("SupplierPacking")),
                            .ProductUnit = r("ProductUnit").ToString(),
                            .isAlternetUnit = r("isAlternetUnit"),
                            .AlternetUnit = r("AlternetUnit").ToString(),
                            .Qty = Convert.ToDecimal(r("Qty")),
                            .Price = Convert.ToDecimal(r("Price")),
                            .Amount = Convert.ToDecimal(r("Amount")),
                            .Disc = Convert.ToDecimal(r("Disc")),
                            .DiscAmount = Convert.ToDecimal(r("DiscAmount")),
                            .VATCode = r("VATCode").ToString(),
                            .VATAmount = Convert.ToDecimal(r("VATAmount")),
                            .NetAmount = Convert.ToDecimal(r("NetAmount")),
                            .ItemClsCode = r("ProductCode").ToString()
                        })
                        End While
                    End Using
                End Using
            End Using

            Return list
        End Function

        ' ===========================================================
        '  UPDATE MASTER WITH VSCU RESPONSE
        ' ===========================================================
        Public Async Function UpdateMasterAsync(master As InvoiceMaster) As Task
            Using conn As New MySqlConnection(_connString)
                Await conn.OpenAsync()

                Dim sql As String =
                "UPDATE invoicemaster SET " &
                "QR_Code = @qr, " &
                "CU_Inv_No = @cuInvNo, " &
                "CU_Serial_No = @cuSerialNo, " &
                "CU_Datetime = @cuDt " &
                "WHERE InvoiceNo = @invoiceNo"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@invoiceNo", master.InvoiceNo)

                    cmd.Parameters.AddWithValue("@qr", master.QR_Code)
                    cmd.Parameters.AddWithValue("@cuInvNo", master.CU_Inv_No)
                    cmd.Parameters.AddWithValue("@cuSerialNo", master.CU_Serial_No)

                    If master.CU_Datetime.HasValue Then
                        cmd.Parameters.AddWithValue("@cuDt", master.CU_Datetime.Value)
                    Else
                        cmd.Parameters.AddWithValue("@cuDt", DBNull.Value)
                    End If

                    Await cmd.ExecuteNonQueryAsync()
                End Using
            End Using
        End Function

    End Class
End Namespace