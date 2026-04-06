namespace SunamoCl._sunamo;

/// <summary>
/// Collection helper class with array and index utilities.
/// </summary>
internal class CA
{
    /// <summary>
    /// Checks whether the list has a valid element at the specified index.
    /// </summary>
    /// <param name="index">Zero-based index to check.</param>
    /// <param name="list">List to check against.</param>
    internal static bool HasIndex(int index, IList list)
    {
        if (index < 0)
        {
            throw new Exception("Invalid parameter index");
        }
        if (list.Count > index)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Converts a one-dimensional array to a two-dimensional array with the specified width.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    /// <param name="flatArray">Source one-dimensional array.</param>
    /// <param name="width">Width of each row in the resulting two-dimensional array.</param>
    internal static T[,] OneDimensionArrayToTwoDirection<T>(T[] flatArray, int width)
    {
        var height = (int)Math.Ceiling(flatArray.Length / (double)width);
        var result = new T[height, width];
        int rowIndex, colIndex;
        for (var index = 0; index < flatArray.Length; index++)
        {
            rowIndex = index / width;
            colIndex = index % width;
            result[rowIndex, colIndex] = flatArray[index];
        }

        return result;
    }

}