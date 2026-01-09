// variables names: ok
namespace SunamoCl.SunamoCmdArgs.Data;

public class CmdArgs
{
    public static object Opts { get; set; } = null!;

    /// <summary>
    ///     must be IEnumerable
    /// </summary>
    public static Action<IEnumerable<Error>> ProcessArgsErrors { get; set; } = null!;

    /// <summary>
    ///     Into A1 insert CmdArgsEveryLine etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="args"></param>
    public static T SaveArgsWorker<T>(string[] args)
    {
        if (ProcessArgsErrors == null) ThrowEx.IsNull("ProcessArgsErrors");

        var rr = Parser.Default.ParseArguments<T>(args);

        var result = rr.WithParsed(SaveArgs);
        result.WithNotParsed(ProcessArgsErrors);

        return (T)Opts;
    }

    private static void SaveArgs<T>(T opts2) where T : notnull
    {
        Opts = opts2;
    }
}