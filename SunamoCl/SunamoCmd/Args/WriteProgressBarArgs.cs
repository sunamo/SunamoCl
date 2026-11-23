namespace SunamoCl.SunamoCmd.Args;

/// <summary>
/// Represents the write progress bar args type.
/// </summary>
public class WriteProgressBarArgs
{
    /// <summary>
    /// Gets or sets the default.
    /// </summary>
    public static WriteProgressBarArgs Default { get; set; } = new();
    /// <summary>
    /// Gets or sets the actual.
    /// </summary>
    public double Actual { get; set; }
    /// <summary>
    /// Gets or sets the overall.
    /// </summary>
    public double Overall { get; set; }

    /// <summary>
    /// Gets or sets the should update.
    /// </summary>
    public bool ShouldUpdate { get; set; }
    /// <summary>
    /// Gets or sets the should write pieces.
    /// </summary>
    public bool ShouldWritePieces { get; set; }

    /// <summary>
    /// Initializes a new instance of the write progress bar args class.
    /// </summary>
    public WriteProgressBarArgs()
    {
    }

    /// <summary>
    /// Write progress bar args.
    /// </summary>
    /// <returns>The write progress bar args result.</returns>
    public WriteProgressBarArgs(bool shouldUpdate) : this()
    {
        this.ShouldUpdate = shouldUpdate;
    }

    /// <summary>
    /// Write progress bar args.
    /// </summary>
    /// <returns>The write progress bar args result.</returns>
    public WriteProgressBarArgs(bool shouldUpdate, double actual, double overall) : this(shouldUpdate)
    {
        this.Actual = actual;
        this.Overall = overall;
        ShouldWritePieces = true;
    }
}
