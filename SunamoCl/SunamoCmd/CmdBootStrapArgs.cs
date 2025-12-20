// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoCl.SunamoCmd;

public class CmdBootStrapArgs
{
    public Action InitSqlMeasureTime;

    #region Cant be null


    public Action RunInDebug;

    public Func<Dictionary<string, Action>> AddGroupOfActions;

    // je zároveň definovaný i v SunamoCmdArgs.Cmd. Zde NSN => commented
    //public Dictionary<string, Action> allActions;
    public bool AskUserIfRelease;

    #endregion
}