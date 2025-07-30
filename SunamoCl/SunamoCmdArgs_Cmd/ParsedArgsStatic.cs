namespace SunamoCl.SunamoCmdArgs_Cmd;

public class ParsedArgsStatic
{
    private static readonly ParsedArgs pa = new();

    private static string Arg1
    {
        get => pa.Arg1;
        set => pa.Arg1 = value;
    }
}