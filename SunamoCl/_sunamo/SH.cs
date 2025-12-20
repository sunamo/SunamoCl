// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoCl._sunamo;

internal class SH
{
    internal static bool HasTextRightFormat(string r, TextFormatDataCl tfd)
    {
        if (tfd.TrimBefore) r = r.Trim();
        long tfdOverallLength = 0;
        foreach (var item in tfd) tfdOverallLength += item.FromTo.to - item.FromTo.from + 1;
        var partsCount = tfd.Count;
        var actualCharFormatData = 0;
        var actualFormatData = tfd[actualCharFormatData];
        var followingFormatData = tfd[actualCharFormatData + 1];
        //int charCount = r.Length;
        //if (tfd.requiredLength != -1)
        //{
        //    if (r.Length != tfd.requiredLength)
        //    {
        //        return false;
        //    }
        //    charCount = Math.Min(r.Length, tfd.requiredLength);
        //}
        var actualChar = 0;
        var processed = 0;
        var from = actualFormatData.FromTo.FromL;
        var remains = actualFormatData.FromTo.ToL;
        var tfdCountM1 = tfd.Count - 1;
        while (true)
        {
            var canBeAnyChar =
                actualFormatData.MustBe == null ||
                actualFormatData.MustBe.Length == 0; //SunamoCollectionsShared.CA.IsEmptyOrNull();
            var isRightChar = false;
            if (canBeAnyChar)
            {
                isRightChar = true;
                remains--;
            }
            else
            {
                if (r.Length <= actualChar) return false;
                isRightChar = actualFormatData.MustBe.Any(d => d == r[actualChar]); //CAG.IsEqualToAnyElement<char>(, );
                if (isRightChar && !canBeAnyChar)
                {
                    actualChar++;
                    processed++;
                    remains--;
                }
            }
            if (!isRightChar)
            {
                if (r.Length <= actualChar) return false;
                isRightChar =
                    followingFormatData.MustBe.Any(d => d == r[actualChar]); //CAG.IsEqualToAnyElement<char>(, );
                if (!isRightChar) return false;
                if (remains != 0 && processed < from) return false;
                if (isRightChar && !canBeAnyChar)
                {
                    actualCharFormatData++;
                    processed++;
                    actualChar++;
                    if (!CA.HasIndex(actualCharFormatData, tfd) && r.Length > actualChar) return false;
                    actualFormatData = tfd[actualCharFormatData];
                    if (CA.HasIndex(actualCharFormatData + 1, tfd))
                        followingFormatData = tfd[actualCharFormatData + 1];
                    else
                        followingFormatData = CharFormatDataCl.Templates.Any;
                    processed = 0;
                    remains = actualFormatData.FromTo.to;
                    remains--;
                }
            }
            if (actualChar == tfdOverallLength)
                if (actualChar == r.Length)
                    //break;
                    return true;
            if (remains == 0)
            {
                ++actualCharFormatData;
                if (!CA.HasIndex(actualCharFormatData, tfd) && r.Length > actualChar) return false;
                actualFormatData = tfd[actualCharFormatData];
                if (CA.HasIndex(actualCharFormatData + 1, tfd))
                    followingFormatData = tfd[actualCharFormatData + 1];
                else
                    followingFormatData = CharFormatDataCl.Templates.Any;
                processed = 0;
                remains = actualFormatData.FromTo.to;
            }
        }
    }

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