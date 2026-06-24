namespace SunamoCl.SunamoCmdArgs_Cmd;

// For apps that did not use Mode, ModeCl can be used. Mode is no longer used for new applications.
public class ProgramCommon
{
    private void ProcessArgsErrors(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            Console.WriteLine($"Argument parsing error: {error}");
        }
    }

    public Tuple<T, Mode>? ProcessArgs<T, Mode>(string[] args, Mode defaultMode)
        where T : CommonArgs
        where Mode : struct
    {
        var argument = Activator.CreateInstance<T>();

        if (argument == null)
        {
            throw new Exception($"Cannot create instance of {typeof(T).FullName}");
        }

        if (args.Length == 1)
        {
            argument.Mode = args[0];
            return new Tuple<T, Mode>(argument, defaultMode);
        }

        string modeArg = "";

        CL.WriteLine("args.Length: " + args.Length);

        if (args.Length != 0)
        {
            CmdArgs.ProcessArgsErrors = ProcessArgsErrors;
            argument = CmdArgs.SaveArgsWorker<T>(args);

            modeArg = argument.Mode;
        }

        if (modeArg != null)
        {
            CL.WriteLine("modeArg is NOT null");
            if (Enum.TryParse<Mode>(modeArg, out var mode))
            {
                return new Tuple<T, Mode>(argument, mode);
            }
            else
            {
                return new Tuple<T, Mode>(argument, defaultMode);
            }
        }
        else
        {
            CL.WriteLine("modeArg is null");

            return new Tuple<T, Mode>(argument, defaultMode);
        }
    }
}
