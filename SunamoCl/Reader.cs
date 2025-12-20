// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoCl;

public class Reader
{
    private static readonly Thread _inputThread;
    private static readonly List<string> _userInput = new();
    private static bool _closeLoop;

    static Reader()
    {
        _inputThread = new Thread(ReaderLoop);
        _closeLoop = false;
        _inputThread.IsBackground = true;
        _inputThread.Start();
    }

    private static void ReaderLoop()
    {
        while (!_closeLoop) _userInput.Add(Console.ReadLine() ?? "");
    }

    public static List<string> ReadLine(int timeOutMilliseconds)
    {
        // EN: Clear previous input and reset loop flag
        // CZ: Vyčistit předchozí vstup a resetovat příznak smyčky
        _userInput.Clear();
        _closeLoop = false;

        Thread.Sleep(timeOutMilliseconds);
        _closeLoop = true;

        // EN: Return a copy to avoid modification issues
        // CZ: Vrátit kopii aby se předešlo problémům s modifikací
        return new List<string>(_userInput);
    }
}