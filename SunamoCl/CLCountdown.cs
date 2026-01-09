// variables names: ok
namespace SunamoCl;

using Timer = System.Timers.Timer;

public partial class CL
{
    private static int timeLeft;
    private static string countdownMessage = string.Empty;

    /// <summary>
    /// Displays an appeal message with a countdown timer
    /// </summary>
    /// <param name="message">Message to display</param>
    /// <param name="seconds">Number of seconds for the countdown</param>
    public static void AppealWithCountdown(string message, int seconds)
    {
        timeLeft = seconds;
        countdownMessage = message;

        // EN: Display initial message with countdown
        // CZ: Zobrazit počáteční zprávu s odpočtem
        Console.Write($"{message} ({timeLeft}s)");

        Timer timer = new(1000);
        timer.Elapsed += WriteTimeLeft;
        timer.AutoReset = true;
        timer.Enabled = true;
        timer.Start();

        List<string> allEntries = Reader.ReadLine(seconds * 1000);
        timer.Stop();

        // EN: Clear the line and show completion
        // CZ: Vyčistit řádek a zobrazit dokončení
        try
        {
            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
        }
        catch (IOException)
        {
            Console.WriteLine(); // Just write newline if console is not available
        }
    }

    /// <summary>
    /// Event handler that updates the countdown display on each timer tick
    /// </summary>
    /// <param name="source">Timer source object</param>
    /// <param name="e">Event arguments</param>
    public static void WriteTimeLeft(object source, ElapsedEventArgs e)
    {
        // EN: Decrement time first
        // CZ: Nejprve snížit čas
        timeLeft -= 1;

        // EN: Stop timer when time runs out
        // CZ: Zastavit časovač když vyprší čas
        if (timeLeft < 0)
        {
            if (source is Timer timer)
            {
                timer.Stop();
            }
            return;
        }

        // EN: Update countdown on same line - clear and rewrite
        // CZ: Aktualizovat odpočet na stejném řádku - vymazat a přepsat
        try
        {
            Console.CursorVisible = false;
            Console.Write("\r" + new string(' ', Console.WindowWidth));
            Console.Write($"\r{countdownMessage} ({timeLeft}s)");
            Console.CursorVisible = true;
        }
        catch (IOException)
        {
            // If console is not available, just skip the update
        }
    }
}