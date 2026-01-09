// variables names: ok
namespace SunamoCl.SunamoCmd.Args;

public class WriteProgressBarArgs
{
    public static WriteProgressBarArgs Default = new();
    public double Actual { get; set; }
    public double Overall { get; set; }

    public bool ShouldUpdate { get; set; }
    public bool WritePieces { get; set; }

    public WriteProgressBarArgs()
    {
    }

    public WriteProgressBarArgs(bool shouldUpdate) : this()
    {
        this.ShouldUpdate = shouldUpdate;
    }

    public WriteProgressBarArgs(bool shouldUpdate, double actual, double overall) : this(shouldUpdate)
    {
        this.Actual = actual;
        this.Overall = overall;
        WritePieces = true;
    }
}