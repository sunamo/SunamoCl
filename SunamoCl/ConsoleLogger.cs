namespace SunamoCl;

/// <summary>
///     Proč dědí z LoggerBase
///     Kdyby nedědil, můžu v pohodě přenést do cl z cmd
///     Odpoveď hledej v metodě CmdApp.SetLogger
///     Abych mohl i nadále používat SetLogger, vytvořil jsem ConsoleLoggerCmd
/// </summary>
public class ConsoleLogger
{
    private static Type _consoleLoggerType = typeof(ConsoleLogger);
    public static Func<string, string> InternationalizationFunction;
    public static ConsoleLogger Instance = new();

    public ConsoleLogger(/*Action<string, string[]> writeLineHandler*/) //: base(writeLineHandler)
    {
    }

    public static void WriteMessage(TypeOfMessageCl typeOfMessage, string text, params string[] args)
    {
        CL.ChangeColorOfConsoleAndWrite(typeOfMessage, text, args);
    }

    #region Change color of Console

    #endregion
}