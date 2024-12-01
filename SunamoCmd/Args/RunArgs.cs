namespace SunamoCl.SunamoCmd.Args;
public class RunArgs
{
    public Func<Task> RunInDebugAsync { get; set; }
    public Func<Dictionary<string, Func<Task<Dictionary<string, object>>>>> AddGroupOfActions { get; set; }
    public bool? AskUserIfRelease { get; set; }
    public bool LoadFromClipboard { get; set; }

    public String[] Args { get; set; }

    public bool CatchUnhandledException
    {
        get; set;
    }
    public bool IsDebug { get; set; }

    public ServiceCollection ServiceCollection
    {
        get; set;
    }
}