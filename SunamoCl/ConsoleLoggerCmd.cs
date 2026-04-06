namespace SunamoCl;

/// <summary>
/// Logger derived from LoggerBaseCl for use with CmdApp.SetLogger.
/// ConsoleLogger cannot be used as ILoggerBase because it lacks the required methods — only ConsoleLoggerCmd has them via LoggerBaseCl inheritance.
/// Must reside here because the base class is also internal.
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