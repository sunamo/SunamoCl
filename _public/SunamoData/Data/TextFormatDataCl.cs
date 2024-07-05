namespace SunamoCl._public.SunamoData.Data;







public class TextFormatDataCl : List<CharFormatDataCl>
{
    
    
    
    
    public int requiredLength = -1;
    public bool trimBefore = false;
    public static class Templates
    {
    }
    
    
    
    
    
    
    public TextFormatDataCl(bool trimBefore, int requiredLength, params CharFormatDataCl[] a)
    {
        this.trimBefore = trimBefore;
        this.requiredLength = requiredLength;
        AddRange(a);
    }
}