Imports Core.Models.Item.Product
Imports MySql.Data.MySqlClient
Namespace Repo
    Public Class ProductRepository
        Private _connString As String

        Public Sub New(connString As String)
            _connString = connString
        End Sub

        Public Function GetAllProducts() As List(Of Product)
            Dim products As New List(Of Product)()
            Using conn As New MySqlConnection(_connString)
                conn.Open()
                Dim sql As String = "SELECT ProductCode, ItemCode, ProductName, Product_Cost_Price, HSCode, ItemTyCd, ItemStdNm, OrgNatCd," &
                                    "SupplierPacking, ProductUnit, SupplyUnit, Product_VAT_Code, TagPrice, Product_Selling_Price," &
                                    "Product_Wholelsale_Price, Product_Custom_Price1, Product_Custom_Price2, ReOrd_Level, IsrcAplcbYn," &
                                    "isActive, CreatedBy, ModifiedBy FROM productmaster WHERE isActive = 1"
                Using cmd As New MySqlCommand(sql, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            products.Add(New Product With {
                                .ItemCd = reader("ProductCode").ToString(),
                                .ItemClsCd = reader("ItemCode").ToString(),
                                .ItemNm = reader("ProductName").ToString(),
                                .DftPrc = Convert.ToDecimal(reader("Product_Cost_Price")),
                                .HSCode = If(IsDBNull(reader("HSCode")), Nothing, reader("HSCode").ToString()),
                                .ItemTyCd = reader("ItemTyCd").ToString(),
                                .ItemStdNm = reader("ItemStdNm").ToString(),
                                .OrgNatCd = reader("OrgNatCd").ToString(),
                                .SupplierPackageUnit = Convert.ToDecimal(reader("SupplierPacking")),
                                .PackageUnit = If(IsDBNull(reader("SupplyUnit")), reader("ProductUnit").ToString(), reader("SupplyUnit").ToString()),
                                .QuantityUnit = reader("ProductUnit").ToString(),
                                .TaxTyCd = reader("Product_VAT_Code").ToString(),
                                .DefaultPrice = Convert.ToDecimal(reader("Product_Selling_Price")),
                                .GroupPrice1 = If(IsDBNull(reader("Product_Wholelsale_Price")), 0D, Convert.ToDecimal(reader("Product_Wholelsale_Price"))),
                                .GroupPrice2 = If(IsDBNull(reader("Product_Custom_Price1")), 0D, Convert.ToDecimal(reader("Product_Custom_Price1"))),
                                .GroupPrice3 = If(IsDBNull(reader("Product_Custom_Price2")), 0D, Convert.ToDecimal(reader("Product_Custom_Price2"))),
                                .IsrcAplcbYn = reader("IsrcAplcbYn").ToString(),
                                .SafetyQty = If(IsDBNull(reader("ReOrd_Level")), 0D, Convert.ToDecimal(reader("ReOrd_Level"))),
                                .IsActive = reader("isActive").ToString(),
                                .CreatedBy = reader("CreatedBy").ToString(),
                                .ModifiedBy = reader("ModifiedBy").ToString()
                            })
                        End While
                    End Using
                End Using
            End Using
            Return products
        End Function

        Public Function GetProductByProductCode(itemCd As String) As Product
            Dim product As Product = Nothing

            Using conn As New MySqlConnection(_connString)
                conn.Open()

                Dim sql As String = "SELECT ProductCode, ItemCode, ProductName, Product_Cost_Price, HSCode, ItemTyCd, ItemStdNm, OrgNatCd," &
                                    "SupplierPacking, ProductUnit, SupplyUnit, Product_VAT_Code, TagPrice, Product_Selling_Price," &
                                    "Product_Wholelsale_Price, Product_Custom_Price1, Product_Custom_Price2, ReOrd_Level, isActive, IsrcAplcbYn," &
                                    " CreatedBy, ModifiedBy FROM testproductmaster WHERE ProductCode = @itemCd LIMIT 1"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@itemCd", itemCd)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            product = New Product With {
                            .ItemCd = reader("ProductCode").ToString(),
                            .ItemClsCd = reader("ItemCode").ToString(),
                            .ItemNm = reader("ProductName").ToString(),
                            .DftPrc = Convert.ToDecimal(reader("Product_Cost_Price")),
                            .HSCode = If(IsDBNull(reader("HSCode")), Nothing, reader("HSCode").ToString()),
                            .SupplierPackageUnit = Convert.ToDecimal(reader("SupplierPacking")),
                            .PackageUnit = If(IsDBNull(reader("SupplyUnit")), reader("ProductUnit"), reader("SupplyUnit")),
                            .QuantityUnit = reader("ProductUnit").ToString(),
                            .TaxTyCd = reader("Product_VAT_Code").ToString(),
                            .DefaultPrice = Convert.ToDecimal(reader("Product_Selling_Price")),
                            .GroupPrice1 = If(IsDBNull(reader("Product_Wholelsale_Price")), 0D, Convert.ToDecimal(reader("Product_Wholelsale_Price"))),
                            .GroupPrice2 = If(IsDBNull(reader("Product_Custom_Price1")), 0D, Convert.ToDecimal(reader("Product_Custom_Price1"))),
                            .GroupPrice3 = If(IsDBNull(reader("Product_Custom_Price2")), 0D, Convert.ToDecimal(reader("Product_Custom_Price2"))),
                            .SafetyQty = If(IsDBNull(reader("ReOrd_Level")), 0D,
                                            Convert.ToDecimal(reader("ReOrd_Level"))),
                            .IsActive = reader("isActive").ToString(),
                            .CreatedBy = reader("CreatedBy").ToString(),
                            .ModifiedBy = reader("ModifiedBy").ToString()
                        }
                        End If
                    End Using
                End Using
            End Using

            Return product
        End Function

    End Class
End Namespace
