namespace SunamoCl.SunamoCmd;

public class CmdBootStrapArgs
{
    public Action InitSqlMeasureTime;

    #region Cant be null


    public Action runInDebug;

    public Func<Dictionary<string, Action>> AddGroupOfActions;

    // je zároveň definovaný i v SunamoCmdArgs.Cmd. Zde NSN => commented 
    //public Dictionary<string, Action> allActions;
    public bool askUserIfRelease;

    #endregion
}