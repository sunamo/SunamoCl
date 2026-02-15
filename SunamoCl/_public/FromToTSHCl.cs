namespace SunamoCl._public;

/// <summary>
/// Generic from-to range class with support for different time and value representations
/// </summary>
/// <typeparam name="T">Type of the from and to values</typeparam>
public class FromToTSHCl<T>
{
    /// <summary>
    /// Gets or sets whether this range represents an empty value
    /// </summary>
    public bool IsEmpty { get; set; }
    /// <summary>
    /// Internal storage for the From value as a long
    /// </summary>
    protected long FromLong;
    /// <summary>
    /// Gets or sets what type of from-to representation is used (DateTime, Unix, etc.)
    /// </summary>
    public FromToUseCl FromToUse { get; set; } = FromToUseCl.DateTime;
    /// <summary>
    /// Internal storage for the To value as a long
    /// </summary>
    protected long ToLong;
    /// <summary>
    /// Initializes a new instance and sets FromToUse to None if the type is int
    /// </summary>
    public FromToTSHCl()
    {
        var type = typeof(T);
        if (type == typeof(int)) FromToUse = FromToUseCl.None;
    }
    /// <summary>
    ///     Use IsEmpty contstant outside of class
    /// </summary>
    /// <param name="isEmpty"></param>
    private FromToTSHCl(bool isEmpty) : this()
    {
        this.IsEmpty = isEmpty;
    }
    /// <summary>
    ///     A3 true = DateTime
    ///     A3 False = None
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="fromToUse"></param>
    public FromToTSHCl(T from, T to, FromToUseCl fromToUse = FromToUseCl.DateTime) : this()
    {
        this.From = from;
        this.To = to;
        this.FromToUse = fromToUse;
    }
    /// <summary>
    /// Gets or sets the start value of the range, stored internally as a long
    /// </summary>
    public T From
    {
        get => (T)(dynamic)FromLong;
        set => FromLong = (long)(dynamic)value!;
    }
    /// <summary>
    /// Gets or sets the end value of the range, stored internally as a long
    /// </summary>
    public T To
    {
        get => (T)(dynamic)ToLong;
        set => ToLong = (long)(dynamic)value!;
    }
    /// <summary>
    /// Gets the From value as a long
    /// </summary>
    public long FromL => FromLong;
    /// <summary>
    /// Gets the To value as a long
    /// </summary>
    public long ToL => ToLong;
}