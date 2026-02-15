namespace SunamoCl.Results;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
/// <summary>
/// Contains results from running the application with RunArgs, including the service provider and the name of the executed action
/// </summary>
public class RunWithRunArgsResults
{
    /// <summary>
    /// Gets or sets the service provider built from the service collection
    /// </summary>
    public required ServiceProvider ServiceProvider { get; set; }
    /// <summary>
    /// Gets or sets the name of the action that was executed
    /// </summary>
    public required string Runned { get; set; }
    /// <summary>
    /// Deconstructs the result into its components
    /// </summary>
    /// <param name="serviceProvider">The service provider</param>
    /// <param name="runned">The name of the executed action</param>
    public void Deconstruct(out ServiceProvider serviceProvider, out string runned)
    {
        serviceProvider = ServiceProvider;
        runned = Runned;
    }
}