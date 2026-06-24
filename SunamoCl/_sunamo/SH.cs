namespace SunamoCl._sunamo;

internal class SH
{
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

    // Simplified version to avoid pulling in many methods and enums from SunamoString.
    internal static bool ContainsCl(string input, string term, SearchStrategy searchStrategy = SearchStrategy.FixedSpace, bool isCaseSensitive = false, bool isPartialMatchSufficient = true)
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
            if (isPartialMatchSufficient)
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

    internal static string NullToStringOrDefault(object value)
    {
        return value == null ? " " + "(null)" : " " + value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWith(string text, string wrapper)
    {
        return wrapper + text + wrapper;
    }
}
