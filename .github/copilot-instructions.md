<!-- Parabank UI Automation Framework - Setup Checklist -->

# Parabank UI Automation Framework Setup

This is a clean, production-ready .NET Playwright automation framework following best practices.

## Project Overview
- **Framework**: NUnit with Playwright
- **Pattern**: Page Object Model (POM)
- **Target**: [Parabank](https://parabank.parasoft.com/)
- **Version**: .NET 7.0
- **Status**: ✅ Production Ready

## Setup Completed
- [x] Project scaffolded with NUnit and Playwright
- [x] Page Object Model structure implemented
- [x] Test scenarios created (Login, Registration, Accounts)
- [x] Test data centralized in `TestData.cs` (no encryption/env files needed)
- [x] CI/CD pipeline configured (GitHub Actions)
- [x] Documentation updated (README.md)
- [x] Removed unnecessary dependencies (DotNetEnv)
- [x] Removed encryption utilities
- [x] All tests passing

## Key Features
- **Clean Architecture**: POM with BaseTest and TestBase for proper setup/teardown
- **Simplified Test Data**: Hardcoded values in `TestData.cs` (no .env file)
- **Automatic Artifacts**: Screenshots, videos, traces on test failures
- **CI/CD Ready**: GitHub Actions workflow included
- **Maintainable**: Clear separation of concerns

## How to Run

### Local Testing
```bash
# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "FullyQualifiedName~Parabank.Tests.LoginTests"

# Run with detailed output
dotnet test --verbosity detailed
```

### CI/CD
Tests automatically run on:
- Push to `main` branch
- Pull requests to `main` branch

Test artifacts (screenshots, videos, traces) are uploaded on failure.

## Project Structure
```
Pages/           → Page Object Model classes
Tests/           → Test classes (LoginTests, RegistrationTests, AccountsTests)
Utils/           → TestData.cs with centralized test data
BaseTest.cs      → NUnit setup fixture (one-time setup)
TestBase.cs      → Base test class (per-test setup)
```

## Best Practices Implemented
- ✅ Independent, repeatable, and scalable tests
- ✅ Proper synchronization using Playwright auto-wait
- ✅ One-time browser setup/teardown in BaseTest
- ✅ Per-test page creation and cleanup
- ✅ Automatic failure artifacts (screenshots, videos, traces)
- ✅ GitHub Actions for continuous integration

## Maintenance Notes
- Test data is hardcoded for simplicity and portability
- Browser is launched once per test run; pages are created per test
- Traces, videos, and screenshots are captured automatically on failure
- CI/CD runs tests on .NET 7.0 (ubuntu-latest)