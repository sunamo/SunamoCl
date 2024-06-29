namespace SunamoCl;


public class ConsoleTemplateLogger : TemplateLoggerBaseCl
{
    public static ConsoleTemplateLogger Instance = new ConsoleTemplateLogger();

    private ConsoleTemplateLogger() : base(ConsoleLogger.WriteMessage)
    {

    }
}
