namespace SunamoCl;


/// <summary>
/// Alternatives: FormatOfString - allow as many as is chars in every match
///
/// can check whether on position is expected char (letter, digit, etc.) but then not allow variable lenght of parsed
/// </summary>
public class TextFormatDataCl : List<CharFormatDataCl>
{
    /// <summary>
    /// Přesná požadovaná délka, nesmí být ani menší, ani větší
    /// Pokud je -1, text může mít jakoukoliv délku
    /// </summary>
    internal int requiredLength = -1;
    internal bool trimBefore = false;
    internal static class Templates
    {
    }
    /// <summary>
    /// Zadej do A2 -1 pokud text může mít jakoukoliv délku
    /// </summary>
    /// <param name="trimBefore"></param>
    /// <param name="requiredLength"></param>
    /// <param name="a"></param>
    internal TextFormatDataCl(bool trimBefore, int requiredLength, params CharFormatDataCl[] a)
    {
        this.trimBefore = trimBefore;
        this.requiredLength = requiredLength;
        AddRange(a);
    }
}