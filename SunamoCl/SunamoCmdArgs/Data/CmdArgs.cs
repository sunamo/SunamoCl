namespace SunamoCl.SunamoCmdArgs.Data;

public class CmdArgs
{
    public static object Options { get; set; } = null!;

    public static Action<IEnumerable<Error>> ProcessArgsErrors { get; set; } = null!;

    public static T SaveArgsWorker<T>(string[] args)
    {
        if (ProcessArgsErrors == null) ThrowEx.IsNull("ProcessArgsErrors");

        var parseResult = Parser.Default.ParseArguments<T>(args);

        var result = parseResult.WithParsed(SaveArgs);
        result.WithNotParsed(ProcessArgsErrors);

        return (T)Options;
    }

    private static void SaveArgs<T>(T options)
    {
        Options = options!;
    }
}
