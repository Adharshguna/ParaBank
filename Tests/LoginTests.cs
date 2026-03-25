using NUnit.Framework;
using Parabank.Pages;
using Parabank.Utils;
using Microsoft.Playwright;
using System.IO;

namespace Parabank.Tests;

[TestFixture]
public class LoginTests : TestBase
{
    [Test]
    public async Task ValidLogin()
    {
        var loginPage = new LoginPage(Page);
        await loginPage.NavigateToLogin();
        await loginPage.LoginAsync(TestData.ValidUsername, TestData.ValidPassword);
        Assert.IsTrue(await loginPage.IsLoginSuccessful(), "Login should be successful");
    }

    [Test]
    public async Task InvalidLogin()
    {
        var loginPage = new LoginPage(Page);
        await loginPage.NavigateToLogin();
        await loginPage.LoginAsync(TestData.InvalidUsername, TestData.InvalidPassword);
        Assert.IsFalse(await loginPage.IsLoginSuccessful(), "Login should fail");
        StringAssert.Contains("Error!", await loginPage.GetErrorMessage(), "Error message should be displayed");
    }

    [Test]
    public async Task EmptyUsername()
    {
        var loginPage = new LoginPage(Page);
        await loginPage.NavigateToLogin();
        await loginPage.LoginAsync("", TestData.ValidPassword);
        Assert.IsFalse(await loginPage.IsLoginSuccessful(), "Login should fail with empty username");
    }

    [Test]
    public async Task EmptyPassword()
    {
        var loginPage = new LoginPage(Page);
        await loginPage.NavigateToLogin();
        await loginPage.LoginAsync(TestData.ValidUsername, "");
        Assert.IsFalse(await loginPage.IsLoginSuccessful(), "Login should fail with empty password");
    }
}