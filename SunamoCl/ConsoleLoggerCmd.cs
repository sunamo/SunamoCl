namespace SunamoCl;

// ConsoleLogger cannot be used as ILoggerBase because it lacks the required methods — only ConsoleLoggerCmd has them via LoggerBaseCl inheritance.
// Must reside here because the base class is also internal.
/// <summary>
/// Represents the console logger cmd type.
/// </summary>
public class ConsoleLoggerCmd : LoggerBaseCl //, ILoggerBase
{
    /// <summary>
    /// Gets or sets the instance.
    /// </summary>
    public static ConsoleLoggerCmd Instance { get; set; } = new(CL.WriteLine);

    /// <summary>
    /// Console logger cmd.
    /// </summary>
    /// <param name="CL.WriteLine">The CL write line.</param>
    /// <returns>The console logger cmd result.</returns>
    public ConsoleLoggerCmd(Action<string, string[]> writeLineHandler) : base(writeLineHandler)
    {
    }
}
