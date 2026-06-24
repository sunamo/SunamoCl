namespace SunamoCl;

public class ClNotify
{
    private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    public static async Task FlashConsoleTitle(string warningText = "!! Action required !!")
    {
        int delayMs = 1000;
        string originalTitle = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows) ? Console.Title : string.Empty;

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
