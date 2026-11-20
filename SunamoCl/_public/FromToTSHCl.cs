namespace SunamoCl._public;

/// <summary>
/// Represents the from to TSH cl t type.
/// </summary>
public class FromToTSHCl<T>
{
    /// <summary>
    /// Gets or sets the is empty.
    /// </summary>
    public bool IsEmpty { get; set; }
    /// <summary>
    /// The from long.
    /// </summary>
    protected long FromLong;
    /// <summary>
    /// Gets or sets the from to use.
    /// </summary>
    public FromToUseCl FromToUse { get; set; } = FromToUseCl.DateTime;
    /// <summary>
    /// The to long.
    /// </summary>
    protected long ToLong;

    /// <summary>
    /// Initializes a new instance of the from to TSH cl class.
    /// </summary>
    public FromToTSHCl()
    {
        var type = typeof(T);
        if (type == typeof(int)) FromToUse = FromToUseCl.None;
    }

    private FromToTSHCl(bool isEmpty) : this()
    {
        this.IsEmpty = isEmpty;
    }

    /// <summary>
    /// Initializes a new instance of the from to TSH cl class.
    /// </summary>
    public FromToTSHCl(T from, T to, FromToUseCl fromToUse = FromToUseCl.DateTime) : this()
    {
        this.From = from;
        this.To = to;
        this.FromToUse = fromToUse;
    }

    /// <summary>
    /// The from.
    /// </summary>
    public T From
    {
        get => (T)(dynamic)FromLong;
        set => FromLong = (long)(dynamic)value!;
    }

    /// <summary>
    /// The to.
    /// </summary>
    public T To
    {
        get => (T)(dynamic)ToLong;
        set => ToLong = (long)(dynamic)value!;
    }

    /// <summary>
    /// Gets the from as long.
    /// </summary>
    public long FromAsLong => FromLong;

    /// <summary>
    /// Gets the to as long.
    /// </summary>
    public long ToAsLong => ToLong;
}
