namespace SunamoCmdArgs.Data;

public class CmdArgs
{
    public static object opts = null;
    /// <summary>
    /// must be IEnumerable
    /// </summary>
    public static Action<IEnumerable<Error>> ProcessArgsErrors;
    static Type type = typeof(CmdArgs);

    /// <summary>
    /// Into A1 insert CmdArgsEveryLine etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="args"></param>
    public static T SaveArgsWorker<T>(string[] args)
    {
        if (ProcessArgsErrors == null)
        {
            ThrowEx.IsNull("ProcessArgsErrors");
        }

        //PD.ShowMb("args" + args[0]);
        var rr = CommandLine.Parser.Default.ParseArguments<T>(args);

        var r = rr.WithParsed(SaveArgs);
        r.WithNotParsed(ProcessArgsErrors);

        return (T)opts;
    }

    static void SaveArgs<T>(T opts2)
    {
        // (T)
        opts = opts2;
        //PD.ShowMb("NoTestForAlreadyRunning1 " + CmdArgsSelling.opts.NoTestForAlreadyRunning);

        //handle options
    }


}
