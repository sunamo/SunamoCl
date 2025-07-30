namespace SunamoCl.SunamoCmd.Args;

public class WriteProgressBarArgs
{
    public static WriteProgressBarArgs Default = new();
    public double actual;
    public double overall;

    public bool update;
    public bool writePieces;

    public WriteProgressBarArgs()
    {
    }

    public WriteProgressBarArgs(bool update) : this()
    {
        this.update = update;
    }

    public WriteProgressBarArgs(bool update, double actual, double overall) : this(update)
    {
        this.actual = actual;
        this.overall = overall;
        writePieces = true;
    }
}