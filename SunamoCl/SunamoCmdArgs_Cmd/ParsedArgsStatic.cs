namespace SunamoCl.SunamoCmdArgs_Cmd;

/// <summary>
/// Represents the parsed args static type.
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
