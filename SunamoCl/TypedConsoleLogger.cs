namespace SunamoCl;

/// <summary>
/// Typed logger implementation that writes color-coded messages to the console based on message type
/// </summary>
public class TypedConsoleLogger : TypedLoggerBaseCl
{
    /// <summary>
    /// Singleton instance of the TypedConsoleLogger
    /// </summary>
    public static TypedConsoleLogger Instance = new();

    private TypedConsoleLogger() : base(CL.ChangeColorOfConsoleAndWrite)
    {
    }
}