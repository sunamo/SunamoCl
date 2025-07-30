namespace SunamoCl._sunamo;
/// <summary>
///     Normálně se volá 100x DonePartially()
/// </summary>
internal class PercentCalculator
{
    internal static Type type = typeof(PercentCalculator);
    private readonly double _hundredPercent = 100d;
    private int _sum;
    private int added;
    internal double onePercent;

    internal PercentCalculator(double overallSum)
    {
        if (overallSum == 0) ThrowEx.DivideByZero();
        onePercent = _hundredPercent / overallSum;
        _overallSum = overallSum;
    }

    internal double last { get; set; }
    internal double _overallSum { get; set; }



    
    
    }