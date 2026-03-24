using NUnit.Framework;
using Parabank.Pages;
using Parabank.Utils;
using Microsoft.Playwright;
using System.IO;

namespace Parabank.Tests;

[TestFixture]
public class LoginTests : BaseTest
{
    private IPage _page;
    private LoginPage _loginPage;

    [SetUp]
    public async Task SetUp()
    {
        _page = await Context!.NewContextAsync().NewPageAsync();
        _loginPage = new LoginPage(_page);
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
    public async Task ValidLogin()
    {
        await _loginPage.NavigateToLogin();
        await _loginPage.LoginAsync(TestData.ValidUsername, TestData.ValidPassword);
        Assert.IsTrue(await _loginPage.IsLoginSuccessful(), "Login should be successful");
    }

    [Test]
    public async Task InvalidLogin()
    {
        await _loginPage.NavigateToLogin();
        await _loginPage.LoginAsync(TestData.InvalidUsername, TestData.InvalidPassword);
        Assert.IsFalse(await _loginPage.IsLoginSuccessful(), "Login should fail");
        StringAssert.Contains("error", await _loginPage.GetErrorMessage(), "Error message should be displayed");
    }

    [Test]
    public async Task EmptyUsername()
    {
        await _loginPage.NavigateToLogin();
        await _loginPage.LoginAsync("", TestData.ValidPassword);
        Assert.IsFalse(await _loginPage.IsLoginSuccessful(), "Login should fail with empty username");
    }

    [Test]
    public async Task EmptyPassword()
    {
        await _loginPage.NavigateToLogin();
        await _loginPage.LoginAsync(TestData.ValidUsername, "");
        Assert.IsFalse(await _loginPage.IsLoginSuccessful(), "Login should fail with empty password");
    }
}