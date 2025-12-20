// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoCl._public;

public class FromToTSHCl<T>
{
    public bool Empty;
    protected long FromLong;
    public FromToUseCl FromToUse = FromToUseCl.DateTime;
    protected long ToLong;
    public FromToTSHCl()
    {
        var type = typeof(T);
        if (type == typeof(int)) FromToUse = FromToUseCl.None;
    }
    /// <summary>
    ///     Use Empty contstant outside of class
    /// </summary>
    /// <param name="empty"></param>
    private FromToTSHCl(bool empty) : this()
    {
        this.Empty = empty;
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