namespace SunamoCl._sunamo;

/// <summary>
/// Calculates progress percentage. Typically DonePartially() is called repeatedly.
/// </summary>
internal class PercentCalculator
{
    private readonly double hundredPercent = 100d;

    /// <summary>
    /// Gets or sets the value representing one percent of the overall sum.
    /// </summary>
    internal double OnePercent { get; set; }

    /// <summary>
    /// Initializes a new instance with the specified overall sum.
    /// </summary>
    /// <param name="overallSum">Total value representing 100%.</param>
    internal PercentCalculator(double overallSum)
    {
        if (overallSum == 0) ThrowEx.DivideByZero();
        OnePercent = hundredPercent / overallSum;
        OverallSum = overallSum;
    }

    /// <summary>
    /// Gets or sets the last calculated percentage value.
    /// </summary>
    internal double Last { get; set; }
    /// <summary>
    /// Gets or sets the overall sum representing 100%.
    /// </summary>
    internal double OverallSum { get; set; }
}