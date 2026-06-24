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

    private FromToTSHCl(bool isEmpty) : this()
    {
        this.IsEmpty = isEmpty;
    }

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

    public long FromAsLong => FromLong;

    public long ToAsLong => ToLong;
}
