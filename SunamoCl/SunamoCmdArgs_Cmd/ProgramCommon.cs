namespace SunamoCl.SunamoCmdArgs_Cmd;

/// <summary>
/// Helper for parsing command-line arguments into typed options and resolving the application mode
/// </summary>
public class ProgramCommon
{
    private void ProcessArgsErrors(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            Console.WriteLine($"Argument parsing error: {error}");
        }
    }

    /// <summary>
    /// Parses command-line arguments into typed options and resolves the application mode.
    /// For apps that did not use Mode, ModeCl can be used. Mode is no longer used for new applications.
    /// </summary>
    /// <typeparam name="T">Arguments type inheriting from CommonArgs</typeparam>
    /// <typeparam name="Mode">Enum type representing available application modes</typeparam>
    /// <param name="args">Command-line arguments to parse</param>
    /// <param name="ifParseFail">Default mode to use when parsing fails</param>
    /// <returns>Tuple of parsed arguments and resolved mode, or null on failure</returns>
    public Tuple<T, Mode>? ProcessArgs<T, Mode>(string[] args, Mode ifParseFail)
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
            return new Tuple<T, Mode>(argument, ifParseFail);
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
                return new Tuple<T, Mode>(argument, ifParseFail);
            }
        }
        else
        {
            CL.WriteLine("modeArg is null");

            return new Tuple<T, Mode>(argument, ifParseFail);
        }
    }
}