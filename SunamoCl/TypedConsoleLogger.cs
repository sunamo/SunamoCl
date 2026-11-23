namespace SunamoCl;

/// <summary>
/// Represents the typed console logger type.
/// </summary>
public class TypedConsoleLogger : TypedLoggerBaseCl
{
    /// <summary>
    /// Gets or sets the instance.
    /// </summary>
    public static TypedConsoleLogger Instance { get; set; } = new();

    private TypedConsoleLogger() : base(CL.ChangeColorOfConsoleAndWrite)
    {
    }
}
