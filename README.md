# JsKRAIntegrator

## Project Summary
JsKRAIntegrator is a .NET application that integrates with the KRA (Kenya Revenue Authority) API flows. The project is split into two primary parts:

- **`Core`** — The main module containing the production business logic, integration code, models, and services. This is the primary codebase to review and extend.
- **`UI`** — A lightweight Windows Forms-based user interface used for manual testing, debugging, and demonstration. It is not intended as the production front-end.

Repository: [JsKRAIntegrator GitHub Repository](https://github.com/Theonlysmartboy/JsKRAIntegrator) (branch: V1)

## How it works (high-level)
1. The `Core` module implements integration components that prepare requests, manage authentication, and handle responses with the external KRA endpoints.
2. The `UI` project hosts a small test harness (Windows Forms) that exercises `Core` functionality for manual verification during development.
3. Developers should make changes primarily in `Core`. Use `UI` only to reproduce and debug scenarios during development.

## Requirements
- Visual Studio 2022
- .NET SDK matching the solution's target framework (open the project properties to check the TFM)

## Build & Run
1. Clone the repository and checkout the `V1` branch:

   ```bash
   git clone https://github.com/Theonlysmartboy/JsKRAIntegrator
   git checkout V1
   ```
2. Open the solution in Visual Studio 2022 (`.sln`) and restore NuGet packages if prompted.
3. Set the startup project as needed:
- For development and automated usage, build and test the `Core` project.
- To run manual tests or reproduce scenarios, set the `UI` project as the startup project and run (F5).

## Tests
- If unit tests exist, run them from Test Explorer in Visual Studio or using `dotnet test` in the solution folder.
- Prefer adding unit tests for any new public logic added to `Core`.

## Contributing
Thanks for contributing! Please follow these guidelines to keep the codebase consistent:

- **Core-first**: Make changes in `Core` unless you are updating the test harness in `UI`.
- **Branching**: Create descriptive feature branches off `V1` (for example, `feature/add-auth-refresh`).
- **Commits**: Use clear, small commits. Include a short description.
- **Pull Requests**: Open PRs against the `V1` branch. Describe the problem, your approach, and any testing steps.
- **Code Style**: Follow the project's `.editorconfig` and rules in `CONTRIBUTING.md`. The repository will contain these files; ensure your changes comply with them.
- **Tests**: Add unit tests for new logic and ensure all tests pass before requesting review.
- **Reviews**: Request at least one reviewer. Address review comments promptly.

## Development Notes
- The `UI` project is a convenience tool for manual testing only — avoid shipping UI changes as part of production features unless explicitly required.
- Keep business logic inside `Core`. The UI should only call into `Core` APIs.

## Contact / Maintainers
- Repository owner: [Theonlysmartboy](https://github.com/Theonlysmartboy)
