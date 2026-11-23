namespace SunamoCl.SunamoCmd.Args;

/// <summary>
/// Represents the run args type.
/// </summary>
public class RunArgs
{
    /// <summary>
    /// Gets or sets the run in debug async.
    /// </summary>
    public Func<Task>? RunInDebugAsync { get; set; }
    /// <summary>
    /// Gets or sets the add group of actions.
    /// </summary>
    public Func<Dictionary<string, Func<Task<Dictionary<string, object>>>>>? AddGroupOfActions { get; set; }
    /// <summary>
    /// Gets or sets the should ask user if release.
    /// </summary>
    public bool? ShouldAskUserIfRelease { get; set; }
    /// <summary>
    /// Gets or sets the should load from clipboard.
    /// </summary>
    public bool ShouldLoadFromClipboard { get; set; }

    /// <summary>
    /// Gets or sets the args.
    /// </summary>
    public String[] Args { get; set; } = [];

    /// <summary>
    /// The should catch unhandled exception.
    /// </summary>
    public bool ShouldCatchUnhandledException
    {
        get; set;
    }
    /// <summary>
    /// Gets or sets the is debug.
    /// </summary>
    public bool IsDebug { get; set; }

    /// <summary>
    /// The service collection.
    /// </summary>
    public IServiceCollection? ServiceCollection
    {
        get; set;
    }

    // When enabled, the application logs all important steps to the console so that AI tools can understand what is happening in the application.
    /// <summary>
    /// Gets or sets the is verbose console logging.
    /// </summary>
    public bool IsVerboseConsoleLogging { get; set; } = true;

    // When set, everything written to Console.Out and Console.Error (including Microsoft.Extensions.Logging console provider)
    // is also written to this file. The file is overwritten on each new application run.
    // This allows AI tools to read the file and understand what happened during the last run.
    /// <summary>
    /// Gets or sets the console log file path.
    /// </summary>
    public string? ConsoleLogFilePath { get; set; }
}
