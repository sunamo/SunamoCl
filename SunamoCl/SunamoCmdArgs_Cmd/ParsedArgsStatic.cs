namespace SunamoCl.SunamoCmdArgs_Cmd;

public class ParsedArgsStatic
{
    private static readonly ParsedArgs parsedArgs = new();

    private static string? Arg1
    {
        get => parsedArgs.Arg1;
        set => parsedArgs.Arg1 = value;
    }
}
