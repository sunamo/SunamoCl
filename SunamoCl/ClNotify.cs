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

        // Vytvoříme nový CancellationTokenSource pro každé volání
        cancellationTokenSource?.Cancel();
        cancellationTokenSource = new CancellationTokenSource();

        Task loopTask = RunInfiniteLoop(
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
            // Logovalo se zde přes logger.LogDebug("Console title flashing cancelled"); ale je to podle mě nesmysl.
            // Pokud bych zde potřeboval logovat, tak příště bez logger
        }
        finally
        {
            // Zajistíme, že se title vždy obnoví
            Console.Title = originalTitle;
        }
        
        Console.WriteLine("✅ Notification loop terminated successfully.");
    }
}