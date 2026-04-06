namespace SunamoCl._sunamo;

/// <summary>
/// String helper class with text format validation and string manipulation utilities.
/// </summary>
internal class SH
{
    /// <summary>
    /// Validates whether the text matches the specified text format definition.
    /// </summary>
    /// <param name="text">Text to validate.</param>
    /// <param name="textFormat">Format definition containing character constraints.</param>
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
    /// Checks whether the input contains the term using the specified search strategy.
    /// Simplified version to avoid pulling in many methods and enums from SunamoString.
    /// </summary>
    /// <param name="input">Text to search in.</param>
    /// <param name="term">Text to search for.</param>
    /// <param name="searchStrategy">Strategy for matching.</param>
    /// <param name="isCaseSensitive">Whether matching is case-sensitive.</param>
    /// <param name="isEnoughPartialContainsOfSplitted">Whether partial containment of split parts is sufficient.</param>
    internal static bool ContainsCl(string input, string term, SearchStrategy searchStrategy = SearchStrategy.FixedSpace, bool isCaseSensitive = false, bool isEnoughPartialContainsOfSplitted = true)
    {
        if (!isCaseSensitive)
        {
            input = input.ToLower();
            term = term.ToLower();
        }
        if (searchStrategy == SearchStrategy.ExactlyName)
        {
            return input == term;
        }
        if (searchStrategy == SearchStrategy.AnySpaces)
        {
            var nonLetterNumberChars = input.Where(character => !char.IsLetterOrDigit(character)).ToList();
            nonLetterNumberChars.AddRange(term.Where(character => !char.IsLetterOrDigit(character)));
            nonLetterNumberChars = nonLetterNumberChars.Distinct().ToList();
            var nonLetterNumberCharsArray = nonLetterNumberChars.ToArray();
            var inputParts = input.Split(nonLetterNumberCharsArray, StringSplitOptions.RemoveEmptyEntries);
            var termParts = term.Split(nonLetterNumberCharsArray, StringSplitOptions.RemoveEmptyEntries);
            if (isEnoughPartialContainsOfSplitted)
            {
                foreach (var item in termParts)
                {
                    if (!input.Contains(item))
                    {
                        return false;
                    }
                }
                return true;
            }
            bool containsAll = true;
            foreach (var item in termParts)
            {
                if (!inputParts.Contains(item))
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
    /// Converts typed whitespace escape sequences (\r\n, \n, \r, \t) to their actual string representations.
    /// </summary>
    /// <param name="delimiter">Escaped whitespace string to convert.</param>
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



    /// <summary>
    /// Returns "(null)" prefixed with space if the value is null, otherwise returns the value as string prefixed with space.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    internal static string NullToStringOrDefault(object value)
    {
        return value == null ? " " + "(null)" : " " + value;
    }






    /// <summary>
    /// Wraps the text with the specified wrapper string on both sides.
    /// </summary>
    /// <param name="text">Text to wrap.</param>
    /// <param name="wrapper">String to prepend and append.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWith(string text, string wrapper)
    {
        return wrapper + text + wrapper;
    }
}