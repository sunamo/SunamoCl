namespace SunamoCl.SunamoCmdArgs.Data;

public class CommonArgs
{
    [Option("Mode", ResourceType = typeof(string))]
    public string Mode { get; set; } = "";

    // By default, only mode and mode args do not need a switch.
    // The folder on the second position after mode must be preserved (used e.g. in VS quick actions with $(ProjectDir)).
    [Option("RunInFolder", ResourceType = typeof(string))]
    public string RunInFolder { get; set; } = string.Empty;
}
