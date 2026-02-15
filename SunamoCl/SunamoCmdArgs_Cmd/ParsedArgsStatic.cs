namespace SunamoCl.SunamoCmdArgs_Cmd;

/// <summary>
/// Provides static access to parsed command-line arguments
/// </summary>
public class ParsedArgsStatic
{
    private static readonly ParsedArgs _parsedArgs = new();

    private static string? Arg1
    {
        get => _parsedArgs.Arg1;
        set => _parsedArgs.Arg1 = value;
    }
}