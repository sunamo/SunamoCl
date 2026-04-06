namespace SunamoCl._public;

/// <summary>
/// Provides lists of numeric, lowercase, and uppercase characters for validation purposes.
/// </summary>
internal class LetterAndDigitCharService
{
    /// <summary>
    /// All alphanumeric characters without special characters.
    /// </summary>
    internal List<char> AllCharsWithoutSpecial { get; set; } = null!;
    /// <summary>
    /// All characters including special characters.
    /// </summary>
    internal List<char> AllChars { get; set; } = null!;
    /// <summary>
    /// Gets the list of numeric digit characters (0-9).
    /// </summary>
    internal List<char> NumericChars { get; } =
        new(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' });
    /// <summary>
    /// Gets the list of lowercase Latin characters (a-z).
    /// </summary>
    internal List<char> LowerChars { get; } = new(new[]
    {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z'
    });
    /// <summary>
    /// Gets the list of uppercase Latin characters (A-Z).
    /// </summary>
    internal List<char> UpperChars { get; } = new(new[]
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
        'W', 'X', 'Y', 'Z'
    });

    /// <summary>
    /// Initializes a new instance and populates the combined character list.
    /// </summary>
    internal LetterAndDigitCharService()
    {
        AllCharsWithoutSpecial = new List<char>(LowerChars.Count + NumericChars.Count + UpperChars.Count);
        AllCharsWithoutSpecial.AddRange(LowerChars);
        AllCharsWithoutSpecial.AddRange(NumericChars);
        AllCharsWithoutSpecial.AddRange(UpperChars);
    }
}