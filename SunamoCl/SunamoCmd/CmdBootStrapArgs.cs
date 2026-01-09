// variables names: ok
namespace SunamoCl.SunamoCmd;

public class CmdBootStrapArgs
{
    public Action InitSqlMeasureTime { get; set; } = null!;

    #region Cant be null


    public Action RunInDebug { get; set; } = null!;

    public Func<Dictionary<string, Action>> AddGroupOfActions { get; set; } = null!;

    public bool AskUserIfRelease { get; set; }

    #endregion
}