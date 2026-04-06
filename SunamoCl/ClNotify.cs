namespace SunamoCl;

/// <summary>
/// Provides console notification functionality by flashing the console title to attract user attention
/// </summary>
public class ClNotify
{
    private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    /// <summary>
    /// Flashes the console title between the original title and a warning text until user presses Enter
    /// </summary>
    /// <param name="warningText">Warning text to alternate with in the console title</param>
    public static async Task FlashConsoleTitle(string warningText = "!! Action required !!")
    {
        int delayMs = 1000;
        string originalTitle = OperatingSystem.IsWindows() ? Console.Title : string.Empty;

        // Create a new CancellationTokenSource for each call
        cancellationTokenSource?.Cancel();
        cancellationTokenSource = new CancellationTokenSource();

        Task loopTask = RunInfiniteLoop(
            cancellationTokenSource.Token, warningText, originalTitle, delayMs);

        await Task.Run(() => Console.ReadLine());

        // After pressing Enter, cancel the task and restore the original title
        cancellationTokenSource.Cancel();
        
        // Wait for the loop task to complete
        try
        {
            await loopTask;
        }
        catch (OperationCanceledException)
        {
            // Expected cancellation
        }
        
        Console.Title = originalTitle;
    }

    /// <summary>
    /// Runs an infinite loop alternating the console title between warning text and original title.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the loop.</param>
    /// <param name="warningText">Warning text to display in title.</param>
    /// <param name="originalTitle">Original console title to restore.</param>
    /// <param name="delayMs">Delay between title switches in milliseconds.</param>
    static async Task RunInfiniteLoop(CancellationToken cancellationToken, string warningText, string originalTitle, int delayMs = 1000)
    {
        Console.Beep();

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.Title = warningText;
                
                try
                {
                    await Task.Delay(delayMs, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    // Cancellation during warningText delay - expected
                    break;
                }

                Console.Title = originalTitle;
                
                try
                {
                    await Task.Delay(delayMs, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    // Cancellation during originalTitle delay - expected
                    break;
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Expected cancellation
        }
        finally
        {
            // Ensure the title is always restored
            Console.Title = originalTitle;
        }
        
        Console.WriteLine("✅ Notification loop terminated successfully.");
    }
}