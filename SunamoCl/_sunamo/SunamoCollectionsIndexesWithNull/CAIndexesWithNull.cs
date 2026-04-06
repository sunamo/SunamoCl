namespace SunamoCl._sunamo.SunamoCollectionsIndexesWithNull;

/// <summary>
/// Provides methods to find indexes of null or empty elements in collections.
/// </summary>
internal class CAIndexesWithNull
{
    /// <summary>
    /// Returns indexes of all null or empty elements in the list.
    /// </summary>
    /// <param name="list">List to search for null or empty elements.</param>
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

    /// <summary>
    /// Returns indexes of all null elements in the list.
    /// </summary>
    /// <param name="list">List to search for null elements.</param>
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