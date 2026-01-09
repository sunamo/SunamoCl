// variables names: ok
namespace SunamoCl._sunamo.SunamoData.Data;

internal class AB
{
    internal static Type Type = typeof(AB);
    internal string A { get; set; }
    internal object B { get; set; }

    internal AB(string aValue, object bValue)
    {
        A = aValue;
        B = bValue;
    }


    /// <param name="aValue"></param>
    /// <param name="bValue"></param>
    internal static AB Get(string aValue, object bValue)
    {
        return new AB(aValue, bValue);
    }

    public override string ToString()
    {
        return A + ":" + B;
    }
}