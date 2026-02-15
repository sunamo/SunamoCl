namespace SunamoCl;

/// <summary>
///     Musí tu být se svými 3 řádky pro CmdApp.SetLogger
///     Udělat InitApp.Logger jako ILoggerBase nejde, protože ConsoleLogger ty metody nemá, ty má jen ConsoleLoggerCmd
///     protože ten je odvozený od LoggerBase
///     Musí být zde, protože bázová třída je taky internal
/// </summary>
public class ConsoleLoggerCmd : LoggerBaseCl //, ILoggerBase
{
    /// <summary>
    /// Gets the singleton instance of ConsoleLoggerCmd configured with CL.WriteLine
    /// </summary>
    public static ConsoleLoggerCmd Instance = new(CL.WriteLine);

    /// <summary>
    /// Initializes a new instance with the specified write delegate
    /// </summary>
    /// <param name="writeLineHandler">Delegate used to write formatted output lines</param>
    public ConsoleLoggerCmd(Action<string, string[]> writeLineHandler) : base(writeLineHandler)
    {
    }
}