// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoCl.SunamoCmdArgs_Cmd;

public class ParsedArgsStatic
{
    private static readonly ParsedArgs _parsedArgs = new();

    private static string Arg1
    {
        get => _parsedArgs.Arg1;
        set => _parsedArgs.Arg1 = value;
    }
}