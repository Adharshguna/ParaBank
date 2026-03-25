using Microsoft.Playwright;

namespace Parabank.Pages;

public class RegistrationPage : BasePage
{
    public RegistrationPage(IPage page) : base(page) {}

    private ILocator FirstNameInput => Page.Locator("#customer\\.firstName");
    private ILocator LastNameInput => Page.Locator("#customer\\.lastName");
    private ILocator AddressInput => Page.Locator("#customer\\.address\\.street");
    private ILocator CityInput => Page.Locator("#customer\\.address\\.city");
    private ILocator StateInput => Page.Locator("#customer\\.address\\.state");
    private ILocator ZipCodeInput => Page.Locator("#customer\\.address\\.zipCode");
    private ILocator PhoneInput => Page.Locator("#customer\\.phoneNumber");
    private ILocator SsnInput => Page.Locator("#customer\\.ssn");
    private ILocator UsernameInput => Page.Locator("#customer\\.username");
    private ILocator PasswordInput => Page.Locator("#customer\\.password");
    private ILocator ConfirmPasswordInput => Page.Locator("#repeatedPassword");
    private ILocator RegisterButton => Page.Locator("input[type='submit'][value='Register']");
    private ILocator SuccessMessage => Page.Locator(".title");
    private ILocator ErrorMessage => Page.Locator(".error");

    public async Task RegisterAsync(string firstName, string lastName, string address, string city, string state, string zipCode, string phone, string ssn, string username, string password, string confirmPassword)
    {
        await FirstNameInput.FillAsync(firstName);
        await LastNameInput.FillAsync(lastName);
        await AddressInput.FillAsync(address);
        await CityInput.FillAsync(city);
        await StateInput.FillAsync(state);
        await ZipCodeInput.FillAsync(zipCode);
        await PhoneInput.FillAsync(phone);
        await SsnInput.FillAsync(ssn);
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await ConfirmPasswordInput.FillAsync(confirmPassword);
        await Task.Delay(500);
        await RegisterButton.ClickAsync();
        await WaitForLoadAsync();
    }

    public async Task<bool> IsRegistrationSuccessful()
    {
        var message = await SuccessMessage.TextContentAsync();
        return message?.StartsWith("Welcome") == true;
    }

    public async Task<string> GetErrorMessage()
    {
        return await ErrorMessage.TextContentAsync() ?? "";
    }
}