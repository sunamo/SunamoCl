namespace SunamoCl.Results;

public class RunWithRunArgsResults
{
    public required ServiceProvider ServiceProvider { get; set; }

    public required string ExecutedActionName { get; set; }

    public void Deconstruct(out ServiceProvider serviceProvider, out string executedActionName)
    {
        serviceProvider = ServiceProvider;
        executedActionName = ExecutedActionName;
    }
}
