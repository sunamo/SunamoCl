

namespace SunamoCl;

public class RunArgs
{
    public AIInitArgsCl aiInitArgs;
    public string appName; public Func<IClipboardHelperCl> createInstanceClipboardHelper; public
#if ASYNC
Func<Task>
#else
Action
#endif 
        runInDebug; public Func<Dictionary<string, Func<Task>>> AddGroupOfActions; public Dictionary<string, Action> pAllActions; public bool? askUserIfRelease; public Action InitSqlMeasureTime; public Action customInit; public Action assingSearchInAll; public Action applyCryptData; public Action assignJsSerialization; public string[] args; public Action psInit; public Dictionary<string, object> groupsOfActionsFromProgramCommon; public Action javascriptSerializationInitUtf8json; public string eventLogNameFromEventLogNames; public Func</*IDatabasesConnections*/ object> dbConns; public Action<ICryptCl> rijndaelBytesInit; public ICryptCl cryptDataWrapperRijn;
    public CreateAppDirsIfDontExistsArgsCl /*(List<string> keysCommonSettings, List<string> keysSettingsList, List<string> keysSettingsBool, List<string> keysSettingsOther)*/ createAppDirsIfDontExistsArgs;

    public Dictionary<string, Func<Task>> pAllActionsAsync;
    public bool isNotUt = true;
    public Func<Func<char, bool>> BitLockerHelperInit;
    public bool IsDebug;
    public Func<Func<string, string, string>, Task> ProgramSharedCreatePathToFiles;
    public Func<string, string, string> AppDataCiGetFileString;
    public Func<IPercentCalculator> createPercentCalculator;

    public Action<string> ThisApp_SetName;
    public Action<CreateAppDirsIfDontExistsArgsCl> AppData_CreateAppDirsIfDontExists;
}
