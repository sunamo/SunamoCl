namespace SunamoCl.SunamoCmd.Essential;

public class ConsoleTemplateLogger : TemplateLoggerBaseCl
{
    public static ConsoleTemplateLogger Instance { get; set; } = new();

    private ConsoleTemplateLogger() : base(ConsoleLogger.WriteMessage)
    {
    }
}
