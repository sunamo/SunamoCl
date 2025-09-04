namespace SunamoCl;
public class ClNotify
{
    static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    public static async Task FlashConsoleTitle(ILogger logger, string warningText = "!! Action required !!")
    {
        int delayMs = 1000;
        string originalTitle = Console.Title;
        
        // Vytvoříme nový CancellationTokenSource pro každé volání
        cancellationTokenSource?.Cancel();
        cancellationTokenSource = new CancellationTokenSource();
        
        Task loopTask = RunInfiniteLoop(logger,
            cancellationTokenSource.Token, warningText, originalTitle, delayMs);

        await Task.Run(() => Console.ReadLine());

        // Po stisknutí Enter zrušíme úlohu a obnovíme původní title
        cancellationTokenSource.Cancel();
        
        // Počkáme na dokončení loop úlohy
        try
        {
            await loopTask;
        }
        catch (OperationCanceledException)
        {
            // Očekáváme, že úloha bude zrušena
        }
        
        Console.Title = originalTitle;
        logger.LogInformation("Console title restored after user pressed Enter");
    }

    static async Task RunInfiniteLoop(ILogger logger, CancellationToken cancellationToken, string warningText, string originalTitle, int delayMs = 1000)
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
                    // Zrušení během čekání na warningText - to je v pořádku
                    break;
                }

                Console.Title = originalTitle;
                
                try
                {
                    await Task.Delay(delayMs, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    // Zrušení během čekání na originalTitle - to je v pořádku
                    break;
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Očekáváme, že úloha bude zrušena
            logger.LogDebug("Console title flashing cancelled");
        }
        finally
        {
            // Zajistíme, že se title vždy obnoví
            Console.Title = originalTitle;
        }
        
        Console.WriteLine("✅ Notification loop terminated successfully.");
    }
}
