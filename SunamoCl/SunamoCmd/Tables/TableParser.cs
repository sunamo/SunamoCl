namespace SunamoCl.SunamoCmd.Tables;

public static class TableParser
{
    private static int[] GetMaxColumnsWidth(string[,] tableValues)
    {
        var maxColumnsWidth = new int[tableValues.GetLength(1)];
        for (var colIndex = 0; colIndex < tableValues.GetLength(1); colIndex++)
            for (var rowIndex = 0; rowIndex < tableValues.GetLength(0); rowIndex++)
            {
                var newLength = tableValues[rowIndex, colIndex].Length;
                var oldLength = maxColumnsWidth[colIndex];

                if (newLength > oldLength) maxColumnsWidth[colIndex] = newLength;
            }

        return maxColumnsWidth;
    }

    #region First approach

    public static string ToStringTable<T>(
        this List<T> values,
        List<string> columnHeaders,
        params Func<T, object>[] valueSelectors)
    {
        return values.ToStringTable(columnHeaders, valueSelectors);
    }

    public static string ToStringTable<T>(
        this T[] values,
        string[] columnHeaders,
        params Func<T, object>[] valueSelectors)
    {
        var tableValues = new string[values.Length + 1, valueSelectors.Length];

        // Fill headers
        for (var colIndex = 0; colIndex < tableValues.GetLength(1); colIndex++)
            tableValues[0, colIndex] = columnHeaders[colIndex];

        // Fill table rows
        for (var rowIndex = 1; rowIndex < tableValues.GetLength(0); rowIndex++)
            for (var colIndex = 0; colIndex < tableValues.GetLength(1); colIndex++)
                tableValues[rowIndex, colIndex] = valueSelectors[colIndex]
                    .Invoke(values[rowIndex - 1])?.ToString() ?? string.Empty;

        return tableValues.ToStringTable();
    }

    public static string ToStringTable(this string[,] tableValues)
    {
        var maxColumnsWidth = GetMaxColumnsWidth(tableValues);
        var headerSplitter = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);

        var stringBuilder = new StringBuilder();
        for (var rowIndex = 0; rowIndex < tableValues.GetLength(0); rowIndex++)
        {
            for (var colIndex = 0; colIndex < tableValues.GetLength(1); colIndex++)
            {
                // Print cell
                var cell = tableValues[rowIndex, colIndex];
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
                stringBuilder.AppendFormat(" |{0}| ", headerSplitter);
                stringBuilder.AppendLine();
            }
        }

        return stringBuilder.ToString();
    }

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
