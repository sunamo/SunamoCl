namespace SunamoCl;


public class FromToTSHCl<T>
{

    internal bool empty;
    protected long fromL;
    internal FromToUseCl ftUse = FromToUseCl.DateTime;
    protected long toL;
    internal FromToTSHCl()
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
    internal FromToTSHCl(T from, T to, FromToUseCl ftUse = FromToUseCl.DateTime) : this()
    {
        this.from = from;
        this.to = to;
        this.ftUse = ftUse;
    }
    internal T from
    {
        get => (T)(dynamic)fromL;
        set => fromL = (long)(dynamic)value;
    }
    internal T to
    {
        get => (T)(dynamic)toL;
        set => toL = (long)(dynamic)value;
    }
    internal long FromL => fromL;
    internal long ToL => toL;
}