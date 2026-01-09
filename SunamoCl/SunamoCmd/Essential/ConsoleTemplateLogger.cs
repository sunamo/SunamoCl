// variables names: ok
namespace SunamoCl.SunamoCmd.Essential;

public class ConsoleTemplateLogger : TemplateLoggerBaseCl
{
    public static ConsoleTemplateLogger Instance = new();

    private ConsoleTemplateLogger() : base(ConsoleLogger.WriteMessage)
    {
    }
}