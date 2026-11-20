namespace SunamoCl.SunamoCmd.Essential;

/// <summary>
/// Represents the console template logger type.
/// </summary>
public class ConsoleTemplateLogger : TemplateLoggerBaseCl
{
    /// <summary>
    /// Gets or sets the instance.
    /// </summary>
    public static ConsoleTemplateLogger Instance { get; set; } = new();

    private ConsoleTemplateLogger() : base(ConsoleLogger.WriteMessage)
    {
    }
}
