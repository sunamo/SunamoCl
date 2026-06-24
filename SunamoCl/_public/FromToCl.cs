namespace SunamoCl._public;

public class FromToCl : FromToTSHCl<long>
{
    public static FromToCl Empty { get; set; } = new(true);

    public FromToCl()
    {
    }

    private FromToCl(bool isEmpty)
    {
        base.IsEmpty = isEmpty;
    }

    public FromToCl(long from, long to, FromToUseCl fromToUse = FromToUseCl.DateTime)
    {
        this.From = from;
        this.To = to;
        this.FromToUse = fromToUse;
    }
}
