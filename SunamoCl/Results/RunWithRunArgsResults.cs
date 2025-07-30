namespace SunamoCl.Results;
public class RunWithRunArgsResults
{
    public ServiceProvider ServiceProvider { get; set; }
    public string Runned { get; set; }
    public void Deconstruct(out ServiceProvider serviceProvider, out string arg)
    {
        serviceProvider = ServiceProvider;
        arg = Runned;
    }
}