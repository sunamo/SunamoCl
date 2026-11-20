namespace SunamoCl.SunamoCmdArgs.Data;

/// <summary>
/// Represents the common args type.
/// </summary>
public class CommonArgs
{
    /// <summary>
    /// Gets or sets the mode.
    /// </summary>
    [Option("Mode", ResourceType = typeof(string))]
    public string Mode { get; set; } = "";

    // By default, only mode and mode args do not need a switch.
    // The folder on the second position after mode must be preserved (used e.g. in VS quick actions with $(ProjectDir)).
    /// <summary>
    /// Gets or sets the run in folder.
    /// </summary>
    [Option("RunInFolder", ResourceType = typeof(string))]
    public string RunInFolder { get; set; } = string.Empty;
}
