// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoCl.SunamoCmdArgs.Data;

public class CmdArgs
{
    public static object Opts;

    /// <summary>
    ///     must be IEnumerable
    /// </summary>
    public static Action<IEnumerable<Error>> ProcessArgsErrors;

    private static Type _type = typeof(CmdArgs);

    /// <summary>
    ///     Into A1 insert CmdArgsEveryLine etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="args"></param>
    public static T SaveArgsWorker<T>(string[] args)
    {
        if (ProcessArgsErrors == null) ThrowEx.IsNull("ProcessArgsErrors");

        //PD.ShowMb("args" + args[0]);
        var rr = Parser.Default.ParseArguments<T>(args);

        var result = rr.WithParsed(SaveArgs);
        result.WithNotParsed(ProcessArgsErrors);

        return (T)Opts;
    }

    private static void SaveArgs<T>(T opts2) where T : notnull
    {
        // (T)
        Opts = opts2;
        //PD.ShowMb("NoTestForAlreadyRunning1 " + CmdArgsSelling.opts.NoTestForAlreadyRunning);

        //handle options
    }
}