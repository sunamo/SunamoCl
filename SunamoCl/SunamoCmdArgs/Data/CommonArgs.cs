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
    /// if I want to run the application in a folder other than the current one. In the default state, the only 2 attributes that do not need a switch are only the mode and args to the mode in each application
    ///
    /// Krom toho ale musí zůstat zachováno i složka na druhém místě hned za módem! Využívá se to např. ve VS při rychlých akcích $(ProjectDir)
    /// </summary>
    [Option("RunInFolder", ResourceType = typeof(string))]
    public string RunInFolder { get; set; } = string.Empty;
}