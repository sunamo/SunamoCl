namespace SunamoCl._public.SunamoData.Data;

/// <summary>
/// Represents format data for text validation, containing a list of character format definitions and text-level constraints
/// </summary>
public class TextFormatDataCl : List<CharFormatDataCl>
{
    /// <summary>
    /// Gets or sets the required length of the text, or -1 if no length requirement
    /// </summary>
    public int RequiredLength { get; set; } = -1;
    /// <summary>
    /// Gets or sets whether the text should be trimmed before validation
    /// </summary>
    public bool ShouldTrimBefore { get; set; }


    /// <summary>
    /// Initializes a new instance with the specified settings and character format definitions
    /// </summary>
    /// <param name="shouldTrimBefore">Whether to trim text before validation</param>
    /// <param name="requiredLength">Required length of the text</param>
    /// <param name="charFormats">Character format definitions for each position</param>
    public TextFormatDataCl(bool shouldTrimBefore, int requiredLength, params CharFormatDataCl[] charFormats)
    {
        this.ShouldTrimBefore = shouldTrimBefore;
        this.RequiredLength = requiredLength;
        AddRange(charFormats);
    }

    /// <summary>
    /// Contains predefined TextFormatDataCl templates for common text format patterns
    /// </summary>
    public static class Templates
    {
    }
}