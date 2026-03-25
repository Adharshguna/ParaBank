using NUnit.Framework;
using Parabank.Pages;
using Parabank.Utils;

namespace Parabank.Tests;

[TestFixture]
public class RegistrationTests : TestBase
{
    [Test]
    public async Task ValidRegistration()
    {
        var loginPage = new LoginPage(Page);
        var registrationPage = new RegistrationPage(Page);

        await loginPage.NavigateToLogin();
        await loginPage.GoToRegister();
        await registrationPage.RegisterAsync(
            TestData.FirstName, TestData.LastName, TestData.Address, TestData.City,
            TestData.State, TestData.ZipCode, TestData.Phone, TestData.Ssn,
            TestData.NewUsername, TestData.NewPassword, TestData.NewPassword
        );

        Assert.IsTrue(await registrationPage.IsRegistrationSuccessful(), "Registration should be successful");
    }


    [Test]
    public async Task RegistrationWithMismatchedPasswords()
    {
        var loginPage = new LoginPage(Page);
        var registrationPage = new RegistrationPage(Page);

        await loginPage.NavigateToLogin();
        await loginPage.GoToRegister();
        await registrationPage.RegisterAsync(
            TestData.FirstName, TestData.LastName, TestData.Address, TestData.City,
            TestData.State, TestData.ZipCode, TestData.Phone, TestData.Ssn,
            TestData.NewUsername, TestData.NewPassword, "differentpassword"
        );

        Assert.IsFalse(await registrationPage.IsRegistrationSuccessful(), "Registration should fail with mismatched passwords");
    }

    [Test]
    public async Task RegistrationWithEmptyPassword()
    {
        var loginPage = new LoginPage(Page);
        var registrationPage = new RegistrationPage(Page);

        await loginPage.NavigateToLogin();
        await loginPage.GoToRegister();
        await registrationPage.RegisterAsync(
            TestData.FirstName, TestData.LastName, TestData.Address, TestData.City,
            TestData.State, TestData.ZipCode, TestData.Phone, TestData.Ssn,
            TestData.NewUsername, TestData.NewPassword, ""
        );

        Assert.IsFalse(await registrationPage.IsRegistrationSuccessful(), "Registration should fail with mismatched passwords");
    }
}
