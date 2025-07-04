namespace SunamoCl;
public class ClNotify
{
    static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    public static async Task FlashConsoleTitle(ILogger logger, string warningText = "!! Action required !!")
    {
        int delayMs = 1000;
        string originalTitle = Console.Title;
        Task loopTask = RunInfiniteLoop(logger,
            cancellationTokenSource.Token, warningText, originalTitle, delayMs);

        await Task.Run(() => Console.ReadLine());

        Console.Title = originalTitle;
    }

    static async Task RunInfiniteLoop(ILogger logger, CancellationToken cancellationToken, string warningText, string originalTitle, int delayMs = 1000)
    {
        Console.Beep();

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                Console.Title = warningText;
                await Task.Delay(delayMs);

                Console.Title = originalTitle;
                await Task.Delay(delayMs);
            }
            catch (OperationCanceledException ex)
            {
                logger.LogWarning("RunInfiniteLoop: " + ex.Message);
            }
        }
        Console.WriteLine("Nekonečná smyčka se úspěšně ukončila.");
    }
}
