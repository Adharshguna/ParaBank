# Parabank UI Automation

This project contains end-to-end UI automation tests for [Parabank](https://parabank.parasoft.com/) using Playwright, .NET 8, and NUnit.

## Features

- Page Object Model (POM) structure
- Positive, negative, and validation test scenarios
- Proper synchronization with auto-wait and explicit waits
- Independent, repeatable, and scalable tests
- Playwright HTML reporting
- GitHub Actions CI/CD pipeline
- Encryption utility for sensitive data
- Screenshots, videos, and traces on test failures

## Prerequisites

- .NET 8 SDK
- Playwright browsers (installed via `playwright install`)

## Setup

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd Parabank
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Install Playwright browsers:
   ```bash
   dotnet tool install --global Microsoft.Playwright.CLI
   playwright install
   ```

4. Configure environment variables:
   - Copy `.env.example` to `.env` (if provided) or create `.env` with:
     ```
     BASE_URL=https://parabank.parasoft.com/
     USERNAME=your_test_username
     ENCRYPTED_PASSWORD=your_encrypted_password
     ```
   - To encrypt a password, use the `EncryptionUtil.Encrypt("yourpassword")` method in code.

## Running Tests

Run all tests:
```bash
dotnet test
```

Run tests with HTML report:
```bash
dotnet test -- Playwright.Reporter=html
```

## Viewing Reports

After running tests, view the HTML report:
```bash
playwright show-report
```

The report will be available in the `playwright-report/` directory.

## Test Structure

- **Pages/**: Page Object classes (BasePage, LoginPage, RegistrationPage, AccountsPage)
- **Tests/**: Test classes (LoginTests, RegistrationTests, AccountsTests)
- **Utils/**: Utility classes (EncryptionUtil, TestData)

## CI/CD

GitHub Actions workflow runs tests on every push and pull request to the main branch. Artifacts (reports, traces, screenshots, videos) are uploaded for failed tests.

## Test Data Strategy

Test data is managed in `Utils/TestData.cs` with environment variables for sensitive data. Passwords are encrypted using `EncryptionUtil`.

## Synchronization

Tests use Playwright's auto-wait capabilities. Explicit waits are implemented where necessary using `WaitForLoadStateAsync` and locator assertions.