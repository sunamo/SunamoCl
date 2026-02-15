namespace SunamoCl.SunamoCmd.Essential;

/// <summary>
/// Template logger implementation that writes messages to the console using ConsoleLogger
/// </summary>
public class ConsoleTemplateLogger : TemplateLoggerBaseCl
{
    /// <summary>
    /// Singleton instance of the ConsoleTemplateLogger
    /// </summary>
    public static ConsoleTemplateLogger Instance = new();

    private ConsoleTemplateLogger() : base(ConsoleLogger.WriteMessage)
    {
    }
}