namespace SunamoCl.SunamoCmd.Tables;

public class CmdTable
{
    private static readonly int tableWidth = 73;

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

    private static List<AB> AbSet(List<int> columnWidths, List<string> columnTexts)
    {
        var columnPairs = new List<AB>();

        for (var i = 0; i < columnWidths.Count; i++) columnPairs.Add(AB.Get(columnTexts[i], columnWidths[i]));
        return columnPairs;
    }

    private static void PrintLine()
    {
        CL.WriteLine(new string('-', tableWidth));
    }

    private static void PrintRow(List<AB> columns)
    {
        var width = (tableWidth - columns.Count) / columns.Count;
        var row = "|";

        foreach (var column in columns) row += AlignCentre(column.Key, (int)column.Value) + "|";

        CL.WriteLine(row);
    }

    private static string AlignCentre(string text, int width)
    {
        text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

        if (string.IsNullOrEmpty(text))
            return new string(' ', width);
        return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
    }
}
