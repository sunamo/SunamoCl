namespace SunamoCl._sunamo;

internal class CA
{
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

    internal static T[,] OneDimensionArrayToTwoDirection<T>(T[] flatArray, int width)
    {
        var height = (int)Math.Ceiling(flatArray.Length / (double)width);
        var result = new T[height, width];
        for (var index = 0; index < flatArray.Length; index++)
        {
            var rowIndex = index / width;
            var colIndex = index % width;
            result[rowIndex, colIndex] = flatArray[index];
        }

        return result;
    }
}
