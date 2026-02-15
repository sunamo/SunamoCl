namespace SunamoCl.SunamoCmd;

/// <summary>
/// Arguments for bootstrapping a command-line application with actions and initialization functions
/// </summary>
public class CmdBootStrapArgs
{
    /// <summary>
    /// Gets or sets the action to initialize SQL measure time tracking
    /// </summary>
    public Action InitSqlMeasureTime { get; set; } = null!;

    #region Cant be null


    /// <summary>
    /// Gets or sets the action to run in debug mode
    /// </summary>
    public Action RunInDebug { get; set; } = null!;

    /// <summary>
    /// Gets or sets the function that provides a group of named actions for user selection
    /// </summary>
    public Func<Dictionary<string, Action>> AddGroupOfActions { get; set; } = null!;

    /// <summary>
    /// Gets or sets whether to ask the user which action to run in release mode
    /// </summary>
    public bool AskUserIfRelease { get; set; }

    #endregion
}