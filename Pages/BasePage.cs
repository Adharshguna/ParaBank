using Microsoft.Playwright;

namespace Parabank.Pages;

public class BasePage
{
    protected IPage Page { get; }

    public BasePage(IPage page)
    {
        Page = page;
    }

    public async Task WaitForLoadAsync() => await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
}