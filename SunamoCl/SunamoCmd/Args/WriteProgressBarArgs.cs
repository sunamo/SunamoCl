namespace SunamoCl.SunamoCmd.Args;

/// <summary>
/// Arguments controlling progress bar display behavior and values
/// </summary>
public class WriteProgressBarArgs
{
    /// <summary>
    /// Gets the default WriteProgressBarArgs instance
    /// </summary>
    public static WriteProgressBarArgs Default = new();
    /// <summary>
    /// Gets or sets the current progress value
    /// </summary>
    public double Actual { get; set; }
    /// <summary>
    /// Gets or sets the total progress value
    /// </summary>
    public double Overall { get; set; }

    /// <summary>
    /// Gets or sets whether the progress bar should be updated on each tick
    /// </summary>
    public bool ShouldUpdate { get; set; }
    /// <summary>
    /// Gets or sets whether to write individual progress pieces
    /// </summary>
    public bool WritePieces { get; set; }

    /// <summary>
    /// Initializes a new instance with default values
    /// </summary>
    public WriteProgressBarArgs()
    {
    }

    /// <summary>
    /// Initializes a new instance with the specified update behavior
    /// </summary>
    /// <param name="shouldUpdate">Whether to update the progress bar on each tick</param>
    public WriteProgressBarArgs(bool shouldUpdate) : this()
    {
        this.ShouldUpdate = shouldUpdate;
    }

    /// <summary>
    /// Initializes a new instance with update behavior and progress values
    /// </summary>
    /// <param name="shouldUpdate">Whether to update the progress bar on each tick</param>
    /// <param name="actual">Current progress value</param>
    /// <param name="overall">Total progress value</param>
    public WriteProgressBarArgs(bool shouldUpdate, double actual, double overall) : this(shouldUpdate)
    {
        this.Actual = actual;
        this.Overall = overall;
        WritePieces = true;
    }
}