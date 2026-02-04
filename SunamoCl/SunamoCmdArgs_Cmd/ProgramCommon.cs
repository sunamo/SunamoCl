namespace SunamoCl.SunamoCmdArgs_Cmd;

/// <summary>
/// Třída by se mohla jmenovat i CommandLineArgsParserHelper
/// Ale jelikož ji všude mám jako instanční ProgramCommon, už ji tak nechám
/// </summary>
public class ProgramCommon
{
    /// <summary>
    ///     must be IEnumerable
    /// </summary>
    /// <param name="errors"></param>
    private void ProcessArgsErrors(IEnumerable<Error> errors)
    {
    }

    /// <summary>
    /// Jako Mode lze použít ModeCl pro apps které Mode nepoužívali. Dnes už Mode nepoužívám pro nové.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Mode"></typeparam>
    /// <param name="args"></param>
    /// <param name="ifParseFail"></param>
    /// <param name="writeError"></param>
    /// <returns></returns>
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

        string arg = "";

        #region Parse and executing node if was set
        CL.WriteLine("args.Length: " + args.Length);

        if (args.Length != 0)
        {
            // 2) parsování atributů
            CmdArgs.ProcessArgsErrors = ProcessArgsErrors;
            argument = CmdArgs.SaveArgsWorker<T>(args);

            arg = argument.Mode;
        }

        if (arg != null)
        {
            CL.WriteLine("arg is NOT null");
            if (Enum.TryParse<Mode>(arg, out var mode))
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
            CL.WriteLine("arg is null");

            return new Tuple<T, Mode>(argument, ifParseFail);
        }

        #endregion
    }

    
}