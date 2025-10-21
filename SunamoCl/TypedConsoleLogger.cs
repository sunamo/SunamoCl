namespace SunamoCl;

public class TypedConsoleLogger : TypedLoggerBaseCl
{
    public static TypedConsoleLogger Instance = new();

    private TypedConsoleLogger() : base(CL.ChangeColorOfConsoleAndWrite)
    {
    }
}