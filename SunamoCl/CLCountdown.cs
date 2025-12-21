namespace SunamoCl;

using Timer = System.Timers.Timer;

public partial class CL
{
    private static int _delay { get; set; }
    private static int _timeLeft { get; set; }
    private static string _countdownMessage { get; set; } = string.Empty;

    public static void AppealWithCountdown(string message, int seconds)
    {
        _delay = seconds;
        _timeLeft = seconds;
        _countdownMessage = message;

        // EN: Display initial message with countdown
        // CZ: Zobrazit počáteční zprávu s odpočtem
        Console.Write($"{message} ({_timeLeft}s)");

        Timer Timer = new(1000);
        Timer.Elapsed += WriteTimeLeft;
        Timer.AutoReset = true;
        Timer.Enabled = true;
        Timer.Start();

        List<string> allEntries = Reader.ReadLine(seconds * 1000);
        Timer.Stop();

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

    public static void WriteTimeLeft(object source, ElapsedEventArgs e)
    {
        // EN: Decrement time first
        // CZ: Nejprve snížit čas
        _timeLeft -= 1;

        // EN: Stop timer when time runs out
        // CZ: Zastavit časovač když vyprší čas
        if (_timeLeft < 0)
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
            Console.Write($"\r{_countdownMessage} ({_timeLeft}s)");
            Console.CursorVisible = true;
        }
        catch (IOException)
        {
            // If console is not available, just skip the update
        }
    }
}