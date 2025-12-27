namespace SunamoCl._public;

public class FromToTSHCl<T>
{
    public bool IsEmpty;
    protected long FromLong;
    public FromToUseCl FromToUse = FromToUseCl.DateTime;
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
    /// <param name="ftUse"></param>
    public FromToTSHCl(T from, T to, FromToUseCl ftUse = FromToUseCl.DateTime) : this()
    {
        this.from = from;
        this.to = to;
        this.FromToUse = ftUse;
    }
    public T from
    {
        get => (T)(dynamic)FromLong;
        set => FromLong = (long)(dynamic)value;
    }
    public T to
    {
        get => (T)(dynamic)ToLong;
        set => ToLong = (long)(dynamic)value;
    }
    public long FromL => FromLong;
    public long ToL => ToLong;
}