# KRA Integrator — CORE Documentation

This document describes the KRA Integrator CORE project (the integration engine). It is intended for developers, architects and functional users. It documents only the CORE project and treats the UI as a demo/test layer (reference only).

Repository path: `Core/` (project file: `Core/Core.vbproj`)

---

Table of Contents
- System Overview
- Architecture Breakdown
- Integration Flow (POS → CORE → KRA → CORE → POS)
- Core Components
- Data Models (representative)
- KRA Communication Layer
- POS Integration Layer (how to call the core)
- UI (Demo Layer) — Reference Only
- Error Handling & Logging
- Configuration
- Developer Integration Guide (VB.NET POS)
- Functional Overview (Non-Technical)

---

1. System Overview

- What the CORE is
    - The `Core` project is the integration engine that translates and forwards POS requests to the local VSCU service (KRA VSCU Java service) and returns structured responses to callers.
- Primary purpose
    - Provide a typed, reusable API for POS systems written in VB.NET to interact with the tax authority adapter (VSCU).
    - Persist logs and read runtime configuration from a MySQL `settings` table.
- High-level architecture
    - POS (or UI demo) -> Core.VSCUIntegrator (client wrapper) -> Core.Main.ApiClient (HTTP) -> VSCU service (default `http://localhost:8088`) -> Core (response parsing/fallback) -> POS

2. Architecture Breakdown

- Core modules (top-level folders/files)
    - `Core/Main` — runtime entry and HTTP client helpers: `VscuStarter.vb`, `ApiClient.vb`, `ApiEndpoints.vb`.
    - `Core/Services` — `VSCUIntegrator.vb` (primary integration surface provided to callers).
    - `Core/Config` — `SettingsManager.vb`, `IntegratorSettings.vb` (configuration access and DTOs).
    - `Core/Logging` — `Logger.vb`, `LogRepository.vb`, `LogEntry.vb` (logging into MySQL table `logs`).
    - `Core/Utils` — `JsonUtil.vb` (JSON serialization/deserialization using Newtonsoft.Json).
    - `Core/Models` — request/response DTOs grouped by domain (Sale, Purchase, Item, Branch, Init, Notice, Stock, Code, etc.).

- Class responsibilities (summary)
    - `VSCUIntegrator` — main public façade for integration operations (methods like `SendSalesAsync`, `InitializeAsync`, `GetCodeDataAsync`, etc.).
    - `ApiClient` — low-level HTTP client wrapper used by the integrator (builds URLs, executes GET/POST, returns raw body).
    - `VscuStarter` — helper to start/stop the local VSCU Java jar (checks port 8088, spawn `java -jar`).
    - `SettingsManager` — reads/writes named settings from the `settings` MySQL table.
    - `Logger` / `LogRepository` — write runtime logs to MySQL `logs` table.
    - `JsonUtil` — central serializer using `JsonConvert` from Newtonsoft.Json.

- Flow of data (text-based diagram)
    - POS (or UI) → instantiate `VSCUIntegrator` → call operation (e.g., `SendSalesAsync(req)`) → `VSCUIntegrator` builds JSON with `JsonUtil` → `ApiClient.SendAsync()` POST JSON to `BaseUrl + endpoint` → VSCU service responds (JSON) → `VSCUIntegrator` deserializes into typed response → caller receives typed response (or a structured fallback on error).

3. Integration Flow (step-by-step)

How POS sends data into core
- The POS creates a `IntegratorSettings` instance (or reads settings via `SettingsManager`) and constructs `VSCUIntegrator` with a `Logger`.
- Example pattern used by the demo UI (reference):
    - Read `base_url`, `pin`, `branch_id`, `device_serial`, `timeout` from `SettingsManager`.
    - Create `Dim integrator As New VSCUIntegrator(settings, logger)`.
    - Call integrator methods such as `SendSalesAsync`, `GetItemAsync`, `SaveItemAsync`, etc.

How core processes requests
- `VSCUIntegrator` maps method calls to HTTP endpoints defined in `ApiEndpoints.vb` (e.g. `SALES` -> `/trnsSales/saveSales`).
- For each method, `VSCUIntegrator`:
    - Serializes the request payload using `JsonUtil.ToJson`.
    - Logs the outgoing request payload (via `Logger.LogAsync`).
    - Uses `ApiClient` to `POST` (or `GET`) the JSON payload to the configured `BaseUrl`.
    - Logs raw response body.
    - Attempts to deserialize response into the typed response class using `JsonUtil.FromJson(Of T)`.
    - On failure (HTTP error, timeout, or deserialization error) it logs the error and returns a structured fallback response (most responses include `resultCd`, `resultMsg`, `resultDt` and `data` shaped to each API).

How data is transformed
- The Core does not alter business semantics; it treats incoming VB.NET DTOs (Models) as the JSON payload shape expected by the VSCU service.
- Serialization/deserialization uses Newtonsoft.Json via `JsonUtil`. The code commonly serializes anonymous types (UI example for `receipt` and `itemList`) or concrete request DTOs from `Core/Models`.

How responses are returned
- Responses are deserialized into typed response classes (e.g., `SalesResponse`, `InitInfoResponse`).
- Every response inherits from `BaseResponse` (has `resultCd`, `resultMsg`, `resultDt`) or contains those fields — the integrator returns the typed object to the caller.
- If the HTTP call fails, `VSCUIntegrator` returns a fallback typed object created either with `MakeBaseFallback` or explicit fallback initializers (usually with `resultCd = "Error"` and `resultMsg` stating the VSCU error).

How UI triggers these flows (REFERENCE ONLY)
- The UI demo constructs `IntegratorSettings` using `SettingsManager` values, constructs `VSCUIntegrator` and calls methods such as `SendSalesAsync` (see `Ui/Sales.vb`).
- The demo also uses `VscuStarter` to ensure the VSCU Java service is running (it checks TCP listeners for port 8088 and may start the configured jar path).

4. Core Components Documentation (major classes)

- `VSCUIntegrator` (`Core/Services/VSCUIntegrator.vb`)
    - Responsibility: Primary façade offered to POS systems. Implements one method per VSCU endpoint (Initialize, GetCodeData, Sales, Purchase, Item Save/Select, Stock, Notices, etc.).
    - Key methods (representative):
    - `InitializeAsync(req As InitInfoRequest) As Task(Of InitInfoResponse)`
    - `GetCodeDataAsync(request As CodeDataRequest) As Task(Of CodeDataResponse)`
    - `SendSalesAsync(req As SalesRequest) As Task(Of SalesResponse)`
    - `SavePurchaseAsync(req As PurchaseTransactionRequest) As Task(Of PurchaseTransactionResponse)`
    - `SendStockMasterAsync(req As StockMasterSaveRequest) As Task(Of StockMasterSaveResponse)`
    - `GetNoticesAsync(req As NoticeRequest) As Task(Of NoticeResponse)`
    - `CallRawAsync(endpoint As String, payloadJson As String) As Task(Of String)` — raw helper for troubleshooting
    - Inputs/Outputs: Accepts typed request models (from `Core/Models`), returns typed response models. On failure returns a structured fallback typed object or `Nothing` in some helper flows.
    - Dependencies: `ApiClient`, `Logger`, `ApiEndpoints`, `JsonUtil`.

- `ApiClient` (`Core/Main/ApiClient.vb`)
    - Responsibility: Small HTTP helper wrapping `HttpClient` for GET/POST with JSON.
    - Key methods:
    - `SendAsync(endpoint As String, json As String) As Task(Of String)` — POST JSON
    - `GetAsync(endpoint As String) As Task(Of String)` — GET
    - `FullUrl(endpoint As String) As String` — builds full URL for diagnostics
    - Important notes: `ApiClient` constructor accepts `baseUrl` and `timeoutMs`. It sets `HttpClient.Timeout` to `TimeSpan.FromMilliseconds(timeoutMs)`. (See NOTE in Configuration below regarding units.)

- `VscuStarter` (`Core/Main/VscuStarter.vb`)
    - Responsibility: Start/stop the VSCU Java service (jar), check if port 8088 is listening, stop a process by port.
    - Key methods:
    - `StartKraVscuJar() As Task(Of Boolean)` — read `vscu_jar_path` from settings, start the jar with `java -jar`.
    - `IsJarRunning() As Task(Of Boolean)` — checks active TCP listeners for port 8088.
    - `StopVscu() As Task(Of String)` — kills stored process.
    - `StopVscuByPort(port As Integer) As Task(Of Integer)` — runs `netstat` and attempts to kill the PID using the port.

- `SettingsManager` (`Core/Config/SettingsManager.vb`)
    - Responsibility: Read/write named settings from/to a MySQL `settings` table. Methods are asynchronous and use `MySql.Data`.
    - Key methods: `GetSettingAsync(key)`, `SetSettingAsync(key, value)`, `GetAllSettings()`, `DeleteSettingAsync(key)`, and convenience getters `GetBaseUrl()`, `GetPin()`, `GetBranchId()`, `GetDeviceSerial()`, `GetTimeout()`.

- `Logger` & `LogRepository` (`Core/Logging`)
    - Responsibility: Persist runtime logs to MySQL via `LogRepository.AddLogAsync`. `Logger.LogAsync` builds `LogEntry` and delegates to `LogRepository`.
    - Log repository writes into table `logs` with columns `level`, `message`, `details`, `created_at`.

- `JsonUtil` (`Core/Utils/JsonUtil.vb`)
    - Responsibility: Centralize JSON serialization/deserialization using `Newtonsoft.Json` (`JsonConvert.SerializeObject` / `DeserializeObject`).

5. Data Models (representative list)

Models are under `Core/Models` and grouped by domain. Each response normally contains `resultCd`, `resultMsg`, `resultDt` (from `BaseResponse`). Examples:

- `Models.BaseResponse` — common response properties: `resultCd`, `resultMsg`, `resultDt`.
- `Models.Init.InitInfoRequest` / `InitInfoResponse` — device/branch initialization (contains `tin`, `bhfId`, `dvcSrlNo` in request; response wraps `InitInfo` with keys such as `intrlKey`, `signKey`, `sdcId`, `mrcNo`, last invoice numbers, etc.).
- `Models.Sale.SalesRequest` / `SalesResponse` — sales/invoice payload and response fields:
    - `SalesRequest` fields include `tin`, `bhfId`, `trdInvcNo`, `invcNo`, customer info and totals, `receipt` (anonymous object used by UI example) and `itemList` (list of item objects with `itemSeq`, `itemCd`, `qty`, `prc`, `taxTyCd`, `taxblAmt`, `taxAmt`, `totAmt`).
    - `SalesResponse.data` contains `rcptNo`, `intrlData`, `rcptSign`, `vsdcRcptPbctDate`, `sdcId`, `mrcNo`.
- Other models documented in code: `PurchaseTransactionRequest/Response`, `ItemSaveRequest/Response`, `ItemInfoRequest/Response`, `StockMovementRequest/Response`, `NoticeRequest/Response`, `CodeDataRequest/Response`, `BranchListRequest/Response`, `CustomerRequest/Response`, `ImportItemsRequest/Response`, etc.

- Serialization formats: JSON is used end-to-end between Core and VSCU service. `ApiClient` posts JSON with `application/json` Content-Type. `JsonUtil` handles serialization and deserialization.

6. KRA Communication Layer

- Protocol
    - HTTP POST/GET of JSON payloads to endpoints defined in `Core/Main/ApiEndpoints.vb`. No SOAP, no binary protocols.
    - Default base URL: `http://localhost:8088` (if `IntegratorSettings.BaseUrl` is empty, `ApiClient` sets base URL to this default).

- Endpoints (as coded in `ApiEndpoints.vb` — exact paths):
    - `/initializer/selectInitInfo`
    - `/code/selectCodes`
    - `/itemClass/selectItemsClass`
    - `/customers/selectCustomer`
    - `/branches/saveBrancheCustomers`
    - `/branches/selectBranches`
    - `/branches/saveBrancheUsers`
    - `/branches/saveBrancheInsurances`
    - `/items/saveItemComposition`
    - `/items/saveItems`
    - `/items/selectItems`
    - `/imports/selectImportItems`
    - `/imports/updateImportItems`
    - `/trnsSales/saveSales`
    - `/trnsPurchase/selectTrnsPurchaseSales`
    - `/trnsPurchase/savePurchases`
    - `/stock/saveStockItems`
    - `/stock/selectStockItems`
    - `/stockMaster/saveStockMaster`
    - `/notices/selectNotices`

- Authentication
    - There is no HTTP header based authentication in `ApiClient` (no tokens, no Basic auth). Authentication/identification fields (such as `tin`, `dvcSrlNo`, `bhfId`, `pin`) are sent as part of the JSON payload where required by the endpoint model (e.g. `SalesRequest.tin`).

- Payload structure
    - Typed VB.NET DTOs under `Core/Models` map 1:1 to JSON payload properties (Newtonsoft.Json default naming preserved). UI often builds anonymous objects for `receipt` and `itemList` which are serialized inline.

- Error handling and retries
    - `ApiClient.SendAsync` calls `HttpClient.PostAsync` and uses `resp.EnsureSuccessStatusCode()`; any non-successful HTTP response will throw and is caught by `VSCUIntegrator.SendAndDeserializeAsync`.
    - `VSCUIntegrator.SendAndDeserializeAsync` logs the exception and returns `Nothing`. Each method then creates a typed fallback response with `resultCd`/`resultMsg` populated. There is no automatic retry logic implemented in the core.

- Timeout handling
    - `ApiClient` sets `HttpClient.Timeout = TimeSpan.FromMilliseconds(timeoutMs)` using the value passed to its constructor.
    - `IntegratorSettings.Timeout` default is `30` (see `Core/Config/IntegratorSettings.vb`). Note: this value is passed directly into `ApiClient` — the `ApiClient` interprets that integer as milliseconds. This means the default `30` causes a 30 millisecond timeout unless the caller stores timeout as milliseconds. Review the `Timeout` units in configuration before production use.

7. POS Integration Layer (how VB.NET POS systems connect)

- Required entry points
    - Instantiate `LogRepository` and `Logger` (for logging), or pass a compatible logger implementation.
    - Prepare `IntegratorSettings` (at minimum `BaseUrl` and sensible `Timeout`). Common settings used by the demo: `base_url`, `pin`, `branch_id`, `device_serial`, `timeout`.
    - Construct `VSCUIntegrator` with the `IntegratorSettings` and `Logger`.
    - Call operations on `VSCUIntegrator` asynchronously, e.g. `Await integrator.SendSalesAsync(salesReq)`.

- Expected request/response formats
    - Requests: strongly typed VB.NET DTOs (see `Core/Models`) or anonymous objects shaped to the model. JSON is produced by `JsonUtil.ToJson` (Newtonsoft).
    - Responses: typed response classes (e.g. `SalesResponse`) with `resultCd` and `resultMsg`. On error the core will return a fallback typed object (not throw) for most methods.

Example (VB.NET usage snippet — POS integration):
```
' Build IntegratorSettings (example)
Dim settings As New Core.Config.IntegratorSettings With {
    .BaseUrl = "http://localhost:8088",
    .Timeout = 30000, ' milliseconds; ApiClient expects milliseconds
    .Pin = "A004195331",
    .BranchId = "001",
    .DeviceSerial = "DEVICE123"
}

Dim logRepo = New Core.Logging.LogRepository(myConnString)
Dim logger = New Core.Logging.Logger(logRepo)
Dim integrator = New Core.Services.VSCUIntegrator(settings, logger)

' Call operation
Dim salesReq = New Core.Models.Sale.SalesRequest() With { .tin = settings.Pin, .bhfId = settings.BranchId }
Dim resp = Await integrator.SendSalesAsync(salesReq)
If resp IsNot Nothing AndAlso resp.resultCd = "000" Then
    ' success – inspect resp.data
Else
    ' fallback / error – inspect resp.resultMsg
End If
```

8. UI (Demo Layer ONLY — reference)

- How the UI triggers core functions
    - The UI reads settings from the DB through `SettingsManager` and builds `IntegratorSettings`.
    - It uses `VscuStarter` to start the local Java VSCU jar (the demo assumes a local VSCU service listening on port 8088).
    - Example: `Ui/Sales.vb` builds a `SalesRequest` from invoice DB data and calls `_integrator.SendSalesAsync(req)`.

- What is simulated vs real
    - The UI constructs the domain payloads from demo DB tables and demonstrates printing/qr generation. The UI is NOT business logic and must not be used as the canonical integration implementation.

- Why UI exists
    - For testing, manual verification and developer reference.

9. Error Handling & Logging

- Logging strategy
    - All outgoing requests and raw responses are logged via `Logger.LogAsync`. Exceptions and deserialization errors are logged with `LogLevel.Error` including stack traces and raw payloads where applicable.
    - Logs are persisted to a MySQL `logs` table by `LogRepository.AddLogAsync`.

- Exception handling flow
    - `ApiClient` throws on non-success HTTP responses.
    - `VSCUIntegrator.SendAndDeserializeAsync` catches the HTTP exception, logs the error, then returns `Nothing`. Callers in `VSCUIntegrator` methods produce fallback typed responses.

- Failure recovery
    - There is no automatic retry mechanism. Recovery is manual or must be implemented by the caller. The integrator provides `CallRawAsync` for troubleshooting and `VscuStarter` to (re)start the VSCU service.

10. Configuration

- Where runtime configuration lives
    - Database table `settings` (accessed via `SettingsManager` using MySQL connection string supplied to `SettingsManager` constructor).
    - Common keys used by the demo/core: `base_url`, `pin`, `branch_id`, `device_serial`, `timeout`, `vscu_jar_path`, `qr_code_base_url`.

- API keys and secrets
    - No HTTP header API key implementation present. Sensitive values like `pin` or `device_serial` are read from `settings` and commonly included in request payloads.

- Important NOTE on `timeout`
    - `IntegratorSettings.Timeout` is an `Integer` with a default value `30` in `IntegratorSettings.vb`.
    - `ApiClient` uses the integer as milliseconds (`TimeSpan.FromMilliseconds(timeoutMs)`). If you keep the default `30` this will be a 30ms HTTP timeout. In practice provide timeout in milliseconds (e.g. `30000` for 30s).

11. Developer Integration Guide (VB.NET POS)

- Steps to integrate
    1. Add a reference to the `Core` project or compile `Core` as a DLL and reference it from the POS solution.
    2. Provide a MySQL connection string (to use `SettingsManager` / `LogRepository`) or implement compatible replacements for settings/logging.
    3. Instantiate `LogRepository` and `Logger`.
    4. Build an `IntegratorSettings` instance and set at minimum `BaseUrl` and `Timeout` (in milliseconds).
    5. Construct `Dim integrator = New Core.Services.VSCUIntegrator(settings, logger)`.
    6. Build request payloads using DTOs from `Core/Models` (or anonymous objects that match property names) and call the appropriate async method (e.g. `Await integrator.SendSalesAsync(req)`).

- Best practices
    - Always set a sensible `Timeout` in milliseconds (e.g. 30000).
    - Ensure `tin` / `pin` / `bhfId` are supplied in requests as required by KRA service.
    - Use the `Logger` to persist important trace for troubleshooting.
    - Do not rely on UI code paths for business logic — the UI is a reference implementation only.

12. Functional Overview (Non-Technical)

- What the system does (simple terms)
    - Receives structured invoice/stock/customer/branch requests from a POS, forwards them to the local KRA adapter service (VSCU), and returns the tax authority response to the POS.
    - Persists logs and reads configuration values from a database.

- How data moves (simple flow)
    - POS sends invoice / item / stock data → Core serializes and sends JSON to VSCU → VSCU responds → Core returns parsed response to POS.

- What users/clerks experience
    - After a sale, the POS sends the invoice to the Core which communicates with KRA and returns a receipt number and signature. The UI demo shows the receipt preview and stores the results in its demo DB.

- Rules and limitations (as implemented)
    - Communication is synchronous HTTP JSON to VSCU; there is no background retry queue implemented in Core.
    - Authentication is performed by inclusion of identifying fields (`tin`, `dvcSrlNo`, `bhfId`, etc.) in the payload — there is no bearer-token header mechanism.

Appendix — Key files (Core)
- `Core/Main/ApiClient.vb`
- `Core/Main/ApiEndpoints.vb`
- `Core/Main/VscuStarter.vb`
- `Core/Services/VSCUIntegrator.vb`
- `Core/Config/SettingsManager.vb`
- `Core/Config/IntegratorSettings.vb`
- `Core/Logging/Logger.vb`
- `Core/Logging/LogRepository.vb`
- `Core/Utils/JsonUtil.vb`
- `Core/Models/*`