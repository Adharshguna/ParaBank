using Microsoft.Playwright;
using NUnit.Framework;

namespace Parabank;

[SetUpFixture]
public class BaseTest
{
    public static IPlaywright? Playwright { get; set; }
    public static IBrowser? Browser { get; set; }

    private static readonly string[] BrowserArgs = new[]
    {
        "--disable-blink-features=AutomationControlled",
        "--no-sandbox",
        "--disable-dev-shm-usage",
        "--disable-web-security"
    };

    [OneTimeSetUp]
    public static async Task SetUp()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            SlowMo = 80,
            Args = BrowserArgs
        });
    }

    [OneTimeTearDown]
    public static async Task TearDown()
    {
        if (Browser != null) await Browser.CloseAsync();
        if (Playwright != null) Playwright.Dispose();
    }
}
