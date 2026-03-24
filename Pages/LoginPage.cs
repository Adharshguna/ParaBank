using Microsoft.Playwright;

namespace Parabank.Pages;

public class LoginPage : BasePage
{
    public LoginPage(IPage page) : base(page) {}

    private ILocator UsernameInput => Page.Locator("#loginPanel input[name='username']");
    private ILocator PasswordInput => Page.Locator("#loginPanel input[name='password']");
    private ILocator LoginButton => Page.Locator("#loginPanel input[type='submit']");
    private ILocator RegisterLink => Page.Locator("a[href*='register']");
    private ILocator ErrorMessage => Page.Locator(".error");

    public async Task NavigateToLogin()
    {
        await Page.GotoAsync(DotNetEnv.Env.GetString("BASE_URL"));
        await WaitForLoadAsync();
    }

    public async Task LoginAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
        await WaitForLoadAsync();
    }

    public async Task GoToRegister()
    {
        await RegisterLink.ClickAsync();
        await WaitForLoadAsync();
    }

    public async Task<bool> IsLoginSuccessful()
    {
        return await Page.Locator("text=Welcome").IsVisibleAsync();
    }

    public async Task<string> GetErrorMessage()
    {
        return await ErrorMessage.TextContentAsync() ?? "";
    }
}