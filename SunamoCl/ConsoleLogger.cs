namespace SunamoCl;

// Does not inherit from LoggerBase — for that, use ConsoleLoggerCmd which was created to support CmdApp.SetLogger.
public class ConsoleLogger
{
    public static Func<string, string> InternationalizationFunction { get; set; } = null!;
    public static ConsoleLogger Instance { get; set; } = new();

    public ConsoleLogger()
    {
    }

    public static void WriteMessage(TypeOfMessageCl typeOfMessage, string text, params string[] args)
    {
        CL.ChangeColorOfConsoleAndWrite(typeOfMessage, text, args);
    }


}
