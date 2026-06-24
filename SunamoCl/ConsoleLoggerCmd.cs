namespace SunamoCl;

// ConsoleLogger cannot be used as ILoggerBase because it lacks the required methods — only ConsoleLoggerCmd has them via LoggerBaseCl inheritance.
// Must reside here because the base class is also internal.
public class ConsoleLoggerCmd : LoggerBaseCl //, ILoggerBase
{
    public static ConsoleLoggerCmd Instance { get; set; } = new(CL.WriteLine);

    public ConsoleLoggerCmd(Action<string, string[]> writeLineHandler) : base(writeLineHandler)
    {
    }
}
