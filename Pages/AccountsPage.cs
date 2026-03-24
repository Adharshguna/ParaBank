using Microsoft.Playwright;

namespace Parabank.Pages;

public class AccountsPage : BasePage
{
    public AccountsPage(IPage page) : base(page) {}

    private ILocator AccountsTable => Page.Locator("#accountTable");
    private ILocator AccountLinks => Page.Locator("#accountTable tbody tr td a");

    public async Task<bool> IsAccountsVisible()
    {
        return await AccountsTable.IsVisibleAsync();
    }

    public async Task<int> GetAccountCount()
    {
        return await AccountLinks.CountAsync();
    }

    public async Task ClickFirstAccount()
    {
        await AccountLinks.First.ClickAsync();
        await WaitForLoadAsync();
    }
}