# Parabank UI Automation Framework

A clean, production-ready .NET Playwright automation framework for [Parabank](https://parabank.parasoft.com/) using NUnit and the Page Object Model (POM) pattern.

## Features

- ✅ **Page Object Model (POM)** - Clean separation of page interactions and test logic
- ✅ **NUnit Framework** - Robust test framework with fixtures and assertions
- ✅ **Playwright** - Modern browser automation with auto-wait capabilities
- ✅ **Hardcoded Test Data** - Centralized in `TestData.cs`, no configuration needed
- ✅ **Video & Screenshot Capture** - Automatic recording on test failures
- ✅ **Trace Recording** - Playwright traces for debugging
- ✅ **CI/CD Integration** - GitHub Actions pipeline for automated testing
- ✅ **Cross-browser Support** - Configured for Chromium (extensible to Firefox, WebKit)
- ✅ **Zero Dependencies** - No environment files or encryption utilities needed

## Prerequisites

- **.NET 7.0 SDK or higher** - [Download](https://dotnet.microsoft.com/download)
- **Git** - For cloning the repository
- **Administrator access** - For installing Playwright browsers

### System Requirements

| OS | Version | Notes |
|---|---|---|
| Windows | 10 or later | Tested on Windows 10/11 |
| macOS | 10.15 or later | Works with Intel and Apple Silicon |
| Linux | Ubuntu 18.04+ | Debian, CentOS, RHEL supported |

## Quick Start (5 minutes)

### Step 1: Clone the Repository
```bash
git clone <repository-url>
cd Parabank
```

### Step 2: Install Dependencies
```bash
dotnet restore
```

### Step 3: Install Playwright Browsers
```bash
dotnet tool install --global Microsoft.Playwright.CLI
playwright install
```

### Step 4: Run Tests
```bash
dotnet test
```

## Complete Setup Guide for New Machine

### Windows Setup

#### 1. Install .NET 7.0 SDK
```powershell
# Download from: https://dotnet.microsoft.com/download/dotnet/7.0
# Or use Chocolatey:
choco install dotnet-sdk-7.0

# Verify installation
dotnet --version
# Expected output: 7.x.x
```

#### 2. Clone Repository
```powershell
git clone <repo-url>
cd Parabank
```

#### 3. Restore Dependencies
```powershell
dotnet restore
```

#### 4. Install Playwright Browsers
```powershell
# Install Playwright CLI tool
dotnet tool install --global Microsoft.Playwright.CLI

# Install browser binaries
playwright install

# Verify installation
playwright --version
```

#### 5. Build Project
```powershell
dotnet build
# Expected output: "Build succeeded"
```

#### 6. Run Tests
```powershell
# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "FullyQualifiedName~LoginTests"

# Run with detailed output
dotnet test --verbosity detailed

# Run with coverage
dotnet test /p:CollectCoverage=true
```
## Test Data Configuration

All test data is **hardcoded** in `Utils/TestData.cs` - **no configuration files needed**:


## Running Tests

### All Tests
```bash
dotnet test
```

### Specific Test Class
```bash
dotnet test --filter "FullyQualifiedName~Parabank.Tests.LoginTests"
```

### Specific Test Method
```bash
dotnet test --filter "Name=ValidLogin"
```
## Project Structure

```
Parabank/
├── Pages/                      # Page Object Model classes
│   ├── BasePage.cs            # Base page with common methods
│   ├── LoginPage.cs           # Login page interactions
│   ├── RegistrationPage.cs    # Registration page interactions
│   └── AccountsPage.cs        # Accounts page interactions
│
├── Tests/                      # Test classes
│   ├── LoginTests.cs          # Login test scenarios
│   ├── RegistrationTests.cs   # Registration test scenarios  
│   └── AccountsTests.cs       # Account functionality tests
│
├── Utils/                      # Utility classes
│   └── TestData.cs            # Centralized test data (hardcoded)
│
├── BaseTest.cs                # NUnit setup fixture (one-time setup/teardown)
├── TestBase.cs                # Base test class (setup/teardown per test)
├── Parabank.csproj            # Project configuration
├── Parabank.sln               # Solution file
│
├── .github/
│   └── workflows/
│       └── ci.yml            # GitHub Actions CI/CD pipeline
│
├── .gitignore                 # Git ignore rules
└── README.md                  # This file
```

## Page Object Model Pattern

Each page extends `BasePage`:

```csharp
public class LoginPage : BasePage
{
    public LoginPage(IPage page) : base(page) { }

    // Locators (selectors)
    private ILocator UsernameInput => Page.Locator("#username");
    private ILocator PasswordInput => Page.Locator("#password");
    private ILocator LoginButton => Page.Locator("button[type='submit']");

    // Methods
    public async Task NavigateToLogin()
    {
        await Page.GotoAsync("https://parabank.parasoft.com/");
        await WaitForLoadAsync();
    }

    public async Task LoginAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
        await WaitForLoadAsync();
    }
}
```

## Test Lifecycle

### BaseTest (One-time setup per test run)
```csharp
[SetUpFixture]
public class BaseTest
{
    [OneTimeSetUp]  // Runs once at the start
    public static async Task SetUp()
    
    [OneTimeTearDown]  // Runs once at the end
    public static async Task TearDown()
}
```

### TestBase (Per-test setup/teardown)
```csharp
public class TestBase
{
    [SetUp]  // Runs before each test
    public async Task TestSetUp()

    [TearDown]  // Runs after each test
    public async Task TestTearDown()
}
```

## Artifacts & Debugging

Tests automatically generate artifacts on **failure only**:

```
screenshots/    # Failed test screenshots (.png)
videos/         # Failed test videos (.webm)
traces/         # Playwright traces (.zip) for detailed debugging
```

**View Playwright trace:**
```bash
playwright show-trace traces/TestName.zip
```

## CI/CD Pipeline

GitHub Actions workflow automatically:
- ✅ Runs on every push and PR to `main`
- ✅ Uses .NET 7.0 SDK
- ✅ Installs Playwright browsers
- ✅ Runs all tests
- ✅ Uploads artifacts on failure
- ✅ Reports results

**View workflow:** `.github/workflows/ci.yml`

## Troubleshooting

| Issue | Solution |
|---|---|
| "dotnet not found" | Install .NET 7.0 SDK |
| "Playwright browsers not found" | Run `playwright install` |
| "Tests fail locally but pass in CI" | Verify .NET version matches |
| "Slow tests" | Check network speed, adjust `SlowMo` in `BaseTest.cs` |
| "Missing screenshots on failure" | Ensure `screenshots/` dir exists |
| "Permission denied on Linux" | Run with `sudo` or add user to `input` group |

## Common Commands

```bash
# Clean build artifacts
dotnet clean

# Rebuild from scratch
dotnet clean && dotnet build

# Run tests and generate coverage report
dotnet test /p:CollectCoverage=true

# Format code
dotnet format

# Check for security vulnerabilities
dotnet list package --vulnerable

# Update NuGet packages
dotnet package search Microsoft.Playwright
```

## Essential Files to Know

| File | Purpose |
|---|---|
| `Parabank.csproj` | Project dependencies and build config |
| `BaseTest.cs` | One-time browser setup |
| `TestBase.cs` | Per-test setup/teardown |
| `Utils/TestData.cs` | All test data (edit to change values) |
| `.gitignore` | Excludes `bin/`, `obj/`, artifacts |
| `.github/workflows/ci.yml` | Automated testing pipeline |

## Contributing

When adding new tests:
1. Create page object in `Pages/`
2. Add test class in `Tests/` extending `TestBase`
3. Use `TestData.cs` for test data
4. Keep tests independent and repeatable
5. Follow existing naming conventions

---