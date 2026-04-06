namespace SunamoCl._public;

/// <summary>
/// Represents a from-to range with long values. Both from and to must always be entered.
/// </summary>
public class FromToCl : FromToTSHCl<long>
{
    /// <summary>
    /// Represents an empty FromToCl range.
    /// </summary>
    public static FromToCl Empty = new(true);

    /// <summary>
    /// Initializes a new instance with default values.
    /// </summary>
    public FromToCl()
    {
    }

    /// <summary>
    /// Initializes a new empty instance. Use <see cref="Empty"/> constant outside of class.
    /// </summary>
    /// <param name="isEmpty">Whether the range is empty.</param>
    private FromToCl(bool isEmpty)
    {
        base.IsEmpty = isEmpty;
    }

    /// <summary>
    /// Initializes a new instance with the specified range and representation type.
    /// </summary>
    /// <param name="from">The start value of the range.</param>
    /// <param name="to">The end value of the range.</param>
    /// <param name="fromToUse">The type of from-to representation (DateTime by default, None for plain numeric).</param>
    public FromToCl(long from, long to, FromToUseCl fromToUse = FromToUseCl.DateTime)
    {
        this.From = from;
        this.To = to;
        this.FromToUse = fromToUse;
    }
}