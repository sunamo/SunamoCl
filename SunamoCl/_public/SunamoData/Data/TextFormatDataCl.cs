namespace SunamoCl._public.SunamoData.Data;

/// <summary>
/// Represents the text format data cl type.
/// </summary>
public class TextFormatDataCl : List<CharFormatDataCl>
{
    /// <summary>
    /// Gets or sets the required length.
    /// </summary>
    public int RequiredLength { get; set; } = -1;
    /// <summary>
    /// Gets or sets the should trim before.
    /// </summary>
    public bool ShouldTrimBefore { get; set; }

    /// <summary>
    /// Text format data cl.
    /// </summary>
    /// <param name="shouldTrimBefore">The should trim before.</param>
    /// <param name="requiredLength">The required length.</param>
    /// <param name="charFormats">The char formats.</param>
    /// <returns>The text format data cl result.</returns>
    public TextFormatDataCl(bool shouldTrimBefore, int requiredLength, params CharFormatDataCl[] charFormats)
    {
        this.ShouldTrimBefore = shouldTrimBefore;
        this.RequiredLength = requiredLength;
        AddRange(charFormats);
    }
}
