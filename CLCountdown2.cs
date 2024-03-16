using SunamoCl;

namespace SunamoCl;
public class CL2
{
    private static int delay { get; set; }
    private static int time_left { get; set; }

    public static void AppealWithCountdown(string message, int s)
    {
        delay = s;
        time_left = s;

        List<string> allEntries = new();
        Console.WriteLine(message);
        Console.SetCursorPosition(0, 2);
        Timer();
        allEntries = Reader.ReadLine((s + 1) * 1000);

        for (var i = 0; i < allEntries.Count; i++) Console.WriteLine(allEntries[i]);
        Console.Read();
    }

    public static void Timer()
    {
        for (var i = 11; i > 0; i--)
        {
            var t = Task.Delay(i * 1000).ContinueWith(_ => WriteTimeLeft());
        }
    }

    public static void WriteTimeLeft()
    {
        var currentLineCursorTop = Console.CursorTop;
        var currentLineCursorLeft = Console.CursorLeft;
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 1);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, 1);
        Console.Write(time_left);
        Console.SetCursorPosition(currentLineCursorLeft, currentLineCursorTop);
        Console.CursorVisible = true;
        time_left -= 1;
    }
}
