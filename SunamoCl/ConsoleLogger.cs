namespace SunamoCl;

/// <summary>
///     Proč dědí z LoggerBase
///     Kdyby nedědil, můžu v pohodě přenést do cl z cmd
///     Odpoveď hledej v metodě CmdApp.SetLogger
///     Abych mohl i nadále používat SetLogger, vytvořil jsem ConsoleLoggerCmd
/// </summary>
public class ConsoleLogger
{
    private static Type consoleLoggerType = typeof(ConsoleLogger);
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
    public ConsoleLogger(/*Action<string, string[]> writeLineHandler*/) //: base(writeLineHandler)
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