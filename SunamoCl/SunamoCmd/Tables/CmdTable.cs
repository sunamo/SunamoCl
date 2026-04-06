namespace SunamoCl.SunamoCmd.Tables;

/// <summary>
/// Renders formatted tables in console output.
/// </summary>
public class CmdTable
{
    private static readonly int tableWidth = 73;

    /// <summary>
    /// Renders a formatted table with headers and data rows to the console
    /// </summary>
    /// <param name="headers">Column header names</param>
    /// <param name="rows">Rows of data to display</param>
    public static void CmdTable2(List<string> headers, List<List<string>> rows)
    {
        var firstRow = rows.First();

        var maxColumnWidths = new List<int>(firstRow.Count);

        CL.Clear();
        PrintLine();

        for (var i = 0; i < rows.Count(); i++)
        for (var columnIndex = 0; columnIndex < firstRow.Count; columnIndex++)
        {
            var row = rows[i];
            var length = row[columnIndex].Length;
            maxColumnWidths.Add(Math.Max(maxColumnWidths[columnIndex], length));
        }

        var headerPairs = AbSet(maxColumnWidths, headers);

        PrintRow(headerPairs);

        PrintLine();

        for (var i = 0; i < rows.Count; i++)
        {
            var rowPairs = AbSet(maxColumnWidths, rows[i]);
            PrintRow(rowPairs);
        }


        PrintLine();
    }

    /// <summary>
    /// Creates a list of AB pairs from column widths and texts.
    /// </summary>
    /// <param name="columnWidths">Width of each column.</param>
    /// <param name="columnTexts">Text content of each column.</param>
    private static List<AB> AbSet(List<int> columnWidths, List<string> columnTexts)
    {
        var columnPairs = new List<AB>();

        for (var i = 0; i < columnWidths.Count; i++) columnPairs.Add(AB.Get(columnTexts[i], columnWidths[i]));
        return columnPairs;
    }

    /// <summary>
    /// Prints a horizontal separator line.
    /// </summary>
    private static void PrintLine()
    {
        CL.WriteLine(new string('-', tableWidth));
    }

    /// <summary>
    /// Prints a single data row with column separators.
    /// </summary>
    /// <param name="columns">Column data with keys as text and values as widths.</param>
    private static void PrintRow(List<AB> columns)
    {
        var width = (tableWidth - columns.Count) / columns.Count;
        var row = "|";

        foreach (var column in columns) row += AlignCentre(column.Key, (int)column.Value) + "|";

        CL.WriteLine(row);
    }

    /// <summary>
    /// Aligns text to the center within the specified width.
    /// </summary>
    /// <param name="text">Text to align.</param>
    /// <param name="width">Total width of the column.</param>
    private static string AlignCentre(string text, int width)
    {
        text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

        if (string.IsNullOrEmpty(text))
            return new string(' ', width);
        return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
    }
}