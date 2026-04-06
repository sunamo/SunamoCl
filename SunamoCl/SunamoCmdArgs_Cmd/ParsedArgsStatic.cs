namespace SunamoCl.SunamoCmdArgs_Cmd;

/// <summary>
/// Provides static access to parsed command-line arguments
/// </summary>
public class ParsedArgsStatic
{
    private static readonly ParsedArgs parsedArgs = new();

    private static string? Arg1
    {
        get => parsedArgs.Arg1;
        set => parsedArgs.Arg1 = value;
    }
}