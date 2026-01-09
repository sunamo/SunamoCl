// variables names: ok
namespace SunamoCl;

public class Reader
{
    private static readonly Thread inputThread;
    private static readonly List<string> userInput = new();
    private static bool isClosingLoop;

    static Reader()
    {
        inputThread = new Thread(ReaderLoop);
        isClosingLoop = false;
        inputThread.IsBackground = true;
        inputThread.Start();
    }

    private static void ReaderLoop()
    {
        while (!isClosingLoop) userInput.Add(Console.ReadLine() ?? "");
    }

    public static List<string> ReadLine(int timeOutMilliseconds)
    {
        // EN: Clear previous input and reset loop flag
        // CZ: Vyčistit předchozí vstup a resetovat příznak smyčky
        userInput.Clear();
        isClosingLoop = false;

        Thread.Sleep(timeOutMilliseconds);
        isClosingLoop = true;

        // EN: Return a copy to avoid modification issues
        // CZ: Vrátit kopii aby se předešlo problémům s modifikací
        return new List<string>(userInput);
    }
}