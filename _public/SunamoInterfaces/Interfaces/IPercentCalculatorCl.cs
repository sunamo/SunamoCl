namespace SunamoCl._public.SunamoInterfaces.Interfaces;

public interface IPercentCalculatorCl
{
    double _overallSum { get; set; }
    double last { get; set; }
    IPercentCalculatorCl Create(double overallSum);
    void AddOnePercent();
    int PercentFor(double value, bool last);
    void ResetComputedSum();
}