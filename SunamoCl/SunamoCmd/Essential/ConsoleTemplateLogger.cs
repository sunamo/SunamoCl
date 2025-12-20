// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoCl.SunamoCmd.Essential;

public class ConsoleTemplateLogger : TemplateLoggerBaseCl
{
    public static ConsoleTemplateLogger Instance = new();

    private ConsoleTemplateLogger() : base(ConsoleLogger.WriteMessage)
    {
    }
}