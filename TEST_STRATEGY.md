# ParaBank UI Automation - Test Strategy Document

**Version:** 1.0  
**Created:** March 25, 2026  
**Project:** ParaBank  
**Framework:** .NET 7.0 + Playwright + NUnit  

---

## 1. What We Do

We automate UI testing for the ParaBank banking application using a **Page Object Model (POM)** pattern. Tests verify core user workflows:

- **Login Tests:** User authentication and session management
- **Registration Tests:** New account creation and form validation

Tests execute in **Chromium browser** written in **C# using NUnit**.

---

## 2. How Tests Work

### 2.1 Test Execution Flow

```
1. Browser launches (one-time per test run)
2. Test creates a page instance
3. Test executes UI interactions:
   - Navigate to page
   - Fill form fields
   - Click buttons
   - Assert expected results
4. Capture artifacts if test fails (screenshot, video, trace)
5. Page closes (cleanup)
6. Next test starts
```

### 2.2 Page Object Model Pattern

**Why POM:**
- Locators centralized in page classes (not scattered in tests)
- Easy to update when UI changes
- Clean, readable test code
- Low maintenance

**Structure:**
```
BasePage (common methods: click, fill, wait)
├── LoginPage (login interactions)
├── RegistrationPage (registration interactions)
         ↓
    Tests (test scenarios)
```

---

## 3. Current Test Scenarios

| Feature | Test File | Tests | Status |
|---------|-----------|-------|--------|
| **Login** | `LoginTests.cs` | Valid login, Invalid credentials, Navigation | ✅ Active |
| **Registration** | `RegistrationTests.cs` | Successful registration, Form submission | ✅ Active |

**Total:** 8 active tests across 3 test classes

---

## 4. Tools and Technology

### 4.1 Technology Stack

| Tool | Version | Why |
|------|---------|-----|
| **.NET** | 7.0 | Modern runtime; cross-platform; strong typing catches errors early |
| **Playwright** | 1.40.0 | Modern browser automation; built-in auto-wait reduces flakiness |
| **NUnit** | 3.13.3 | Standard .NET test framework; simple syntax; good GitHub integration |
| **Chromium** | Latest | Fast, lightweight, headless-capable, open-source |
| **GitHub Actions** | Native | CI/CD built into GitHub; free for public repos; automatic on push/PR |

### 4.2 Browser Configuration

```csharp
new BrowserTypeLaunchOptions
{
    Headless = false,           // See browser during local testing
    SlowMo = 80,                // 80ms delay prevents flakiness
    Args = new[] {              // Prevent bot detection
        "--disable-blink-features=AutomationControlled",
        "--start-maximized"
    }
}
```

**Why these settings:**
- **Headless = false:** Visual debugging; see what's happening locally
- **SlowMo = 80ms:** Page gets time to respond; reduces flaky tests
- **Automation Detection Disabled:** Prevents bot-detection on secure sites

### 4.3 Playwright Auto-Wait

Playwright **automatically waits** for elements before interacting:
```csharp
await UsernameInput.ClickAsync();  // Waits for element visible & enabled
await PasswordInput.FillAsync("password");  // Waits for input ready
```

**Why:** Eliminates manual waits and timing issues; more reliable

---

## 5. Test Data

- **Source:** `Utils/TestData.cs`
- **Why:** 'Password' encription
- **Data:**
  ```csharp
  BaseUrl = "https://parabank.parasoft.com/"
  Username = "parabank"
  Password = "DFNLKDNF92273Y@"
  FirstName = "TestUser"
  LastName = "QA"
  Address = "123 Test Street"
  ```

---

## 6. How Reports Are Generated

### 6.1 Test Execution Report

When you run `dotnet test`:

```
Test Summary:
  Total Tests: 8
  Passed: 8
  Failed: 0
  Duration: 42 seconds
```

### 6.2 Failure Artifacts (Automatic)

When a test fails, three artifacts are captured:

| Artifact | Location | Purpose |
|----------|----------|---------|
| **Screenshot** | `bin/Debug/net7.0/screenshots/` | Visual snapshot of failure |
| **Video** | `bin/Debug/net7.0/videos/` | Full test execution recording |
| **Trace** | `bin/Debug/net7.0/traces/` | Detailed Playwright action log |

**Why these artifacts:**
- **Screenshots:** Quickly see what went wrong visually
- **Videos:** Replay exact sequence of actions that caused failure
- **Traces:** Debug with Playwright Inspector; inspect DOM, network, logs
---

## 7. How to Run Tests Locally

```bash
# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "FullyQualifiedName~LoginTests"

# Run with detailed output
dotnet test --verbosity detailed
```

**Performance:** ~30-40 seconds for full suite

---

## 8. Project Structure

```
ParaBank/
├── Pages/
│   ├── BasePage.cs              ← Common methods (wait, click, fill)
│   ├── LoginPage.cs             ← Login interactions
│   ├── RegistrationPage.cs      ← Registration interactions
│ 
├── Tests/
│   ├── LoginTests.cs            ← Login test scenarios
│   ├── RegistrationTests.cs     ← Registration test scenarios
│   
├── Utils/
│   └── TestData.cs              ← Centralized test data
├── BaseTest.cs                  ← One-time browser setup
├── TestBase.cs                  ← Per-test setup/teardown
└── Parabank.csproj              ← Dependencies & config
```

---

## 9. Page Object Model Code Example

**BasePage (Common Methods):**
```csharp
public class BasePage
{
    protected IPage Page { get; set; }
    
    public async Task ClickAsync(ILocator locator)
        => await locator.ClickAsync();
    
    public async Task FillAsync(ILocator locator, string value)
        => await locator.FillAsync(value);
}
```

**LoginPage (Specific):**
```csharp
public class LoginPage : BasePage
{
    private ILocator UsernameInput => Page.Locator("input[name='username']");
    private ILocator LoginButton => Page.Locator("input[value='Log In']");
    
    public async Task LoginAsync(string username, string password)
    {
        await FillAsync(UsernameInput, username);
        await ClickAsync(LoginButton);
    }
}
```

**Test (Clean):**
```csharp
[TestFixture]
public class LoginTests : TestBase
{
    [Test]
    public async Task ValidLoginTest()
    {
        var loginPage = new LoginPage { Page = Page };
        await loginPage.LoginAsync(TestData.Username, TestData.ValidPassword);
        Assert.That(await loginPage.IsLoginSuccessful(), Is.True);
    }
}
```

**Why POM:**
- Locators centralized in page classes
- Easy to update when UI changes
- Tests are clean and readable
- Low maintenance

---

## 10. Performance

| Phase | Time |
|-------|------|
| Browser Launch | ~3s (one-time) |
| Per Test | 2-5s |
| Full Suite (8 tests) | ~30-40s |

---

## 12. Summary

| Aspect | Implementation |
|--------|-----------------|
| **What** | Automated UI tests for login, registration |
| **How** | Page Object Model using Playwright + NUnit |
| **Tools** | .NET 7.0, Playwright 1.40.0, NUnit 3.13.3, Chromium, GitHub Actions |
| **Reports** | Console output + screenshots/videos/traces on failure |
| **Data** | `TestData.cs` |
| **Speed** | ~40s for full suite |

---

**End of Document**