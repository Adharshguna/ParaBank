using Microsoft.Playwright;
using NUnit.Framework;

namespace Parabank;

[SetUpFixture]
public class BaseTest
{
    public static IPlaywright? Playwright { get; private set; }
    public static IBrowser? Browser { get; private set; }
    public static IBrowserContext? Context { get; private set; }

    [OneTimeSetUp]
    public static async Task SetUp()
    {
        DotNetEnv.Env.Load();
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = bool.Parse(Environment.GetEnvironmentVariable("HEADLESS") ?? "false"),
            SlowMo = 50,
        });
        Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            RecordVideoDir = "videos/",
            RecordVideoSize = new RecordVideoSize() { Width = 1280, Height = 720 }
        });
    }

    [OneTimeTearDown]
    public static async Task TearDown()
    {
        if (Context != null) await Context.CloseAsync();
        if (Browser != null) await Browser.CloseAsync();
        if (Playwright != null) await Playwright.DisposeAsync();
    }
}
