namespace SunamoCl._public;

internal class LetterAndDigitCharService
{
    internal List<char> AllCharsWithoutSpecial { get; set; } = null!;
    internal List<char> AllChars { get; set; } = null!;
    internal List<char> NumericChars { get; } =
        new(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' });
    internal List<char> LowerChars { get; } = new(new[]
    {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z'
    });
    internal List<char> UpperChars { get; } = new(new[]
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
        'W', 'X', 'Y', 'Z'
    });

    internal LetterAndDigitCharService()
    {
        AllCharsWithoutSpecial = new List<char>(LowerChars.Count + NumericChars.Count + UpperChars.Count);
        AllCharsWithoutSpecial.AddRange(LowerChars);
        AllCharsWithoutSpecial.AddRange(NumericChars);
        AllCharsWithoutSpecial.AddRange(UpperChars);
    }
}
