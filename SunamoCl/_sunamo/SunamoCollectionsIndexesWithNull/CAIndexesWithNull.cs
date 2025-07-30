namespace SunamoCl._sunamo.SunamoCollectionsIndexesWithNull;

internal class CAIndexesWithNull
{
    internal static List<int> IndexesWithNullOrEmpty(IList times)
    {
        var nulled = new List<int>();
        var i = 0;
        foreach (var item in times)
        {
            if (item == null)
                nulled.Add(i);
            else if (item.ToString() == string.Empty) nulled.Add(i);
            i++;
        }

        return nulled;
    }

    internal static List<int> IndexesWithNull(IList times)
    {
        var nulled = new List<int>();
        var i = 0;
        foreach (var item in times)
        {
            if (item == null) nulled.Add(i);
            i++;
        }

        return nulled;
    }
}