namespace SunamoCl._sunamo.SunamoCollectionsIndexesWithNull;

internal class CAIndexesWithNull
{
    internal static List<int> IndexesWithNullOrEmpty(IList list)
    {
        var nullIndexes = new List<int>();
        var index = 0;
        foreach (var item in list)
        {
            if (item == null)
                nullIndexes.Add(index);
            else if (item.ToString() == string.Empty) nullIndexes.Add(index);
            index++;
        }

        return nullIndexes;
    }

    internal static List<int> IndexesWithNull(IList list)
    {
        var nullIndexes = new List<int>();
        var index = 0;
        foreach (var item in list)
        {
            if (item == null) nullIndexes.Add(index);
            index++;
        }

        return nullIndexes;
    }
}
