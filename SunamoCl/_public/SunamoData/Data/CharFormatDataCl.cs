namespace SunamoCl._public.SunamoData.Data;

public class CharFormatDataCl
{
    public FromToCl FromTo { get; set; } = null!;


    public char[] MustBe { get; set; } = null!;


    public bool? Upper { get; set; } = false;

    public CharFormatDataCl(bool? upper, char[] mustBe)
    {
        this.Upper = upper;
        this.MustBe = mustBe;
    }

    public CharFormatDataCl()
    {
    }

    public static CharFormatDataCl GetOnlyNumbers(FromToCl requiredLength)
    {
        LetterAndDigitCharService letterAndDigitCharService = new LetterAndDigitCharService();

        var data = new CharFormatDataCl();
        data.FromTo = requiredLength;
        data.MustBe = letterAndDigitCharService.NumericChars.ToArray();
        return data;
    }


    public static CharFormatDataCl Get(bool? upper, FromToCl fromTo, params char[] mustBe)
    {
        var data = new CharFormatDataCl(upper, mustBe);
        data.FromTo = fromTo;
        return data;
    }



    public static class Templates
    {
        internal static readonly char NotNumberChar = (char)9;
        public static CharFormatDataCl Dash { get; set; } = Get(null, new FromToCl(1, 1), '-');
        public static CharFormatDataCl NotNumber { get; set; } = Get(null, new FromToCl(1, 1), NotNumberChar);


        public static CharFormatDataCl TwoLetterNumber { get; set; }
        public static CharFormatDataCl Any { get; set; }

        static Templates()
        {
            var requiredLength = new FromToCl(1, 2);
            TwoLetterNumber = GetOnlyNumbers(requiredLength);
            Any = Get(null, new FromToCl(0, int.MaxValue));
        }
    }
}