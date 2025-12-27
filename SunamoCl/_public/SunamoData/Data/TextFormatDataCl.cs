namespace SunamoCl._public.SunamoData.Data;

public class TextFormatDataCl : List<CharFormatDataCl>
{
    public int RequiredLength = -1;
    public bool ShouldTrimBefore;


    public TextFormatDataCl(bool shouldTrimBefore, int requiredLength, params CharFormatDataCl[] a)
    {
        this.ShouldTrimBefore = shouldTrimBefore;
        this.RequiredLength = requiredLength;
        AddRange(a);
    }

    public static class Templates
    {
    }
}