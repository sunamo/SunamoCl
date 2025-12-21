namespace SunamoCl.SunamoCmd.Args;

public class WriteProgressBarArgs
{
    public static WriteProgressBarArgs Default = new();
    public double Actual;
    public double Overall;

    public bool Update;
    public bool WritePieces;

    public WriteProgressBarArgs()
    {
    }

    public WriteProgressBarArgs(bool update) : this()
    {
        this.Update = update;
    }

    public WriteProgressBarArgs(bool update, double actual, double overall) : this(update)
    {
        this.Actual = actual;
        this.Overall = overall;
        WritePieces = true;
    }
}