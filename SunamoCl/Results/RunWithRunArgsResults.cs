namespace SunamoCl.Results;

/// <summary>
/// Represents the run with run args results type.
/// </summary>
public class RunWithRunArgsResults
{
    /// <summary>
    /// Gets or sets the service provider.
    /// </summary>
    public required ServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// Gets or sets the executed action name.
    /// </summary>
    public required string ExecutedActionName { get; set; }

    /// <summary>
    /// Deconstruct.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="executedActionName">The executed action name.</param>
    /// <returns>The deconstruct result.</returns>
    public void Deconstruct(out ServiceProvider serviceProvider, out string executedActionName)
    {
        serviceProvider = ServiceProvider;
        executedActionName = ExecutedActionName;
    }
}
