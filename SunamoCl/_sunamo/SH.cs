namespace SunamoCl._sunamo;

internal class SH
{
    internal static bool HasTextRightFormat(string text, TextFormatDataCl textFormat)
    {
        if (textFormat.ShouldTrimBefore) text = text.Trim();
        long textFormatDataOverallLength = 0;
        foreach (var item in textFormat) textFormatDataOverallLength += item.FromTo.To - item.FromTo.From + 1;
        var partsCount = textFormat.Count;
        var actualCharFormatData = 0;
        var actualFormatData = textFormat[actualCharFormatData];
        var followingFormatData = textFormat[actualCharFormatData + 1];
        //int charCount = text.Length;
        //if (textFormat.requiredLength != -1)
        //{
        //    if (text.Length != textFormat.requiredLength)
        //    {
        //        return false;
        //    }
        //    charCount = Math.Min(text.Length, textFormat.requiredLength);
        //}
        var actualChar = 0;
        var processed = 0;
        var from = actualFormatData.FromTo.FromL;
        var remains = actualFormatData.FromTo.ToL;
        var textFormatCountMinusOne = textFormat.Count - 1;
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
                if (text.Length <= actualChar) return false;
                isRightChar = actualFormatData.MustBe?.Any(character => character == text[actualChar]) ?? false; //CAG.IsEqualToAnyElement<char>(, );
                if (isRightChar && !canBeAnyChar)
                {
                    actualChar++;
                    processed++;
                    remains--;
                }
            }
            if (!isRightChar)
            {
                if (text.Length <= actualChar) return false;
                isRightChar =
                    followingFormatData.MustBe?.Any(character => character == text[actualChar]) ?? false; //CAG.IsEqualToAnyElement<char>(, );
                if (!isRightChar) return false;
                if (remains != 0 && processed < from) return false;
                if (isRightChar && !canBeAnyChar)
                {
                    actualCharFormatData++;
                    processed++;
                    actualChar++;
                    if (!CA.HasIndex(actualCharFormatData, textFormat) && text.Length > actualChar) return false;
                    actualFormatData = textFormat[actualCharFormatData];
                    if (CA.HasIndex(actualCharFormatData + 1, textFormat))
                        followingFormatData = textFormat[actualCharFormatData + 1];
                    else
                        followingFormatData = CharFormatDataCl.Templates.Any;
                    processed = 0;
                    remains = actualFormatData.FromTo.To;
                    remains--;
                }
            }
            if (actualChar == textFormatDataOverallLength)
                if (actualChar == text.Length)
                    //break;
                    return true;
            if (remains == 0)
            {
                ++actualCharFormatData;
                if (!CA.HasIndex(actualCharFormatData, textFormat) && text.Length > actualChar) return false;
                actualFormatData = textFormat[actualCharFormatData];
                if (CA.HasIndex(actualCharFormatData + 1, textFormat))
                    followingFormatData = textFormat[actualCharFormatData + 1];
                else
                    followingFormatData = CharFormatDataCl.Templates.Any;
                processed = 0;
                remains = actualFormatData.FromTo.To;
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
    /// <param name="isEnoughPartialContainsOfSplitted"></param>
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



    internal static string NullToStringOrDefault(object value)
    {
        return value == null ? " " + "(null)" : " " + value;
    }






    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWith(string value, string wrapper)
    {
        return wrapper + value + wrapper;
    }
}