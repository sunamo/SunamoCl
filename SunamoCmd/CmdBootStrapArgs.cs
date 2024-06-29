namespace SunamoCl;

public class CmdBootStrapArgs
{
    #region Cant be null
    public string appName;
    public IClipboardHelperCl clipboardHelperWin;
    public Action runInDebug;
    public Func<Dictionary<string, Action>> AddGroupOfActions;
    // je zároveň definovaný i v SunamoCmdArgs.Cmd. Zde NSN => commented 
    //public Dictionary<string, Action> allActions;
    public bool askUserIfRelease;
    #endregion

    public Action InitSqlMeasureTime;
}
