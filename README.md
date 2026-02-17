# JsKRAIntegrator

## Project Summary
JsKRAIntegrator is a .NET application that integrates with the KRA (Kenya Revenue Authority) API flows. The project is split into two primary parts:

- **`Core`** — The main module containing the production business logic, integration code, models, and services. This is the primary codebase to review and extend.
- **`UI`** — A lightweight Windows Forms-based user interface used for manual testing, debugging, and demonstration. It is not intended as the production front-end.

Repository: [JsKRAIntegrator GitHub Repository](https://github.com/Theonlysmartboy/JsKRAIntegrator) (branch: V1.1)

## How it works (high-level)
1. The `Core` module implements integration components that prepare requests, manage authentication, and handle responses with the external KRA endpoints.
2. The `UI` project hosts a small test harness (Windows Forms) that exercises `Core` functionality for manual verification during development.
3. Developers should make changes primarily in `Core`. Use `UI` only to reproduce and debug scenarios during development.

## Requirements
- Visual Studio 2022
- .NET SDK matching the solution's target framework (open the project properties to check the TFM)

## Build & Run
1. Clone the repository and checkout the `V1.1` branch:

   ```bash
   git clone https://github.com/Theonlysmartboy/JsKRAIntegrator
   git checkout V1
   ```
2. Open the solution in Visual Studio 2022 (`.sln`) and restore NuGet packages if prompted.
3. Set the startup project as needed:
- For development and automated usage, build and test the `Core` project.
- To run manual tests or reproduce scenarios, set the `UI` project as the startup project and run (F5).

## UI — Test Harness

### Purpose
The `UI` project is a lightweight Windows Forms test harness for manual testing, debugging, and demonstration of the `Core` module. It is not the production front-end and should not contain business logic; all production code lives in `Core`.

### High-level architecture
- **`Core`** — Contains production business logic, services, models, integration helpers, and is the authoritative module.
- **`UI`** — Calls into `Core` to exercise scenarios. The UI only orchestrates user interactions and visualizes results (receipt preview, print preview, simple CRUD screens for test data).

#### Key forms and examples
- **`Sales` (Ui\Sales.vb)**: Demonstrates sending an invoice via `Core.VSCUIntegrator`, rendering athermal-style receipt preview, QR generation, and printing. This form:
- Loads invoice master and detail rows from the local repository (`Ui.Repo.SalesRepository`).
- Builds a `Core.Models.Sale.SalesRequest` and calls `VSCUIntegrator.SendSalesAsync`.
- Persists VSCU response values (CU serial, receipt number, QR URL) back to the invoice master.
- Renders a long receipt bitmap for scrollable preview and thermal-style printing.

### Runtime settings and dependencies
- Settings are persisted in the project's settings store and loaded via `Core.Config.SettingsManager`. Required keys used by the UI (examples):
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

### Build & run (Visual Studio 2022)
1. Open the solution in **Visual Studio 2022**.
2. Restore NuGet packages if prompted.
3. Set the startup project to `UI` to use the Windows Forms test harness, or to `Core` for library testing.
4. Ensure the connection string/settings repository is accessible (the `UI` project reads settings and invoice data via a repository that uses the configured connection string).
5. Use the `Sales` form to input an invoice number and click `Send Invoice` to exercise the end-to-end flow.

### Testing & debugging guidance
- Use the `UI` to reproduce integration scenarios only. Avoid putting logic here — add or fix behavior in `Core` and verify through the UI.
- The `Sales` form logs errors using `Core.Logging.Logger`, which persists via the UI repository implementation. Check logs when troubleshooting.
- Receipt rendering: the UI constructs a tall `Bitmap` containing all receipt lines and draws QR codes inline next to the `CU SN:` line. Printing is implemented by slicing this bitmap into pages for thermal-style continuous printing.

### Contributing to the UI project
- Keep changes focused on tooling, debugging helpers, and small UX improvements for testing.
- Do not move business logic into `UI`. Any change that affects integration or validation should be made in `Core` with corresponding unit tests.
- Follow the repository-level `CONTRIBUTING.md` and `.editorconfig` rules.
- When adding third-party packages to the UI, prefer packages scoped for developer experience (e.g., logging adapters, diagnostic helpers) and document them in this README.

### Notes
- The `UI` project is intended as a developer tool. Production integrations should only consume `Core` and not depend on any Windows Forms components.

## Tests
- If unit tests exist, run them from Test Explorer in Visual Studio or using `dotnet test` in the solution folder.
- Prefer adding unit tests for any new public logic added to `Core`.

## Contributing
Thanks for contributing! Please follow these guidelines to keep the codebase consistent:

- **Core-first**: Make changes in `Core` unless you are updating the test harness in `UI`.
- **Branching**: Create descriptive feature branches off `V1.1` (for example, `feature/add-auth-refresh`).
- **Commits**: Use clear, small commits. Include a short description.
- **Pull Requests**: Open PRs against the `V1.1` branch. Describe the problem, your approach, and any testing steps.
- **Code Style**: Follow the project's `.editorconfig` and rules in `CONTRIBUTING.md`. The repository will contain these files; ensure your changes comply with them.
- **Tests**: Add unit tests for new logic and ensure all tests pass before requesting review.
- **Reviews**: Request at least one reviewer. Address review comments promptly.

## Development Notes
- The `UI` project is a convenience tool for manual testing only — avoid shipping UI changes as part of production features unless explicitly required.
- Keep business logic inside `Core`. The UI should only call into `Core` APIs.

## Contact / Maintainers
- Repository owner: [Theonlysmartboy](https://github.com/Theonlysmartboy)
