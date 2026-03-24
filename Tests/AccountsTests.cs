using NUnit.Framework;
using Parabank.Pages;
using Parabank.Utils;
using Microsoft.Playwright;
using System.IO;

namespace Parabank.Tests;

[TestFixture]
public class AccountsTests : BaseTest
{
    private IPage _page;
    private LoginPage _loginPage;
    private AccountsPage _accountsPage;

    [SetUp]
    public async Task SetUp()
    {
        _page = await Context!.NewContextAsync().NewPageAsync();
        _loginPage = new LoginPage(_page);
        _accountsPage = new AccountsPage(_page);
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
    public async Task ViewAccounts()
    {
        await _loginPage.NavigateToLogin();
        await _loginPage.LoginAsync(TestData.ValidUsername, TestData.ValidPassword);
        Assert.IsTrue(await _loginPage.IsLoginSuccessful(), "Login should be successful");
        Assert.IsTrue(await _accountsPage.IsAccountsVisible(), "Accounts should be visible");
        Assert.Greater(await _accountsPage.GetAccountCount(), 0, "Should have at least one account");
    }
}