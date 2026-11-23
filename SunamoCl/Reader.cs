namespace SunamoCl;

/// <summary>
/// Represents the reader type.
/// </summary>
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

    /// <summary>
    /// Read line.
    /// </summary>
    /// <param name="timeOutMilliseconds">The time out milliseconds.</param>
    /// <returns>The read line result.</returns>
    public static List<string> ReadLine(int timeOutMilliseconds)
    {
        userInput.Clear();
        isClosingLoop = false;

        Thread.Sleep(timeOutMilliseconds);
        isClosingLoop = true;

        return new List<string>(userInput);
    }
}
