namespace SunamoCl._sunamo;

/// <summary>
///     Normálně se volá 100x DonePartially()
/// </summary>
internal class PercentCalculator
{
    internal static Type Type = typeof(PercentCalculator);
    private readonly double _hundredPercent = 100d;
    private int _sum;
    private int _added;
    internal double OnePercent;

    internal PercentCalculator(double overallSum)
    {
        if (overallSum == 0) ThrowEx.DivideByZero();
        OnePercent = _hundredPercent / overallSum;
        _overallSum = overallSum;
    }

    internal double Last { get; set; }
    internal double _overallSum { get; set; }



    
    
    }