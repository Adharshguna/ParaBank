using Microsoft.Playwright;
using NUnit.Framework;

namespace Parabank;

public class TestBase
{
    protected IPage Page { get; private set; } = null!;

    [SetUp]
    public async Task TestSetUp()
    {
        Page = await BaseTest.Browser!.NewPageAsync();
        
        // Start tracing for this specific test
        await Page.Context.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    [TearDown]
    public async Task TestTearDown()
    {
        var testName = TestContext.CurrentContext.Test.Name;
        var isFailed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed;

        // Get project root directory (3 levels up from bin)
        var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));

        try
        {
            if (isFailed && Page != null && !Page.IsClosed)
            {
                var screenshotsDir = Path.Combine(projectRoot, "screenshots");
                var videosDir = Path.Combine(projectRoot, "videos");
                Directory.CreateDirectory(screenshotsDir);
                Directory.CreateDirectory(videosDir);
                
                try
                {
                    await Page.ScreenshotAsync(new PageScreenshotOptions 
                    { 
                        Path = Path.Combine(screenshotsDir, $"{testName}.png") 
                    });
                }
                catch { /* Screenshot failed, continue */ }
                
                try
                {
                    if (Page.Video != null)
                    {
                        await Page.Video.SaveAsAsync(Path.Combine(videosDir, $"{testName}.webm"));
                    }
                }
                catch { /* Video save failed, continue */ }
            }

            var tracesDir = Path.Combine(projectRoot, "traces");
            Directory.CreateDirectory(tracesDir);
            
            // Stop tracing for this test (always, not just on failure)
            try
            {
                await Page.Context.Tracing.StopAsync(new TracingStopOptions
                {
                    Path = Path.Combine(tracesDir, $"{testName}.zip")
                });
            }
            catch { /* Tracing stop failed, continue */ }
        }
        finally
        {
            if (Page != null && !Page.IsClosed)
            {
                await Page.CloseAsync();
            }
        }
    }
}