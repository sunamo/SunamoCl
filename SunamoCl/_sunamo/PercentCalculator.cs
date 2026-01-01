namespace SunamoCl._sunamo;

/// <summary>
///     Normálně se volá 100x DonePartially()
/// </summary>
internal class PercentCalculator
{
    internal static Type Type = typeof(PercentCalculator);
    private readonly double hundredPercent = 100d;
    internal double OnePercent;

    internal PercentCalculator(double overallSum)
    {
        if (overallSum == 0) ThrowEx.DivideByZero();
        OnePercent = hundredPercent / overallSum;
        OverallSum = overallSum;
    }

    internal double Last { get; set; }
    internal double OverallSum { get; set; }
    }