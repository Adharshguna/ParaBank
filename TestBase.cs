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

        try
        {
            if (isFailed && Page != null && !Page.IsClosed)
            {
                Directory.CreateDirectory("screenshots");
                Directory.CreateDirectory("videos");
                
                try
                {
                    await Page.ScreenshotAsync(new PageScreenshotOptions 
                    { 
                        Path = Path.Combine("screenshots", $"{testName}.png") 
                    });
                }
                catch { /* Screenshot failed, continue */ }
                
                try
                {
                    if (Page.Video != null)
                    {
                        await Page.Video.SaveAsAsync(Path.Combine("videos", $"{testName}.webm"));
                    }
                }
                catch { /* Video save failed, continue */ }
            }

            Directory.CreateDirectory("traces");
            
            // Stop tracing for this test
            try
            {
                await Page.Context.Tracing.StopAsync(new TracingStopOptions
                {
                    Path = Path.Combine("traces", $"{testName}.zip")
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