namespace SunamoCl;

// Does not inherit from LoggerBase — for that, use ConsoleLoggerCmd which was created to support CmdApp.SetLogger.
/// <summary>
/// Represents the console logger type.
/// </summary>
public class ConsoleLogger
{
    /// <summary>
    /// Gets or sets the internationalization function.
    /// </summary>
    public static Func<string, string> InternationalizationFunction { get; set; } = null!;
    /// <summary>
    /// Gets or sets the instance.
    /// </summary>
    public static ConsoleLogger Instance { get; set; } = new();

    /// <summary>
    /// Console logger.
    /// </summary>
    /// <returns>The console logger result.</returns>
    public ConsoleLogger()
    {
    }

    /// <summary>
    /// Write message.
    /// </summary>
    /// <returns>The write message result.</returns>
    public static void WriteMessage(TypeOfMessageCl typeOfMessage, string text, params string[] args)
    {
        CL.ChangeColorOfConsoleAndWrite(typeOfMessage, text, args);
    }


}
