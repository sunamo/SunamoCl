namespace SunamoCl._sunamo;

/// <summary>
///     Normálně se volá 100x DonePartially()
/// </summary>
internal class PercentCalculator
{
    public static Type type = typeof(PercentCalculator);
    private readonly double _hundredPercent = 100d;
    private int _sum;
    private int added;
    public double onePercent;

    public PercentCalculator(double overallSum)
    {
        if (overallSum == 0) ThrowEx.DivideByZero();
        onePercent = _hundredPercent / overallSum;
        _overallSum = overallSum;
    }

    public double last { get; set; }
    public double _overallSum { get; set; }

    public PercentCalculator Create(double overallSum)
    {
        return new PercentCalculator(overallSum);
    }

    public void AddOnePercent()
    {
        added++;
        last += onePercent;
    }

    /// <summary>
    ///     Dont know when is AddOne more useful than AddOnePercent => private
    /// </summary>
    private void AddOne()
    {
        last += 1;
    }

    /// <summary>
    ///     Is automatically called with PercentFor with last
    /// </summary>
    public void ResetComputedSum()
    {
        _sum = 0;
        Func<string, short> d = short.Parse;
    }

    /// <summary>
    ///     Was used for generating text output with inBothCount, files1Count, files2Count
    /// </summary>
    /// <param name="value"></param>
    /// <param name="last"></param>
    /// <returns></returns>
    public int PercentFor(double value, bool last)
    {
        // cannot divide by zero
        if (_overallSum == 0) return 0;
        // value - 
        // 
        var quocient = value / _overallSum;
        var result = (int)(_hundredPercent * quocient);
        _sum += result;
        if (last)
        {
            var diff = _sum - 100;
            if (_sum != 0) result -= diff;
            ResetComputedSum();
        }
#if DEBUG
        if (result == -2147483648) Debugger.Break();
#endif
        return result;
    }
}