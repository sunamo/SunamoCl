namespace SunamoCl.SunamoCmd;

/// <summary>
/// Represents the cmd boot strap args type.
/// </summary>
public class CmdBootStrapArgs
{
    /// <summary>
    /// Gets or sets the init sql measure time.
    /// </summary>
    public Action InitSqlMeasureTime { get; set; } = null!;

    #region Cant be null

    /// <summary>
    /// Gets or sets the run in debug.
    /// </summary>
    public Action RunInDebug { get; set; } = null!;

    /// <summary>
    /// Gets or sets the add group of actions.
    /// </summary>
    public Func<Dictionary<string, Action>> AddGroupOfActions { get; set; } = null!;

    /// <summary>
    /// Gets or sets the should ask user if release.
    /// </summary>
    public bool ShouldAskUserIfRelease { get; set; }

    #endregion
}
