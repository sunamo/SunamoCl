namespace SunamoCl._public.SunamoData.Data;

/// <summary>
/// Represents format data for a single character position, defining constraints like allowed characters and case
/// </summary>
public class CharFormatDataCl
{
    /// <summary>
    /// Gets or sets the range constraint for the character position
    /// </summary>
    public FromToCl FromTo { get; set; } = null!;


    /// <summary>
    /// Gets or sets the array of allowed characters for this position
    /// </summary>
    public char[] MustBe { get; set; } = null!;


    /// <summary>
    /// Gets or sets whether the character must be uppercase (true), lowercase (false), or any case (null)
    /// </summary>
    public bool? Upper { get; set; } = false;

    /// <summary>
    /// Initializes a new instance with the specified case and allowed characters
    /// </summary>
    /// <param name="upper">Whether the character must be uppercase</param>
    /// <param name="mustBe">Array of allowed characters</param>
    public CharFormatDataCl(bool? upper, char[] mustBe)
    {
        this.Upper = upper;
        this.MustBe = mustBe;
    }

    /// <summary>
    /// Initializes a new instance with default values
    /// </summary>
    public CharFormatDataCl()
    {
    }

    /// <summary>
    /// Creates a CharFormatDataCl configured to accept only numeric characters within the specified length range
    /// </summary>
    /// <param name="requiredLength">Range constraint for the number of numeric characters</param>
    /// <returns>A configured CharFormatDataCl for numeric-only input</returns>
    public static CharFormatDataCl GetOnlyNumbers(FromToCl requiredLength)
    {
        LetterAndDigitCharService letterAndDigitCharService = new LetterAndDigitCharService();

        var data = new CharFormatDataCl();
        data.FromTo = requiredLength;
        data.MustBe = letterAndDigitCharService.NumericChars.ToArray();
        return data;
    }


    /// <summary>
    /// Creates a CharFormatDataCl with the specified case, range, and allowed characters
    /// </summary>
    /// <param name="upper">Whether the character must be uppercase</param>
    /// <param name="fromTo">Range constraint for the character position</param>
    /// <param name="mustBe">Array of allowed characters</param>
    /// <returns>A configured CharFormatDataCl instance</returns>
    public static CharFormatDataCl Get(bool? upper, FromToCl fromTo, params char[] mustBe)
    {
        var data = new CharFormatDataCl(upper, mustBe);
        data.FromTo = fromTo;
        return data;
    }



    /// <summary>
    /// Contains predefined CharFormatDataCl templates for common character format patterns
    /// </summary>
    public static class Templates
    {
        internal static readonly char NotNumberChar = (char)9;

        /// <summary>
        /// Template matching a single dash character
        /// </summary>
        public static CharFormatDataCl Dash { get; set; } = Get(null, new FromToCl(1, 1), '-');

        /// <summary>
        /// Template matching a single non-number character
        /// </summary>
        public static CharFormatDataCl NotNumber { get; set; } = Get(null, new FromToCl(1, 1), NotNumberChar);


        /// <summary>
        /// Template matching a one or two digit number
        /// </summary>
        public static CharFormatDataCl TwoLetterNumber { get; set; }

        /// <summary>
        /// Template matching any number of any characters
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