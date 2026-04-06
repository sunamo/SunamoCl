namespace SunamoCl.SunamoCmdArgs.Data;

/// <summary>
/// Base class for command-line arguments containing common properties like mode and run folder
/// </summary>
public class CommonArgs
{
    /// <summary>
    /// Gets or sets the application mode determining which action to execute
    /// </summary>
    [Option("Mode", ResourceType = typeof(string))]
    public string Mode { get; set; } = "";

    /// <summary>
    /// Gets or sets the folder path to run the application in instead of the current directory.
    /// By default, only mode and mode args do not need a switch.
    /// The folder on the second position after mode must be preserved (used e.g. in VS quick actions with $(ProjectDir)).
    /// </summary>
    [Option("RunInFolder", ResourceType = typeof(string))]
    public string RunInFolder { get; set; } = string.Empty;
}