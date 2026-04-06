namespace SunamoCl;

/// <summary>
/// Provides static console logging with internationalization support.
/// Does not inherit from LoggerBase — for that, use ConsoleLoggerCmd which was created to support CmdApp.SetLogger.
/// </summary>
public class ConsoleLogger
{
    /// <summary>
    /// Gets or sets the function used for internationalizing log messages
    /// </summary>
    public static Func<string, string> InternationalizationFunction = null!;
    /// <summary>
    /// Gets the singleton instance of ConsoleLogger
    /// </summary>
    public static ConsoleLogger Instance = new();

    /// <summary>
    /// Initializes a new instance of ConsoleLogger
    /// </summary>
    public ConsoleLogger()
    {
    }

    /// <summary>
    /// Writes a typed message to the console with appropriate coloring
    /// </summary>
    /// <param name="typeOfMessage">Type of message determining the console color</param>
    /// <param name="text">Message text to write</param>
    /// <param name="args">Format arguments for the text</param>
    public static void WriteMessage(TypeOfMessageCl typeOfMessage, string text, params string[] args)
    {
        CL.ChangeColorOfConsoleAndWrite(typeOfMessage, text, args);
    }


}