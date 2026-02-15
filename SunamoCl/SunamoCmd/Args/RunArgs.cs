namespace SunamoCl.SunamoCmd.Args;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
/// <summary>
/// Arguments for configuring the command-line application run behavior including debug mode, actions, and service setup
/// </summary>
public class RunArgs
{
    /// <summary>
    /// Gets or sets the async function to run when in debug mode
    /// </summary>
    public Func<Task>? RunInDebugAsync { get; set; }
    /// <summary>
    /// Gets or sets the function that provides groups of actions for user selection
    /// </summary>
    public Func<Dictionary<string, Func<Task<Dictionary<string, object>>>>>? AddGroupOfActions { get; set; }
    /// <summary>
    /// Gets or sets whether to ask the user to select an action in release mode
    /// </summary>
    public bool? AskUserIfRelease { get; set; }
    /// <summary>
    /// Gets or sets whether to load input data from clipboard
    /// </summary>
    public bool LoadFromClipboard { get; set; }

    /// <summary>
    /// Gets or sets the command-line arguments passed to the application
    /// </summary>
    public String[] Args { get; set; } = [];

    /// <summary>
    /// Gets or sets whether to catch and handle unhandled exceptions
    /// </summary>
    public bool CatchUnhandledException
    {
        get; set;
    }
    /// <summary>
    /// Gets or sets whether the application is running in debug mode
    /// </summary>
    public bool IsDebug { get; set; }

    /// <summary>
    /// Gets or sets the service collection for dependency injection configuration
    /// </summary>
    public IServiceCollection? ServiceCollection
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets whether to enable verbose console logging. Default is true.
    /// When enabled, the application logs all important steps to the console so that AI tools can understand what is happening in the application.
    /// </summary>
    public bool VerboseConsoleLogging { get; set; } = true;

    /// <summary>
    /// Gets or sets the file path for mirroring all console output.
    /// When set, everything written to Console.Out and Console.Error (including Microsoft.Extensions.Logging console provider)
    /// is also written to this file. The file is overwritten on each new application run.
    /// This allows AI tools to read the file and understand what happened during the last run.
    /// </summary>
    public string? ConsoleLogFilePath { get; set; }
}