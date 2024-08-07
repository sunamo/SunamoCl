namespace SunamoCl._public.SunamoData.Data;

public class CharFormatDataCl
{
    public FromToCl fromTo;


    public char[] mustBe;


    public bool? upper = false;

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
        var data = new CharFormatDataCl();
        data.fromTo = requiredLength;
        data.mustBe = AllChars.numericChars.ToArray();
        return data;
    }


    public static CharFormatDataCl Get(bool? upper, FromToCl fromTo, params char[] mustBe)
    {
        var data = new CharFormatDataCl(upper, mustBe);
        data.fromTo = fromTo;
        return data;
    }

    public static class Templates
    {
        public static CharFormatDataCl dash = Get(null, new FromToCl(1, 1), AllChars.dash);
        public static CharFormatDataCl notNumber = Get(null, new FromToCl(1, 1), AllChars.notNumber);


        public static CharFormatDataCl twoLetterNumber;
        public static CharFormatDataCl Any;

        static Templates()
        {
            var requiredLength = new FromToCl(1, 2);
            twoLetterNumber = GetOnlyNumbers(requiredLength);
            Any = Get(null, new FromToCl(0, int.MaxValue));
        }
    }
}