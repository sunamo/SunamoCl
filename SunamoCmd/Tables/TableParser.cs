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

    public static class First
    {
        //   #region First approach
        //   public static string ToStringTable<T>(
        //this IList<T> values,
        //string[] columnHeaders,
        //params Func<T, object>[] valueSelectors)
        //   {
        //       return ToStringTable(values.ToArray(), columnHeaders, valueSelectors);
        //   }

        //   public static string ToStringTable<T>(
        // this T[] values,
        // string[] columnHeaders,
        // params Func<T, object>[] valueSelectors)
        //   {
        //       Debug.Assert(columnHeaders.Length == valueSelectors.Length);

        //       var arrValues = new string[values.Length + 1, valueSelectors.Length];

        //       // Fill headers
        //       for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
        //       {
        //           arrValues[0, colIndex] = columnHeaders[colIndex];
        //       }

        //       // Fill table rows
        //       for (int rowIndex = 1; rowIndex < arrValues.GetLength(0); rowIndex++)
        //       {
        //           for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
        //           {
        //               arrValues[rowIndex, colIndex] = valueSelectors[colIndex]
        //                 .Invoke(values[rowIndex - 1]).ToString();
        //           }
        //       }

        //       return ToStringTable(arrValues);
        //   }

        //   public static string ToStringTable(this string[,] arrValues)
        //   {
        //       int[] maxColumnsWidth = GetMaxColumnsWidth(arrValues);
        //       var headerSpliter = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);

        //       var sb = new StringBuilder();
        //       for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
        //       {
        //           for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
        //           {
        //               // Print cell
        //               string cell = arrValues[rowIndex, colIndex];
        //               cell = cellSH.PadRight(maxColumnsWidth[colIndex]);
        //               sb.Append(" | ");
        //               sb.Append(cell);
        //           }

        //           // Print end of line
        //           sb.Append(" | ");
        //           sb.AppendLine();

        //           // Print splitter
        //           if (rowIndex == 0)
        //           {
        //               sb.AppendFormat(" |{0}| ", headerSpliter);
        //               sb.AppendLine();
        //           }
        //       }

        //       return sb.ToString();
        //   }
        //   #endregion
    }

    public static class Second
    {
        //    #region Second approach
        //    public static string ToStringTable<T>(
        //this IList<T> values,
        //params Expression<Func<T, object>>[] valueSelectors)
        //    {
        //        var headers = valueSelectors.Select(func => GetProperty(func).Name).ToArray();
        //        var selectors = valueSelectors.Select(exp => exp.Compile()).ToArray();
        //        return ToStringTable(values, headers, selectors);
        //    }

        //    private static PropertyInfo GetProperty<T>(Expression<Func<T, object>> expresstion)
        //    {
        //        if (expresstion.Body is UnaryExpression)
        //        {
        //            if ((expresstion.Body as UnaryExpression).Operand is MemberExpression)
        //            {
        //                return ((expresstion.Body as UnaryExpression).Operand as MemberExpression).Member as PropertyInfo;
        //            }
        //        }

        //        if ((expresstion.Body is MemberExpression))
        //        {
        //            return (expresstion.Body as MemberExpression).Member as PropertyInfo;
        //        }
        //        return null;
        //    }
        //    #endregion
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
        //Debug.Assert(columnHeaders.Length == valueSelectors.Length);

        var arrValues = new string[values.Length + 1, valueSelectors.Length];

        // Fill headers
        for (var colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            arrValues[0, colIndex] = columnHeaders[colIndex];

        // Fill table rows
        for (var rowIndex = 1; rowIndex < arrValues.GetLength(0); rowIndex++)
            for (var colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                arrValues[rowIndex, colIndex] = valueSelectors[colIndex]
                    .Invoke(values[rowIndex - 1]).ToString();

        return arrValues.ToStringTable();
    }

    public static string ToStringTable(this string[,] arrValues)
    {
        var maxColumnsWidth = GetMaxColumnsWidth(arrValues);
        var headerSpliter = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);

        var sb = new StringBuilder();
        for (var rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
        {
            for (var colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                // Print cell
                var cell = arrValues[rowIndex, colIndex];
                cell = cell.PadRight(maxColumnsWidth[colIndex]);
                sb.Append(" | ");
                sb.Append(cell);
            }

            // Print end of line
            sb.Append(" | ");
            sb.AppendLine();

            // Print splitter
            if (rowIndex == 0)
            {
                sb.AppendFormat(" |{0}| ", headerSpliter);
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }

    public static string ToStringTable(List<string> headers, IList<List<string>> last)
    {
        var f = last.First();
        var s = new List<string>(f.Count * last.Count() + f.Count);

        s.AddRange(headers);
        foreach (var item in last) s.AddRange(item);

        string[,] od = CA.OneDimensionArrayToTwoDirection(s.ToArray(), f.Count);

        return od.ToStringTable();
    }

    #endregion
}