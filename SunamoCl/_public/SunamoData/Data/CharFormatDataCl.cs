namespace SunamoCl._public.SunamoData.Data;

/// <summary>
/// Represents the char format data cl type.
/// </summary>
public class CharFormatDataCl
{
    /// <summary>
    /// Gets or sets the from to.
    /// </summary>
    public FromToCl FromTo { get; set; } = null!;

    /// <summary>
    /// Gets or sets the must be.
    /// </summary>
    public char[] MustBe { get; set; } = null!;

    /// <summary>
    /// Gets or sets the upper.
    /// </summary>
    public bool? Upper { get; set; } = false;

    /// <summary>
    /// Char format data cl.
    /// </summary>
    /// <param name="upper">The upper.</param>
    /// <param name="mustBe">The must be.</param>
    /// <returns>The char format data cl result.</returns>
    public CharFormatDataCl(bool? upper, char[] mustBe)
    {
        this.Upper = upper;
        this.MustBe = mustBe;
    }

    /// <summary>
    /// Char format data cl.
    /// </summary>
    /// <param name="upper">The upper.</param>
    /// <param name="mustBe">The must be.</param>
    /// <returns>The char format data cl result.</returns>
    public CharFormatDataCl()
    {
    }

    /// <summary>
    /// Get only numbers.
    /// </summary>
    /// <returns>The get only numbers result.</returns>
    public static CharFormatDataCl GetOnlyNumbers(FromToCl requiredLength)
    {
        var letterAndDigitCharService = new LetterAndDigitCharService();

        var data = new CharFormatDataCl();
        data.FromTo = requiredLength;
        data.MustBe = letterAndDigitCharService.NumericChars.ToArray();
        return data;
    }

    /// <summary>
    /// Get.
    /// </summary>
    /// <returns>The get result.</returns>
    public static CharFormatDataCl Get(bool? upper, FromToCl fromTo, params char[] mustBe)
    {
        var data = new CharFormatDataCl(upper, mustBe);
        data.FromTo = fromTo;
        return data;
    }

    /// <summary>
    /// Represents the templates type.
    /// </summary>
    public static class Templates
    {
        internal static readonly char NotNumberChar = (char)9;

        /// <summary>
        /// Gets or sets the dash.
        /// </summary>
        public static CharFormatDataCl Dash { get; set; } = Get(null, new FromToCl(1, 1), '-');

        /// <summary>
        /// Gets or sets the not number.
        /// </summary>
        public static CharFormatDataCl NotNumber { get; set; } = Get(null, new FromToCl(1, 1), NotNumberChar);

        /// <summary>
        /// Gets or sets the two letter number.
        /// </summary>
        public static CharFormatDataCl TwoLetterNumber { get; set; }

        /// <summary>
        /// Gets or sets the any.
        /// </summary>
        public static CharFormatDataCl Any { get; set; }

        static Templates()
        {
            var requiredLength = new FromToCl(1, 2);
            TwoLetterNumber = GetOnlyNumbers(requiredLength);
            Any = Get(null, new FromToCl(0, int.MaxValue));
        }
    }
}
