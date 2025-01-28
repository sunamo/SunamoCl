namespace SunamoCl._sunamo.SunamoData.Data;

internal class AB
{
    internal static Type type = typeof(AB);
    internal string A;
    internal object B;

    internal AB(string a, object b)
    {
        A = a;
        B = b;
    }


    /// <param name="a"></param>
    /// <param name="b"></param>
    internal static AB Get(string a, object b)
    {
        return new AB(a, b);
    }

    public override string ToString()
    {
        return A + ":" + B;
    }
}