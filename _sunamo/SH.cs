namespace SunamoCl._sunamo;

internal class SH
{
    /// <summary>
    /// Pojmenovaná takto protože prvně jsem tuto metodu napsal pro SunamoCl, abych nemusel kopírovat mraky metod a enumů ze SunamoString
    /// </summary>
    /// <param name="input"></param>
    /// <param name="term"></param>
    /// <param name="searchStrategy"></param>
    /// <param name="caseSensitive"></param>
    /// <returns></returns>
    internal static bool ContainsCl(string input, string term, SearchStrategy searchStrategy = SearchStrategy.FixedSpace, bool caseSensitive = false, bool isEnoughPartialContainsOfSplitted = true)
    {
        if (!caseSensitive)
        {
            input = input.ToLower();
            term = term.ToLower();
        }
        // musel bych dotáhnout min 2 metody a další enumy
        if (searchStrategy == SearchStrategy.ExactlyName)
        {
            return input == term;
        }
        if (searchStrategy == SearchStrategy.AnySpaces)
        {
            var nonLetterNumberChars = input.Where(ch => !char.IsLetterOrDigit(ch)).ToList();
            nonLetterNumberChars.AddRange(term.Where(ch => !char.IsLetterOrDigit(ch)));
            nonLetterNumberChars = nonLetterNumberChars.Distinct().ToList();
            var nonLetterNumberCharsArray = nonLetterNumberChars.ToArray();
            var pInput = input.Split(nonLetterNumberCharsArray, StringSplitOptions.RemoveEmptyEntries);
            var pTerm = term.Split(nonLetterNumberCharsArray, StringSplitOptions.RemoveEmptyEntries);
            if (isEnoughPartialContainsOfSplitted)
            {
                foreach (var item in pTerm)
                {
                    if (!input.Contains(item))
                    {
                        return false;
                    }
                }
                return true;
            }
            bool containsAll = true;
            foreach (var item in pTerm)
            {
                if (!pInput.Contains(item))
                {
                    containsAll = false;
                    break;
                }
            }
            return containsAll;
        }
        return input.Contains(term);
    }

    /// <summary>
    ///     Convert \r\n to NewLine etc.
    /// </summary>
    /// <param name="delimiter"></param>
    internal static string ConvertTypedWhitespaceToString(string delimiter)
    {
        const string nl = @"
";
        switch (delimiter)
        {
            // must use \r\n, not Environment.NewLine (is not constant)
            case "\\r\\n":
            case "\\n":
            case "\\r":
                return nl;

            case "\\t":
                return "\t";
        }
        return delimiter;
    }

    internal static string FirstCharLower(string nazevPP)
    {
        if (nazevPP.Length < 2) return nazevPP;
        var sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToLower() + sb;
    }

    internal static string JoinNL(List<string> l)
    {
        StringBuilder sb = new();
        foreach (var item in l) sb.AppendLine(item);
        var r = string.Empty;
        r = sb.ToString();
        return r;
    }

    internal static string NullToStringOrDefault(object n)
    {
        return n == null ? " " + "(null)" : " " + n;
    }

    /// <summary>
    ///     Usage: BadFormatOfElementInList
    ///     If null, return "(null)"
    ///     nemůžu odstranit z sunamo, i tam se používá.
    /// </summary>
    /// <param name="n"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    internal static string NullToStringOrDefault(object n, string v)
    {
        return n == null ? " " + "(null)" : " " + v.ToString();
    }

    internal static List<string> SplitCharMore(string s, params char[] dot)
    {
        return s.Split(dot, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitMore(string s, params string[] dot)
    {
        return s.Split(dot, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitNone(string text, params string[] deli)
    {
        return text.Split(deli, StringSplitOptions.None).ToList();
    }

    internal static string TrimEnd(string name, string ext)
    {
        while (name.EndsWith(ext)) return name.Substring(0, name.Length - ext.Length);
        return name;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWith(string value, string wrapper)
    {
        return wrapper + value + wrapper;
    }
}