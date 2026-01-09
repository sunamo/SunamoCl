// variables names: ok
namespace SunamoCl.Results;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public class RunWithRunArgsResults
{
    public required ServiceProvider ServiceProvider { get; set; }
    public required string Runned { get; set; }
    public void Deconstruct(out ServiceProvider serviceProvider, out string runned)
    {
        serviceProvider = ServiceProvider;
        runned = Runned;
    }
}