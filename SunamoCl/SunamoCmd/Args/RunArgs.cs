// variables names: ok
namespace SunamoCl.SunamoCmd.Args;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public class RunArgs
{
    public Func<Task>? RunInDebugAsync { get; set; }
    public Func<Dictionary<string, Func<Task<Dictionary<string, object>>>>>? AddGroupOfActions { get; set; }
    public bool? AskUserIfRelease { get; set; }
    public bool LoadFromClipboard { get; set; }

    public String[] Args { get; set; } = [];

    public bool CatchUnhandledException
    {
        get; set;
    }
    public bool IsDebug { get; set; }

    public IServiceCollection? ServiceCollection
    {
        get; set;
    }
}