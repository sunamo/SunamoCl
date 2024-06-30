namespace SunamoCl;





public class CharFormatDataCl
{
    
    
    
    
    public bool? upper = false;
    
    
    
    public char[] mustBe = null;
    public static class Templates
    {
        public static CharFormatDataCl dash = Get(null, new FromToCl(1, 1), AllChars.dash);
        public static CharFormatDataCl notNumber = Get(null, new FromToCl(1, 1), AllChars.notNumber);
        
        
        
        public static CharFormatDataCl twoLetterNumber;
        static Templates()
        {
            FromToCl requiredLength = new FromToCl(1, 2);
            twoLetterNumber = GetOnlyNumbers(requiredLength);
            Any = Get(null, new FromToCl(0, int.MaxValue));
        }
        public static CharFormatDataCl Any;
    }
    public FromToCl fromTo = null;
    public CharFormatDataCl(bool? upper, char[] mustBe)
    {
        this.upper = upper;
        this.mustBe = mustBe;
    }
    public CharFormatDataCl()
    {
    }
    public static CharFormatDataCl GetOnlyNumbers(FromToCl requiredLength)
    {
        CharFormatDataCl data = new CharFormatDataCl();
        data.fromTo = requiredLength;
        data.mustBe = AllChars.numericChars.ToArray();
        return data;
    }
    
    
    
    
    
    
    
    public static CharFormatDataCl Get(bool? upper, FromToCl fromTo, params char[] mustBe)
    {
        CharFormatDataCl data = new CharFormatDataCl(upper, mustBe);
        data.fromTo = fromTo;
        return data;
    }
}