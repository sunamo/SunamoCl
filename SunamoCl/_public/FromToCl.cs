namespace SunamoCl._public;

/// <summary>
///     Must have always entered both from and to
///     None of event could have unlimited time!
/// </summary>
public class FromToCl : FromToTSHCl<long>
{
    /// <summary>
    /// Represents an empty FromToCl range
    /// </summary>
    public static FromToCl Empty = new(true);

    /// <summary>
    /// Initializes a new instance with default values
    /// </summary>
    public FromToCl()
    {
    }

    /// <summary>
    ///     Use IsEmpty contstant outside of class
    /// </summary>
    /// <param name="isEmpty"></param>
    private FromToCl(bool isEmpty)
    {
        base.IsEmpty = isEmpty;
    }

    /// <summary>
    ///     A3 true = DateTime
    ///     A3 False = None
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="fromToUse"></param>
    public FromToCl(long from, long to, FromToUseCl fromToUse = FromToUseCl.DateTime)
    {
        this.From = from;
        this.To = to;
        this.FromToUse = fromToUse;
    }
}