namespace SunamoCl;
using Timer = System.Timers.Timer;

public partial class CL
{
    private static int delay { get; set; }
    private static int time_left { get; set; }

    public static void AppealWithCountdown(string message, int s)
    {
        delay = s;
        time_left = s;

        List<string> allEntries = new();
        Console.WriteLine($"⏱️  {message}");
        Console.SetCursorPosition(0, 2);

        Timer Timer = new(1000);
        Timer.Elapsed += WriteTimeLeft;
        Timer.AutoReset = true;
        Timer.Enabled = true;
        Timer.Start();

        allEntries = Reader.ReadLine(s * 1000);
        Timer.Stop();

        Console.WriteLine();
        Console.WriteLine("╔═══════════════════════════════════════════════╗");
        Console.WriteLine("║  📝 Entries received:                       ║");
        Console.WriteLine("╠═══════════════════════════════════════════════╣");
        for (var i = 0; i < allEntries.Count; i++) 
            Console.WriteLine($"║  {(i + 1):D2}. {allEntries[i].PadRight(40)} ║");
        Console.WriteLine("╚═══════════════════════════════════════════════╝");
        Console.Read();
    }

    public static void WriteTimeLeft(object source, ElapsedEventArgs e)
    {
        var currentLineCursorTop = Console.CursorTop;
        var currentLineCursorLeft = Console.CursorLeft;
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 1);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, 1);
        Console.Write($"⏰ Time remaining: {time_left}s");
        Console.SetCursorPosition(currentLineCursorLeft, currentLineCursorTop);
        Console.CursorVisible = true;
        time_left -= 1;
    }
}