namespace SunamoCl._public.SunamoData.Data;

public class TextFormatDataCl : List<CharFormatDataCl>
{
    public int RequiredLength { get; set; } = -1;
    public bool ShouldTrimBefore { get; set; }


    public TextFormatDataCl(bool shouldTrimBefore, int requiredLength, params CharFormatDataCl[] charFormats)
    {
        this.ShouldTrimBefore = shouldTrimBefore;
        this.RequiredLength = requiredLength;
        AddRange(charFormats);
    }

    public static class Templates
    {
    }
}