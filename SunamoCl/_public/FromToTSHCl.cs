namespace SunamoCl._public;

public class FromToTSHCl<T>
{
    public bool IsEmpty { get; set; }
    protected long FromLong;
    public FromToUseCl FromToUse { get; set; } = FromToUseCl.DateTime;
    protected long ToLong;
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
    public T From
    {
        get => (T)(dynamic)FromLong;
        set => FromLong = (long)(dynamic)value!;
    }
    public T To
    {
        get => (T)(dynamic)ToLong;
        set => ToLong = (long)(dynamic)value!;
    }
    public long FromL => FromLong;
    public long ToL => ToLong;
}