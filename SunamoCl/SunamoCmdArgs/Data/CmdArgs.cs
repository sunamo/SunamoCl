namespace SunamoCl.SunamoCmdArgs.Data;

/// <summary>
/// Represents the cmd args type.
/// </summary>
public class CmdArgs
{
    /// <summary>
    /// Gets or sets the options.
    /// </summary>
    public static object Options { get; set; } = null!;

    /// <summary>
    /// Gets or sets the process args errors.
    /// </summary>
    public static Action<IEnumerable<Error>> ProcessArgsErrors { get; set; } = null!;

    /// <summary>
    /// Save args worker t.
    /// </summary>
    /// <typeparam name="T">The type T.</typeparam>
    /// <param name="args">The args.</param>
    /// <returns>The save args worker t result.</returns>
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
