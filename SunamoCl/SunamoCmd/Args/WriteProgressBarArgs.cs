namespace SunamoCl.SunamoCmd.Args;

public class WriteProgressBarArgs
{
    public static WriteProgressBarArgs Default = new();
    public double Actual;
    public double Overall;

    public bool ShouldUpdate;
    public bool WritePieces;

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