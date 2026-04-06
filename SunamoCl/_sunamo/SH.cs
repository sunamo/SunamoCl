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
        long overallLength = 0;
        foreach (var item in textFormat) overallLength += item.FromTo.To - item.FromTo.From + 1;
        var currentFormatIndex = 0;
        var currentFormat = textFormat[currentFormatIndex];
        var nextFormat = textFormat[currentFormatIndex + 1];
        var currentCharIndex = 0;
        var processed = 0;
        var minimumLength = currentFormat.FromTo.FromAsLong;
        var remainingCount = currentFormat.FromTo.ToAsLong;
        while (true)
        {
            var canBeAnyChar =
                currentFormat.MustBe == null ||
                currentFormat.MustBe.Length == 0;
            var isRightChar = false;
            if (canBeAnyChar)
            {
                isRightChar = true;
                remainingCount--;
            }
            else
            {
                if (text.Length <= currentCharIndex) return false;
                isRightChar = currentFormat.MustBe?.Any(character => character == text[currentCharIndex]) ?? false;
                if (isRightChar && !canBeAnyChar)
                {
                    currentCharIndex++;
                    processed++;
                    remainingCount--;
                }
            }
            if (!isRightChar)
            {
                if (text.Length <= currentCharIndex) return false;
                isRightChar =
                    nextFormat.MustBe?.Any(character => character == text[currentCharIndex]) ?? false;
                if (!isRightChar) return false;
                if (remainingCount != 0 && processed < minimumLength) return false;
                if (isRightChar && !canBeAnyChar)
                {
                    currentFormatIndex++;
                    processed++;
                    currentCharIndex++;
                    if (!CA.HasIndex(currentFormatIndex, textFormat) && text.Length > currentCharIndex) return false;
                    currentFormat = textFormat[currentFormatIndex];
                    if (CA.HasIndex(currentFormatIndex + 1, textFormat))
                        nextFormat = textFormat[currentFormatIndex + 1];
                    else
                        nextFormat = CharFormatDataCl.Templates.Any;
                    processed = 0;
                    remainingCount = currentFormat.FromTo.To;
                    remainingCount--;
                }
            }
            if (currentCharIndex == overallLength)
                if (currentCharIndex == text.Length)
                    return true;
            if (remainingCount == 0)
            {
                ++currentFormatIndex;
                if (!CA.HasIndex(currentFormatIndex, textFormat) && text.Length > currentCharIndex) return false;
                currentFormat = textFormat[currentFormatIndex];
                if (CA.HasIndex(currentFormatIndex + 1, textFormat))
                    nextFormat = textFormat[currentFormatIndex + 1];
                else
                    nextFormat = CharFormatDataCl.Templates.Any;
                processed = 0;
                remainingCount = currentFormat.FromTo.To;
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
        const string newLine = @"
";
        switch (delimiter)
        {
            case "\\r\\n":
            case "\\n":
            case "\\r":
                return newLine;

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