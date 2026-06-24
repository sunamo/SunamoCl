namespace SunamoCl;

public class TypedConsoleLogger : TypedLoggerBaseCl
{
    public static TypedConsoleLogger Instance { get; set; } = new();

    private TypedConsoleLogger() : base(CL.ChangeColorOfConsoleAndWrite)
    {
    }
}
