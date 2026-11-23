namespace SunamoCl._public;

/// <summary>
/// Represents the from to cl type.
/// </summary>
public class FromToCl : FromToTSHCl<long>
{
    /// <summary>
    /// Gets or sets the empty.
    /// </summary>
    public static FromToCl Empty { get; set; } = new(true);

    /// <summary>
    /// From to cl.
    /// </summary>
    /// <param name="true">The true.</param>
    /// <returns>The from to cl result.</returns>
    public FromToCl()
    {
    }

    private FromToCl(bool isEmpty)
    {
        base.IsEmpty = isEmpty;
    }

    /// <summary>
    /// From to cl.
    /// </summary>
    /// <param name="from">The from.</param>
    /// <param name="to">The to.</param>
    /// <param name="fromToUse">The from to use.</param>
    /// <returns>The from to cl result.</returns>
    public FromToCl(long from, long to, FromToUseCl fromToUse = FromToUseCl.DateTime)
    {
        this.From = from;
        this.To = to;
        this.FromToUse = fromToUse;
    }
}
