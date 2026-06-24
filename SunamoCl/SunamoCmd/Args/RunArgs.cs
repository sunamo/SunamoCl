namespace SunamoCl.SunamoCmd.Args;

public class RunArgs
{
    public Func<Task>? RunInDebugAsync { get; set; }
    public Func<Dictionary<string, Func<Task<Dictionary<string, object>>>>>? AddGroupOfActions { get; set; }
    public bool? ShouldAskUserIfRelease { get; set; }
    public bool ShouldLoadFromClipboard { get; set; }

    public String[] Args { get; set; } = [];

    public bool ShouldCatchUnhandledException
    {
        get; set;
    }
    public bool IsDebug { get; set; }

    public IServiceCollection? ServiceCollection
    {
        get; set;
    }

    // When enabled, the application logs all important steps to the console so that AI tools can understand what is happening in the application.
    public bool IsVerboseConsoleLogging { get; set; } = true;

    // When set, everything written to Console.Out and Console.Error (including Microsoft.Extensions.Logging console provider)
    // is also written to this file. The file is overwritten on each new application run.
    // This allows AI tools to read the file and understand what happened during the last run.
    public string? ConsoleLogFilePath { get; set; }
}
