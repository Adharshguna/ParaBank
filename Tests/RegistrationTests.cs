using NUnit.Framework;
using Parabank.Pages;
using Parabank.Utils;
using Microsoft.Playwright;
using System.IO;

namespace Parabank.Tests;

[TestFixture]
public class RegistrationTests : BaseTest
{
    private IPage _page;
    private LoginPage _loginPage;
    private RegistrationPage _registrationPage;

    [SetUp]
    public async Task SetUp()
    {
        _page = await Context!.NewContextAsync().NewPageAsync();
        _loginPage = new LoginPage(_page);
        _registrationPage = new RegistrationPage(_page);
        await _page.Context.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    [TearDown]
    public async Task TearDown()
    {
        var testName = TestContext.CurrentContext.Test.Name;
        var isFailed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed;
        if (isFailed)
        {
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = Path.Combine("screenshots", $"{testName}.png") });
            if (_page.Video != null)
            {
                await _page.Video.SaveAsAsync(Path.Combine("videos", $"{testName}.webm"));
            }
        }
        await _page.Context.Tracing.StopAsync(new TracingStopOptions
        {
            Path = Path.Combine("traces", $"{testName}.zip")
        });
        await _page.CloseAsync();
    }

    [Test]
    public async Task ValidRegistration()
    {
        await _loginPage.NavigateToLogin();
        await _loginPage.GoToRegister();
        await _registrationPage.RegisterAsync(
            TestData.FirstName, TestData.LastName, TestData.Address, TestData.City,
            TestData.State, TestData.ZipCode, TestData.Phone, TestData.Ssn,
            TestData.NewUsername, TestData.NewPassword, TestData.NewPassword
        );
        Assert.IsTrue(await _registrationPage.IsRegistrationSuccessful(), "Registration should be successful");
    }

    [Test]
    public async Task RegistrationWithExistingUsername()
    {
        await _loginPage.NavigateToLogin();
        await _loginPage.GoToRegister();
        await _registrationPage.RegisterAsync(
            TestData.FirstName, TestData.LastName, TestData.Address, TestData.City,
            TestData.State, TestData.ZipCode, TestData.Phone, TestData.Ssn,
            TestData.ValidUsername, TestData.NewPassword, TestData.NewPassword
        );
        Assert.IsFalse(await _registrationPage.IsRegistrationSuccessful(), "Registration should fail with existing username");
        StringAssert.Contains("exists", await _registrationPage.GetErrorMessage(), "Error message should indicate username exists");
    }

    [Test]
    public async Task RegistrationWithMismatchedPasswords()
    {
        await _loginPage.NavigateToLogin();
        await _loginPage.GoToRegister();
        await _registrationPage.RegisterAsync(
            TestData.FirstName, TestData.LastName, TestData.Address, TestData.City,
            TestData.State, TestData.ZipCode, TestData.Phone, TestData.Ssn,
            TestData.NewUsername, TestData.NewPassword, "differentpassword"
        );
        Assert.IsFalse(await _registrationPage.IsRegistrationSuccessful(), "Registration should fail with mismatched passwords");
    }
}