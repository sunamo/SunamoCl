namespace SunamoCl._sunamo.SunamoCollectionsIndexesWithNull;

internal class CAIndexesWithNull
{
    internal static List<int> IndexesWithNullOrEmpty(IList list)
    {
        var nulled = new List<int>();
        var index = 0;
        foreach (var item in list)
        {
            if (item == null)
                nulled.Add(index);
            else if (item.ToString() == string.Empty) nulled.Add(index);
            index++;
        }

        return nulled;
    }

    internal static List<int> IndexesWithNull(IList list)
    {
        var nulled = new List<int>();
        var index = 0;
        foreach (var item in list)
        {
            if (item == null) nulled.Add(index);
            index++;
        }

        return nulled;
    }
}