namespace SunamoCl.SunamoCmdArgs.Data;

/// <summary>
/// Provides command-line argument parsing and storage using CommandLineParser library.
/// </summary>
public class CmdArgs
{
    /// <summary>
    /// Gets or sets the parsed options object.
    /// </summary>
    public static object Options { get; set; } = null!;

    /// <summary>
    /// Gets or sets the handler for argument parsing errors. Must accept IEnumerable of Error.
    /// </summary>
    public static Action<IEnumerable<Error>> ProcessArgsErrors { get; set; } = null!;

    /// <summary>
    /// Parses command-line arguments into the specified options type and stores the result.
    /// </summary>
    /// <typeparam name="T">Options type to parse arguments into.</typeparam>
    /// <param name="args">Command-line arguments to parse.</param>
    /// <returns>Parsed options object of type T.</returns>
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
