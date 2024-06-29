namespace SunamoCl;


/// <summary>
/// Udává jak musí být vstupní text zformátovaný
/// </summary>
public class CharFormatDataCl
{
    /// <summary>
    /// Null = no matter
    /// Nejvhodnější je zde výčet Windows.UI.Text.LetterCase
    /// </summary>
    internal bool? upper = false;
    /// <summary>
    /// Nemusí mít žádný prvek, pak může být znak libovolný
    /// </summary>
    internal char[] mustBe = null;
    internal static class Templates
    {
        internal static CharFormatDataCl dash = Get(null, new FromToCl(1, 1), AllChars.dash);
        internal static CharFormatDataCl notNumber = Get(null, new FromToCl(1, 1), AllChars.notNumber);
        /// <summary>
        /// When doesn't contains fixed, is from 0 to number
        /// </summary>
        internal static CharFormatDataCl twoLetterNumber;
        static Templates()
        {
            FromToCl requiredLength = new FromToCl(1, 2);
            twoLetterNumber = GetOnlyNumbers(requiredLength);
            Any = Get(null, new FromToCl(0, int.MaxValue));
        }
        internal static CharFormatDataCl Any;
    }
    internal FromToCl fromTo = null;
    internal CharFormatDataCl(bool? upper, char[] mustBe)
    {
        this.upper = upper;
        this.mustBe = mustBe;
    }
    internal CharFormatDataCl()
    {
    }
    internal static CharFormatDataCl GetOnlyNumbers(FromToCl requiredLength)
    {
        CharFormatDataCl data = new CharFormatDataCl();
        data.fromTo = requiredLength;
        data.mustBe = AllChars.numericChars.ToArray();
        return data;
    }
    /// <summary>
    /// A1 Null = no matter
    ///
    /// </summary>
    /// <param name="upper"></param>
    /// <param name="fromTo"></param>
    /// <param name="mustBe"></param>
    internal static CharFormatDataCl Get(bool? upper, FromToCl fromTo, params char[] mustBe)
    {
        CharFormatDataCl data = new CharFormatDataCl(upper, mustBe);
        data.fromTo = fromTo;
        return data;
    }
}