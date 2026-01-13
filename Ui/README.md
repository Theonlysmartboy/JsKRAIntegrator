# UI (Ui) — Test Harness

## Purpose
The `Ui` project is a lightweight Windows Forms test harness for manual testing, debugging, and demonstration of the `Core` module. It is not the production front-end and should not contain business logic; all production code lives in `Core`.

## High-level architecture
- `Core` — Contains production business logic, services, models, integration helpers and is the authoritative module.
- `Ui` — Calls into `Core` to exercise scenarios. The UI only orchestrates user interactions and visualizes results (receipt preview, print preview, simple CRUD screens for test data).

Key forms and examples
- `Sales` (`Ui\Sales.vb`): Demonstrates sending an invoice via `Core.VSCUIntegrator`, rendering a thermal-style receipt preview, QR generation, and printing. This form:
	- Loads invoice master and detail rows from the local repository (`Ui.Repo.SalesRepository`).
	- Builds a `Core.Models.Sale.SalesRequest` and calls `VSCUIntegrator.SendSalesAsync`.
	- Persists VSCU response values (CU serial, receipt number, QR URL) back to the invoice master.
	- Renders a long receipt bitmap for scrollable preview and thermal-style printing.

## Runtime settings and dependencies
	- Settings are persisted in the project's settings store and loaded via `Core.Config.SettingsManager`.
	Required keys used by the UI (examples):
	- `base_url` — integrator base URL
	- `pin` — business PIN (TIN)
	- `branch_id` — branch identifier
	- `device_serial` — (optional) device serial
	- `timeout` — HTTP timeout in seconds (defaults to 30)
	- `qr_code_base_url` — base URL used to build QR payload displayed on receipts

- Dependencies used by the UI project:
	- `QRCoder` — generate QR codes shown on receipts
	- `Newtonsoft.Json` — payload serialization
	- `System.Drawing` — receipt rendering

## Build & run (Visual Studio 2022)
1. Open the solution in __Visual Studio 2022__.
2. Restore NuGet packages if prompted.
3. Set startup project to `Ui` to use the Windows Forms test harness, or to `Core` for library testing.
4. Ensure the connection string / settings repository is accessible (the `Ui` project reads settings and invoice data via a repository that uses the configured connection string).
5. Use the `Sales` form to input an invoice number and click `Send Invoice` to exercise the end-to-end flow.

## Testing & debugging guidance
- Use the `UI` to reproduce integration scenarios only. Avoid putting logic here — add or fix behavior in `Core` and verify through the UI.
- The `Sales` form logs errors using `Core.Logging.Logger` which persists via the UI repository implementation. Check logs when troubleshooting.
- Receipt rendering: the UI constructs a tall `Bitmap` containing all receipt lines and draws QR codes inline next to the `CU SN:` line. Printing is implemented by slicing this bitmap into pages for thermal-style continuous printing.

## Contributing to the UI project
- Keep changes focused on tooling, debugging helpers, and small UX improvements for testing.
- Do not move business logic into `Ui`. Any change that affects integration or validation should be made in `Core` with corresponding unit tests.
- Follow the repository-level `CONTRIBUTING.md` and `.editorconfig` rules.
- When adding third-party packages to the UI, prefer packages scoped for developer experience (e.g., logging adapters, diagnostic helpers) and document them in this README.

## Notes
- The `Ui` project is intended as a developer tool. Production integrations should only consume `Core` and not depend on any Windows Forms components.
