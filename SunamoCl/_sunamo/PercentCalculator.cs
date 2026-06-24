namespace SunamoCl._sunamo;

internal class PercentCalculator
{
    private readonly double hundredPercent = 100d;

    internal double OnePercent { get; set; }

    internal PercentCalculator(double overallSum)
    {
        if (overallSum == 0) ThrowEx.DivideByZero();
        OnePercent = hundredPercent / overallSum;
        OverallSum = overallSum;
    }

    internal double Last { get; set; }
    internal double OverallSum { get; set; }
}
