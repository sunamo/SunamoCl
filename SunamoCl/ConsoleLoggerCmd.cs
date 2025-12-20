// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoCl;

/// <summary>
///     Musí tu být se svými 3 řádky pro CmdApp.SetLogger
///     Udělat InitApp.Logger jako ILoggerBase nejde, protože ConsoleLogger ty metody nemá, ty má jen ConsoleLoggerCmd
///     protože ten je odvozený od LoggerBase
///     Musí být zde, protože bázová třída je taky internal
/// </summary>
public class ConsoleLoggerCmd : LoggerBaseCl //, ILoggerBase
{
    public static ConsoleLoggerCmd Instance = new(CL.WriteLine);

    public ConsoleLoggerCmd(Action<string, string[]> writeLineHandler) : base(writeLineHandler)
    {
    }
}