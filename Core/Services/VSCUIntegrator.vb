
Imports System.Net.Http
Imports System.Text
Imports Core.Config
Imports Core.Enums
Imports Core.Logging
Imports Core.Main
Imports Core.Models.Branch
Imports Core.Models.Branch.Customer
Imports Core.Models.Branch.User
Imports Core.Models.Code
Imports Core.Models.Import
Imports Core.Models.Init
Imports Core.Models.Item.Classification
Imports Core.Models.Item.Info
Imports Core.Models.Item.Product
Imports Core.Models.Item.Stock
Imports Core.Models.Notice
Imports Core.Models.Purchase
Imports Core.Models.Sale
Imports Core.Utils
Imports Newtonsoft.Json

Namespace Services
    Public Class VSCUIntegrator
        Private ReadOnly _client As ApiClient
        Private ReadOnly _logger As Logger

        Public Sub New(settings As IntegratorSettings, logger As Logger)
            If settings Is Nothing Then Throw New ArgumentNullException(NameOf(settings))
            If logger Is Nothing Then Throw New ArgumentNullException(NameOf(logger))
            _logger = logger
            _client = New ApiClient(If(String.IsNullOrEmpty(settings.BaseUrl), "http://localhost:8088", settings.BaseUrl), settings.Timeout)
        End Sub

        ' Generic helper: send request (GET/POST) and deserialize into T.
        ' Returns Nothing on failure (caller will produce fallback).
        Private Async Function SendAndDeserializeAsync(Of T As Class)(endpoint As String, Optional payload As Object = Nothing, Optional isGet As Boolean = False) As Task(Of T)

            Dim fullUrl = _client.FullUrl(endpoint)
            Dim jsonPayload As String = If(payload IsNot Nothing, JsonUtil.ToJson(payload), Nothing)

            Dim raw As String = Nothing
            Dim caughtEx As Exception = Nothing
            Dim deserializeEx As Exception = Nothing

            ' 1) Try sending
            Try
                Await _logger.LogAsync(LogLevel.Info, $"Calling {fullUrl}", jsonPayload)

                If isGet Then
                    raw = Await _client.GetAsync(endpoint)
                Else
                    raw = Await _client.SendAsync(endpoint, jsonPayload)
                End If

            Catch ex As Exception
                caughtEx = ex
            End Try

            ' 2) Handle send exception OUTSIDE catch
            If caughtEx IsNot Nothing Then
                Await _logger.LogAsync(
            LogLevel.Error,
            $"Exception calling {fullUrl}: {caughtEx.Message}",
            caughtEx.ToString() &
            If(String.IsNullOrEmpty(jsonPayload), "", vbCrLf & "Payload: " & jsonPayload)
        )
                Return Nothing
            End If

            ' 3) Log successful response
            Await _logger.LogAsync(LogLevel.Info, $"Response from {fullUrl}", raw)

            ' 4) Try deserializing
            Dim result As T = Nothing
            Try
                result = JsonUtil.FromJson(Of T)(raw)

            Catch ex As Exception
                deserializeEx = ex
            End Try

            ' 5) Handle deserialize exception OUTSIDE catch
            If deserializeEx IsNot Nothing Then
                Await _logger.LogAsync(
            LogLevel.Error,
            $"Deserialization error from {fullUrl}: {deserializeEx.Message}",
            raw
        )
                Return Nothing
            End If

            Return result
        End Function

        ' Helper to build a standard fallback BaseResponse-derived object with resultCd/resultMsg/resultDt
        Private Function MakeBaseFallback(Of T As {New})(msg As String) As T
            Dim inst As New T()

            Try
                Dim typ = inst.GetType()
                Dim propCd = typ.GetProperty("resultCd")
                Dim propMsg = typ.GetProperty("resultMsg")
                Dim propDt = typ.GetProperty("resultDt")

                If propCd IsNot Nothing Then propCd.SetValue(inst, "500")
                If propMsg IsNot Nothing Then propMsg.SetValue(inst, msg)
                If propDt IsNot Nothing Then propDt.SetValue(inst, DateTime.Now.ToString("yyyyMMddHHmmss"))

            Catch
                ' ignore reflection errors
            End Try

            Return inst
        End Function

        ' -----------------------
        ' 1) Device Initialization (POST)
        ' -----------------------
        Public Async Function InitializeAsync(req As InitInfoRequest) As Task(Of InitInfoResponse)
            Dim endpoint = ApiEndpoints.SELECT_INIT

            Dim resp = Await SendAndDeserializeAsync(Of InitInfoResponse)(endpoint, req, isGet:=False)
            If resp IsNot Nothing Then
                Return resp
            End If

            ' fallback structured response when the call failed
            Dim fallback = MakeBaseFallback(Of InitInfoResponse)("VSCU error: failed to call Initialize")
            fallback.resultCd = "ERROR"  ' or -1
            fallback.resultMsg = "VSCU error: failed to call Initialize"
            fallback.data = Nothing
            Return fallback
        End Function

        ' -----------------------
        ' 2) Code Data (POST)
        ' -----------------------
        Public Async Function GetCodeDataAsync(request As CodeDataRequest) As Task(Of CodeDataResponse)
            Dim endpoint = ApiEndpoints.CODE_DATA

            ' POST request with payload
            Dim resp = Await SendAndDeserializeAsync(Of CodeDataResponse)(endpoint, request, isGet:=False)

            If resp IsNot Nothing Then
                Return resp
            End If

            ' Build fallback response
            Dim fallback As New CodeDataResponse With {
                .resultCd = "Error",
                .resultMsg = "VSCU error: failed to call CodeData",
                .data = New CodeDataRoot With {
                    .clsList = New List(Of CodeClass)()
                }
            }
            Return fallback
        End Function

        ' -----------------------
        ' 3) Branch Information (POST)
        ' -----------------------
        Public Async Function GetBranchListAsync(req As BranchListRequest) As Task(Of BranchListResponse)
            Dim endpoint = ApiEndpoints.BRANCH_LIST
            Dim resp = Await SendAndDeserializeAsync(Of BranchListResponse)(endpoint, req, isGet:=False)
            If resp IsNot Nothing Then
                Return resp
            End If
            ' Fallback
            Dim fallback As New BranchListResponse With {
                    .resultCd = "Error",
                    .resultMsg = "VSCU error: failed to call BranchList",
                    .resultDt = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    .data = New BranchListData With {
                    .bhfList = New List(Of BranchInfo)()
                }
            }
            Return fallback
        End Function

        ' -----------------------
        ' 4) Customer List (POST)
        ' -----------------------
        Public Async Function GetCustomerListAsync(req As CustomerRequest) As Task(Of CustomerResponse)
            Dim endpoint = ApiEndpoints.CUSTOMER_LIST
            Dim resp = Await SendAndDeserializeAsync(Of CustomerResponse)(endpoint, req, isGet:=False)
            If resp IsNot Nothing Then Return resp
            ' Fallback if API call fails
            Dim fallback As New CustomerResponse With {
                    .resultCd = "Error",
                    .resultMsg = "VSCU error: failed to call CustomerList",
                    .resultDt = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    .data = New CustomerData With {
                    .custList = New List(Of CustomerInfo)()
                }
            }
            Return fallback
        End Function

        '--------------------------
        ' 4b) Branch-Customer Save (POST)
        '--------------------------
        Public Async Function SaveBranchCustomerAsync(req As SaveBranchCustomerRequest) As Task(Of SaveBranchCustomerResponse)
            Dim endpoint = ApiEndpoints.SAVE_BRANCH_CUSTOMER
            Dim resp = Await SendAndDeserializeAsync(Of SaveBranchCustomerResponse)(
                    endpoint,
                    req,
                    isGet:=False)
            If resp IsNot Nothing Then Return resp
            ' Fallback if API call fails
            Dim fallback As New SaveBranchCustomerResponse With {
                .resultCd = "Error",
                .resultMsg = "VSCU error: failed to call SaveBranchCustomer",
                .resultDt = DateTime.Now.ToString("yyyyMMddHHmmss"),
                .data = Nothing
            }
            Return fallback
        End Function

        '-----------------------
        'Branch User Save (POST)
        '-----------------------
        Public Async Function SaveBranchUserAsync(req As BranchUserSaveRequest) As Task(Of BranchUserSaveResponse)
            Dim endpoint = ApiEndpoints.BRANCH_USER_SAVE
            Dim resp = Await SendAndDeserializeAsync(Of BranchUserSaveResponse)(endpoint, req, isGet:=False)
            If resp IsNot Nothing Then
                Return resp
            End If
            ' Fallback if API call fails
            Dim fallback As New BranchUserSaveResponse With {
                .resultCd = "Error",
                .resultMsg = "VSCU error: failed to call SaveBranchUser",
                .resultDt = DateTime.Now.ToString("yyyyMMddHHmmss"),
                .data = Nothing
            }
            Return fallback
        End Function

        ' -----------------------
        ' 5) Item Classification Information (POST)
        ' -----------------------
        Public Async Function SendItemClassificationInfoAsync(req As ItemClassificationRequest) As Task(Of ItemClassificationResponse)
            Dim endpoint = ApiEndpoints.ITEM_CLASSIFICATION_SELECTOR
            Dim resp = Await SendAndDeserializeAsync(Of ItemClassificationResponse)(endpoint, req, isGet:=False)
            If resp IsNot Nothing Then Return resp

            ' Fallback if API call fails
            Dim fallback As New ItemClassificationResponse
            fallback.resultCd = "Error"
            fallback.resultMsg = "VSCU error: failed to call ItemClassificationSelector"
            fallback.data = New ItemClassificationData() With {
                            .itemClsList = New List(Of ItemCls)()
                            }
            Return fallback
        End Function

        '------------------------
        ' 5b) Item  Information (POST)
        '------------------------
        Public Async Function GetItemAsync(query As ItemInfoRequest) As Task(Of ItemInfoResponse)
            Dim endpoint = ApiEndpoints.ITEM_SELECT
            Dim resp = Await SendAndDeserializeAsync(Of ItemInfoResponse)(endpoint, query, isGet:=False)
            If resp IsNot Nothing Then Return resp
            ' Fallback if API call fails
            Dim fallback As New ItemInfoResponse
            fallback.resultCd = "Error"
            fallback.resultMsg = "VSCU error: failed to call ItemSelect"
            fallback.data = New ItemInfoData() With {
                            .itemList = New List(Of ItemInfo)()
                            }
            Return fallback
        End Function
        '------------------------
        ' 5C) Item  Save (POST)
        '------------------------
        Public Async Function SaveItemAsync(req As ItemSaveRequest) As Task(Of ItemSaveResponse)
            Dim endPoint = ApiEndpoints.ITEM_SAVE
            Dim resp = Await SendAndDeserializeAsync(Of ItemSaveResponse)(endPoint, req, isGet:=False)
            If resp IsNot Nothing Then Return resp

            Dim fallback = MakeBaseFallback(Of ItemSaveResponse)("VSCU error: failed to call ItemSave")
            Return fallback
        End Function
        ' -----------------------
        ' 5d) Imported Item (POST)
        ' -----------------------
        Public Async Function SendImportedItemAsync(req As ImportedItemRequest) As Task(Of ImportedItemResponse)
            Dim endpoint = ApiEndpoints.IMPORTED_ITEM_UPDATE
            Dim resp = Await SendAndDeserializeAsync(Of ImportedItemResponse)(endpoint, req, isGet:=False)
            If resp IsNot Nothing Then Return resp

            Dim fallback = MakeBaseFallback(Of ImportedItemResponse)("VSCU error: failed to call ImportedItem")
            fallback.result = New ImportedItemData() With {.itemCode = req.itemCode, .importId = Nothing, .accepted = False}
            Return fallback
        End Function

        ' -----------------------
        ' 5e) Imported Items GET
        ' -----------------------
        Public Async Function GetImportedItemsAsync(Optional query As String = Nothing) As Task(Of ImportedItemResponse)
            Dim endpoint = ApiEndpoints.IMPORTED_ITEM_SELECT
            Dim target = endpoint
            If Not String.IsNullOrEmpty(query) Then
                target &= "?" & query
            End If

            Dim resp = Await SendAndDeserializeAsync(Of ImportedItemResponse)(target, Nothing, isGet:=False)
            If resp IsNot Nothing Then Return resp

            Dim fallback = MakeBaseFallback(Of ImportedItemResponse)("VSCU error: failed to call GetImportedItems")
            fallback.result = New ImportedItemData() With {.itemCode = Nothing, .importId = Nothing, .accepted = False}
            Return fallback
        End Function

        ' -----------------------
        ' 6) Sales (POST)
        ' -----------------------
        Public Async Function SendSalesAsync(req As SalesRequest) As Task(Of SalesResponse)
            Dim endpoint = ApiEndpoints.SALES ' set this constant in Core.ApiEndpoints
            Dim resp = Await SendAndDeserializeAsync(Of SalesResponse)(endpoint, req, isGet:=False)
            If resp IsNot Nothing Then Return resp

            Dim fallback As New SalesResponse
            fallback.resultCd = "Error"
            fallback.resultMsg = "VSCU error: failed to call SalesRegister"
            fallback.data = New SalesResponseData()
            Return fallback
        End Function

        ' -----------------------
        ' 7) Purchase (POST)
        ' -----------------------
        Public Async Function SendPurchaseAsync(req As PurchaseRequest) As Task(Of PurchaseResponse)
            Dim endpoint = ApiEndpoints.PURCHASE
            Dim resp = Await SendAndDeserializeAsync(Of PurchaseResponse)(endpoint, req, isGet:=False)
            If resp IsNot Nothing Then Return resp

            Dim fallback = MakeBaseFallback(Of PurchaseResponse)("VSCU error: failed to call Purchase")
            fallback.result = New PurchaseData() With {.purchaseId = req.purchaseId, .accepted = False}
            Return fallback
        End Function

        ' -----------------------
        ' 7b) Purchase GET
        ' -----------------------
        Public Async Function GetPurchaseAsync(query As PurchaseInfoRequest) As Task(Of PurchaseInfoResponse)
            Dim endpoint = ApiEndpoints.PURCHASE

            Dim resp = Await SendAndDeserializeAsync(Of PurchaseInfoResponse)(endpoint, query, isGet:=False)

            If resp IsNot Nothing AndAlso resp.data IsNot Nothing Then
                Return resp
            End If

            ' fallback if API failed
            Dim fb As New PurchaseInfoResponse
            fb.resultCd = "999"
            fb.resultMsg = "No data returned"
            fb.data = New PurchaseInfoData With {.saleList = New List(Of PurchaseRecord)}
            Return fb
        End Function

        ' -----------------------
        ' 8) Stock (POST)
        ' -----------------------
        Public Async Function SendStockAsync(req As StockSaveRequest) As Task(Of StockSaveResponse)
            Dim endpoint = ApiEndpoints.STOCK_SAVE
            Dim resp = Await SendAndDeserializeAsync(Of StockSaveResponse)(endpoint, req, isGet:=False)
            If resp IsNot Nothing Then Return resp

            Dim fallback As New StockSaveResponse()
            fallback.resultCd = "Error"
            fallback.resultMsg = "VSCU error: failed to call StockSave"
            fallback.data = New StockSaveResponse()
            Return fallback
        End Function

        ' -----------------------
        ' 8 a) Stock Request(POST)
        ' -----------------------
        Public Async Function GetStockAsync(req As StockInfoRequest) As Task(Of StockInfoResponse)
            Dim endpoint = ApiEndpoints.STOCK_SAVE
            Dim resp = Await SendAndDeserializeAsync(Of StockInfoResponse)(endpoint, req, isGet:=False)
            If resp IsNot Nothing Then Return resp

            Dim fallback = MakeBaseFallback(Of StockInfoResponse)("VSCU error: failed to call Stock")
            fallback.result = New StockInfoData() With {.itemCode = req.itemCode, .currentQuantity = 0, .updated = False}
            Return fallback
        End Function

        ' -----------------------
        ' 9) Notices (POST)
        ' -----------------------
        Public Async Function GetNoticesAsync(req As NoticeRequest) As Task(Of NoticeResponse)

            Dim endpoint = ApiEndpoints.NOTICE_SELECT

            Dim resp = Await SendAndDeserializeAsync(Of NoticeResponse)(endpoint, req, isGet:=False)

            If resp IsNot Nothing AndAlso resp.data IsNot Nothing Then
                Return resp
            End If

            ' fallback if API failed
            Dim fallback As New NoticeResponse
            fallback.resultCd = "999"
            fallback.resultMsg = "VSCU error: failed to call NoticeSelect"
            fallback.resultDt = DateTime.Now.ToString("yyyyMMddHHmmss")
            fallback.data = New NoticeData With {
                .noticeList = New List(Of NoticeItem)
            }

            Return fallback

        End Function

        ' -----------------------
        ' Generic helper: call arbitrary endpoint with raw JSON and get back raw body
        ' Useful for tests and troubleshooting
        ' -----------------------
        Public Async Function CallRawAsync(endpoint As String, payloadJson As String) As Task(Of String)
            Dim fullUrl = _client.FullUrl(endpoint)
            Dim raw As String = Nothing
            Dim caughtException As Exception = Nothing

            Try
                Await _logger.LogAsync(LogLevel.Info, $"Calling {fullUrl}", payloadJson)

                raw = Await _client.SendAsync(endpoint, payloadJson)

                Await _logger.LogAsync(LogLevel.Info, $"Response from {fullUrl}", raw)

                Return raw

            Catch ex As Exception
                ' Store the exception; DO NOT AWAIT in the Catch block
                caughtException = ex
                ' Return an error JSON (but logging must happen after Catch)
                Return JsonUtil.ToJson(New With {.error = True, .message = ex.Message})
            Finally
                ' Optional: anything non-await cleanup goes here
            End Try

            ' ---- After Catch (safe zone for Await) ----
            If caughtException IsNot Nothing Then
                Await _logger.LogAsync(
                    LogLevel.Error,
                    $"Exception calling {fullUrl}: {caughtException.Message}",
                    caughtException.ToString()
                )
            End If
        End Function
    End Class
End Namespace
