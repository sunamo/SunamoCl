namespace SunamoCl;
public class Reader
{
    private static readonly Thread inputThread;
    private static readonly List<string> userInput = new();
    private static bool closeLoop;

    static Reader()
    {
        inputThread = new Thread(reader);
        closeLoop = false;
        inputThread.IsBackground = true;
        inputThread.Start();
    }

    private static void reader()
    {
        while (!closeLoop) userInput.Add(Console.ReadLine());
    }

    public static List<string> ReadLine(int timeOutMilliseconds)
    {
        Thread.Sleep(timeOutMilliseconds);
        closeLoop = true;
        return userInput;
    }
}
