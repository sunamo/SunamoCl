namespace SunamoCl.SunamoCmd.Tables;

/// <summary>
///     LIke a idiot I have developed this from https://stackoverflow.com/a/856918/9327173
/// </summary>
public class CmdTable
{
    private static readonly int _tableWidth = 73;

    public static void CmdTable2(List<string> headers, List<List<string>> rows)
    {
        var firstRow = rows.First();

        var max = new List<int>(firstRow.Count);

        CL.Clear();
        PrintLine();

        for (var i = 0; i < rows.Count(); i++)
        for (var columnIndex = 0; columnIndex < firstRow.Count; columnIndex++)
        {
            var list = rows[i];
            var length = list[columnIndex].Length;
            max.Add(Math.Max(max[columnIndex], length));
        }

        var header = AbSet(max, headers);

        PrintRow(header);

        PrintLine();

        for (var i = 0; i < rows.Count; i++)
        {
            header = AbSet(max, rows[i]);
            PrintRow(header);
        }


        PrintLine();
    }

    private static List<AB> AbSet(List<int> columnWidths, List<string> columnTexts)
    {
        var ab = new List<AB>();

        for (var i = 0; i < columnWidths.Count; i++) ab.Add(AB.Get(columnTexts[i], columnWidths[i]));
        return ab;
    }

    private static void PrintLine()
    {
        CL.WriteLine(new string('-', _tableWidth));
    }

    private static void PrintRow(List<AB> columns)
    {
        var width = (_tableWidth - columns.Count) / columns.Count;
        var row = "|";

        foreach (var column in columns) row += AlignCentre(column.A, (int)column.B) + "|";

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