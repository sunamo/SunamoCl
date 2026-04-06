namespace SunamoCl.SunamoCmdArgs.Data;

/// <summary>
/// Provides command-line argument parsing and storage using CommandLineParser library
/// </summary>
public class CmdArgs
{
    /// <summary>
    /// Gets or sets the parsed options object
    /// </summary>
    public static object Opts { get; set; } = null!;

    /// <summary>
    ///     must be IEnumerable
    /// </summary>
    public static Action<IEnumerable<Error>> ProcessArgsErrors { get; set; } = null!;

    /// <summary>
    /// Parses command-line arguments into the specified options type and stores the result
    /// </summary>
    /// <typeparam name="T">Options type to parse arguments into (e.g. CmdArgsEveryLine)</typeparam>
    /// <param name="args">Command-line arguments to parse</param>
    /// <returns>Parsed options object of type T</returns>
    public static T SaveArgsWorker<T>(string[] args)
    {
        if (ProcessArgsErrors == null) ThrowEx.IsNull("ProcessArgsErrors");

        var parseResult = Parser.Default.ParseArguments<T>(args);

        var result = parseResult.WithParsed(SaveArgs);
        result.WithNotParsed(ProcessArgsErrors);

        return (T)Opts;
    }

    private static void SaveArgs<T>(T options)
    {
        Opts = options!;
    }
}