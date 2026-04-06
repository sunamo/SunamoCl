namespace SunamoCl;

/// <summary>
/// Provides non-blocking console reading with a timeout mechanism using a background thread
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
    /// Reads console input for the specified duration and returns all lines entered
    /// </summary>
    /// <param name="timeOutMilliseconds">Duration in milliseconds to collect input</param>
    /// <returns>List of lines entered during the timeout period</returns>
    public static List<string> ReadLine(int timeOutMilliseconds)
    {
        userInput.Clear();
        isClosingLoop = false;

        Thread.Sleep(timeOutMilliseconds);
        isClosingLoop = true;

        return new List<string>(userInput);
    }
}