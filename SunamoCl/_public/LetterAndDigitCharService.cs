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
    /// List of numeric digit characters (0-9).
    /// </summary>
    internal readonly List<char> NumericChars =
        new(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' });
    /// <summary>
    /// List of lowercase Latin characters (a-z).
    /// </summary>
    internal readonly List<char> LowerChars = new(new[]
    {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z'
    });
    /// <summary>
    /// List of uppercase Latin characters (A-Z).
    /// </summary>
    internal readonly List<char> UpperChars = new(new[]
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