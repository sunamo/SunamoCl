namespace SunamoCl.SunamoCmd.Tables;

/// <summary>
///     Working
///     Rightly set up width of column by content
/// </summary>
public static class TableParser
{
    private static int[] GetMaxColumnsWidth(string[,] arrValues)
    {
        var maxColumnsWidth = new int[arrValues.GetLength(1)];
        for (var colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            for (var rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                var newLength = arrValues[rowIndex, colIndex].Length;
                var oldLength = maxColumnsWidth[colIndex];

                if (newLength > oldLength) maxColumnsWidth[colIndex] = newLength;
            }

        return maxColumnsWidth;
    }

    #region First approach

    /// <summary>
    /// Converts a list of objects into a formatted string table using the specified column headers and value selectors
    /// </summary>
    /// <typeparam name="T">Type of the objects in the list</typeparam>
    /// <param name="values">List of objects to display as rows</param>
    /// <param name="columnHeaders">Headers for each column</param>
    /// <param name="valueSelectors">Functions to extract column values from each object</param>
    /// <returns>Formatted string table</returns>
    public static string ToStringTable<T>(
        this List<T> values,
        List<string> columnHeaders,
        params Func<T, object>[] valueSelectors)
    {
        return values.ToStringTable(columnHeaders, valueSelectors);
    }

    /// <summary>
    /// Converts an array of objects into a formatted string table using the specified column headers and value selectors
    /// </summary>
    /// <typeparam name="T">Type of the objects in the array</typeparam>
    /// <param name="values">Array of objects to display as rows</param>
    /// <param name="columnHeaders">Headers for each column</param>
    /// <param name="valueSelectors">Functions to extract column values from each object</param>
    /// <returns>Formatted string table</returns>
    public static string ToStringTable<T>(
        this T[] values,
        string[] columnHeaders,
        params Func<T, object>[] valueSelectors)
    {
        //Debug.Assert(columnHeaders.Length == valueSelectors.Length);

        var arrValues = new string[values.Length + 1, valueSelectors.Length];

        // Fill headers
        for (var colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            arrValues[0, colIndex] = columnHeaders[colIndex];

        // Fill table rows
        for (var rowIndex = 1; rowIndex < arrValues.GetLength(0); rowIndex++)
            for (var colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                arrValues[rowIndex, colIndex] = valueSelectors[colIndex]
                    .Invoke(values[rowIndex - 1])?.ToString() ?? string.Empty;

        return arrValues.ToStringTable();
    }

    /// <summary>
    /// Converts a two-dimensional string array into a formatted string table with column alignment
    /// </summary>
    /// <param name="arrValues">Two-dimensional array of string values</param>
    /// <returns>Formatted string table with header separator</returns>
    public static string ToStringTable(this string[,] arrValues)
    {
        var maxColumnsWidth = GetMaxColumnsWidth(arrValues);
        var headerSpliter = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);

        var stringBuilder = new StringBuilder();
        for (var rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
        {
            for (var colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                // Print cell
                var cell = arrValues[rowIndex, colIndex];
                cell = cell.PadRight(maxColumnsWidth[colIndex]);
                stringBuilder.Append(" | ");
                stringBuilder.Append(cell);
            }

            // Print end of line
            stringBuilder.Append(" | ");
            stringBuilder.AppendLine();

            // Print splitter
            if (rowIndex == 0)
            {
                stringBuilder.AppendFormat(" |{0}| ", headerSpliter);
                stringBuilder.AppendLine();
            }
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Converts headers and rows into a formatted string table by flattening into a two-dimensional array
    /// </summary>
    /// <param name="headers">Column header names</param>
    /// <param name="rows">Rows of data as lists of strings</param>
    /// <returns>Formatted string table</returns>
    public static string ToStringTable(List<string> headers, IList<List<string>> rows)
    {
        var firstRow = rows.First();
        var flatValues = new List<string>(firstRow.Count * rows.Count() + firstRow.Count);

        flatValues.AddRange(headers);
        foreach (var item in rows) flatValues.AddRange(item);

        string[,] twoDimensionalArray = CA.OneDimensionArrayToTwoDirection(flatValues.ToArray(), firstRow.Count);

        return twoDimensionalArray.ToStringTable();
    }

    #endregion
}