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
        LetterAndDigitCharService letterAndDigitCharService = new LetterAndDigitCharService();

        var data = new CharFormatDataCl();
        data.fromTo = requiredLength;
        data.mustBe = letterAndDigitCharService.numericChars.ToArray();
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
        internal static readonly char notNumberChar = (char)9;
        public static CharFormatDataCl dash = Get(null, new FromToCl(1, 1), '-');
        public static CharFormatDataCl notNumber = Get(null, new FromToCl(1, 1), notNumberChar);


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