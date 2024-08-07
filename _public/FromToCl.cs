namespace SunamoCl._public;

/// <summary>
///     Must have always entered both from and to
///     None of event could have unlimited time!
/// </summary>
public class FromToCl : FromToTSHCl<long>
{
    public static FromToCl Empty = new(true);

    public FromToCl()
    {
    }

    /// <summary>
    ///     Use Empty contstant outside of class
    /// </summary>
    /// <param name="empty"></param>
    private FromToCl(bool empty)
    {
        this.empty = empty;
    }

    /// <summary>
    ///     A3 true = DateTime
    ///     A3 False = None
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="ftUse"></param>
    public FromToCl(long from, long to, FromToUseCl ftUse = FromToUseCl.DateTime)
    {
        this.from = from;
        this.to = to;
        this.ftUse = ftUse;
    }
}