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



    internal static string NullToStringOrDefault(object n)
    {
        return n == null ? " " + "(null)" : " " + n;
    }

    




    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWith(string value, string wrapper)
    {
        return wrapper + value + wrapper;
    }
}