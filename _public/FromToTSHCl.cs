namespace SunamoCl._public;
public class FromToTSHCl<T>
{
    public bool empty;
    protected long fromL;
    public FromToUseCl ftUse = FromToUseCl.DateTime;
    protected long toL;
    public FromToTSHCl()
    {
        var t = typeof(T);
        if (t == Types.tInt) ftUse = FromToUseCl.None;
    }
    /// <summary>
    ///     Use Empty contstant outside of class
    /// </summary>
    /// <param name="empty"></param>
    private FromToTSHCl(bool empty) : this()
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
    public FromToTSHCl(T from, T to, FromToUseCl ftUse = FromToUseCl.DateTime) : this()
    {
        this.from = from;
        this.to = to;
        this.ftUse = ftUse;
    }
    public T from
    {
        get => (T)(dynamic)fromL;
        set => fromL = (long)(dynamic)value;
    }
    public T to
    {
        get => (T)(dynamic)toL;
        set => toL = (long)(dynamic)value;
    }
    public long FromL => fromL;
    public long ToL => toL;
}