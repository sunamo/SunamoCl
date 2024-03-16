
namespace SunamoCl.SunamoCmd;
using SunamoExceptions.OnlyInSE;
using SunamoPlatformUwpInterop.Args;

public class CmdBootStrap
{
    public static CLProgressBar clpb = new CLProgressBar();

    public static void AddToAllActions(string v, Dictionary<string, VoidVoid> actions, Dictionary<string, object> allActions)
    {
        string key = null;

        foreach (var item in actions)
        {
            key = v + AllStringsSE.swd + item.Key;

            if (allActions.ContainsKey(key))
            {
                break;
            }

            if (item.Key != "None")
            {
                allActions.Add(key, item.Value);
            }
        }
    }

    /// <summary>
    /// Nevrací nikdy null. Buď result z CL.AskUser (pokud se má uživatele ptát) nebo SE.
    /// </summary>
    public static
#if ASYNC
    async Task<string>
#else
    string
#endif
 Run(AIInitArgs aiInitArgs, string appName, Func<IClipboardHelper> createInstanceClipboardHelper,
#if ASYNC
    Func<Task>
#else
Action
#endif
 runInDebug, Func<Dictionary<string, TaskVoid>> AddGroupOfActions, Dictionary<string, VoidVoid> pAllActions, bool? askUserIfRelease, Action InitSqlMeasureTime, Action customInit, Action assingSearchInAll,
        Action applyCryptData, Action assignJsSerialization, string[] args, Action psInit, Dictionary<string, object> groupsOfActionsFromProgramCommon, Action javascriptSerializationInitUtf8json, string eventLogNameFromEventLogNames, Func</*IDatabasesConnections*/ object> dbConns, Action<ICrypt> rijndaelBytesInit,
        ICrypt cryptDataWrapperRijn, /*(List<string> keysCommonSettings, List<string> keysSettingsList, List<string> keysSettingsBool, List<string> keysSettingsOther)*/ CreateAppFoldersIfDontExistsArgs createAppFoldersIfDontExistsArgs, Dictionary<string, TaskVoid> pAllActionsAsync, bool isNotUt, Func<Func<char, bool>> BitLockerHelperInit, bool isDebug, Func<Func<string, string, string>, Task> ProgramSharedCreatePathToFiles, Func<string, string, string> AppDataCiGetFileString, Func<IPercentCalculator> createPercentCalculator, Action<string> ThisApp_SetName, Action<CreateAppFoldersIfDontExistsArgs> AppData_CreateAppFoldersIfDontExists)
    {
        return
#if ASYNC
    await
#endif
 Run2(new RunArgs
 {

     aiInitArgs = aiInitArgs,
     appName = appName,
     createInstanceClipboardHelper = createInstanceClipboardHelper,
     runInDebug = runInDebug,
     AddGroupOfActions = AddGroupOfActions,
     pAllActions = pAllActions,
     askUserIfRelease = askUserIfRelease,
     InitSqlMeasureTime = InitSqlMeasureTime,
     customInit = customInit,
     assingSearchInAll = assingSearchInAll,
     applyCryptData = applyCryptData,
     assignJsSerialization = assignJsSerialization,
     args = args,
     psInit = psInit,
     groupsOfActionsFromProgramCommon = groupsOfActionsFromProgramCommon,
     javascriptSerializationInitUtf8json = javascriptSerializationInitUtf8json,
     eventLogNameFromEventLogNames = eventLogNameFromEventLogNames,
     dbConns = dbConns,
     rijndaelBytesInit = rijndaelBytesInit,
     cryptDataWrapperRijn = cryptDataWrapperRijn,
     createAppFoldersIfDontExistsArgs = createAppFoldersIfDontExistsArgs,
     pAllActionsAsync = pAllActionsAsync,
     isNotUt = isNotUt,
     BitLockerHelperInit = BitLockerHelperInit,
     IsDebug = isDebug,
     ProgramSharedCreatePathToFiles = ProgramSharedCreatePathToFiles,
     AppDataCiGetFileString = AppDataCiGetFileString,
     createPercentCalculator = createPercentCalculator,
     ThisApp_SetName = ThisApp_SetName,
     AppData_CreateAppFoldersIfDontExists = AppData_CreateAppFoldersIfDontExists
 });
    }

    //void mustBeInStacktrace()
    //{

    //}

    /// <summary>
    /// If user cannot select, A4,5 can be empty
    /// askUserIfRelease = null - ask user even in debug
    ///
    /// Nevrací nikdy null. Buď result z CL.AskUser (pokud se má uživatele ptát) nebo SE.
    /// pAllActions must be from ProgramShared
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="clipboardHelperWin"></param>
    /// <param name="runInDebug"></param>
    /// <param name="AddGroupOfActions"></param>
    /// <param name="pAllActions"></param>
    public static
#if ASYNC
    async Task<string>
#else
    string
#endif
 Run2(RunArgs a)
    {
        List<string> wasNull = new List<string>();

        AI.Init(a.aiInitArgs);

        var appName = a.appName;
        var createInstanceClipboardHelper = a.createInstanceClipboardHelper;
        var runInDebug = a.runInDebug;
        var AddGroupOfActions = a.AddGroupOfActions;
        var pAllActions = a.pAllActions;
        var askUserIfRelease = a.askUserIfRelease;
        var InitSqlMeasureTime = a.InitSqlMeasureTime;
        var customInit = a.customInit;
        var assingSearchInAll = a.assingSearchInAll;
        var applyCryptData = a.applyCryptData;
        var assignJsSerialization = a.assignJsSerialization;
        var args = a.args;
        var psInit = a.psInit;
        var groupsOfActionsFromProgramCommon = a.groupsOfActionsFromProgramCommon;
        var javascriptSerializationInitUtf8json = a.javascriptSerializationInitUtf8json;
        var eventLogNameFromEventLogNames = a.eventLogNameFromEventLogNames;
        var dbConns = a.dbConns;
        var rijndaelBytesInit = a.rijndaelBytesInit;
        var cryptDataWrapperRijn = a.cryptDataWrapperRijn;
        var createAppFoldersIfDontExistsArgs = a.createAppFoldersIfDontExistsArgs;
        var pAllActionsAsync = a.pAllActionsAsync;
        var isNotUt = a.isNotUt;
        var bitLockerHelperInit = a.BitLockerHelperInit;
        var isDebug = a.IsDebug;
        var ProgramSharedCreatePathToFiles = a.ProgramSharedCreatePathToFiles;
        var AppDataCiGetFileString = a.AppDataCiGetFileString;
        var createPercentCalculator = a.createPercentCalculator;
        var thisApp_SetName = a.ThisApp_SetName;
        var appData_CreateAppFoldersIfDontExists = a.AppData_CreateAppFoldersIfDontExists;

        if (bitLockerHelperInit != null)
        {
            ThrowEx.IsLockedByBitLocker = bitLockerHelperInit();
        }

        //ThisApp.EventLogName = eventLogNameFromEventLogNames;

        //CL.i18n = sess.i18n;

        if (rijndaelBytesInit != null && cryptDataWrapperRijn != null)
        {
            rijndaelBytesInit(cryptDataWrapperRijn);
        }

        if (dbConns != null)
        {
            dbConns();
        }

        if (javascriptSerializationInitUtf8json != null)
        {
            javascriptSerializationInitUtf8json.Invoke();
        }

        if (assingSearchInAll != null)
        {
            assingSearchInAll();
        }

        if (applyCryptData != null)
        {
            applyCryptData();
        }

        if (assignJsSerialization != null)
        {
            assignJsSerialization();
        }

        if (psInit != null)
        {
            psInit();
        }

        if (thisApp_SetName == null)
        {
            wasNull.Add(nameof(thisApp_SetName));
        }
        else
        {
            thisApp_SetName(appName);
        }

        if (appData_CreateAppFoldersIfDontExists == null)
        {
            wasNull.Add(nameof(appData_CreateAppFoldersIfDontExists));
        }
        else
        {
            appData_CreateAppFoldersIfDontExists(createAppFoldersIfDontExistsArgs);
        }


        if (InitSqlMeasureTime != null)
        {
            InitSqlMeasureTime();
        }

        CmdApp.Init();

        /*
Měl jsem chybu TypeLoadException: Could not load type 'cmd.Essential.ConsoleLogger' from assembly 
        'SunamoCl, Version=24.1.14.1, Culture=neutral, 
        PublicKeyToken=null'.
         */

        CmdApp.EnableConsoleLogging(true);

        if (createInstanceClipboardHelper != null)
        {
            var instance = createInstanceClipboardHelper();
            //InitApp.Clipboard = instance;
            CL.ClipboardHelper = instance;
        }

        // Logger se využíval na mnoha místech, musím nechat
        //InitApp.Logger = ConsoleLoggerCmd.Instance;
        //InitApp.TemplateLogger = ConsoleTemplateLogger.Instance;
        //InitApp.TypedLogger = TypedConsoleLogger.Instance;

        // tady musím předat DI metody pro inicializaci
        //BasePathsHelper.Init();

        //XlfResourcesHSunamo.SaveResouresToRLSunamo(LocalizationLanguagesLoader.Load());

        bool askUser = false;

        string arg = string.Empty;

        if (!askUserIfRelease.HasValue)
        {
            askUser = true;
        }

        if (customInit != null)
        {
            customInit();
        }

        #region Copied from Initialize.cs
        if (ProgramSharedCreatePathToFiles != null)
        {
            await ProgramSharedCreatePathToFiles(AppDataCiGetFileString);
        }
        else
        {
            wasNull.Add(nameof(ProgramSharedCreatePathToFiles));
        }


        #region #2 Specific initialization which is not in CmdBootStrap
        //NetHelperSunamo.NEVER_EAT_POISON_Disable_CertificateValidation();

        ////SunamoCzAdminHelper.InitializeStaticTables();

        //// must be false, otherwise will raise errors that is not allowed i18n in asp.net
        //Exc.aspnet = false;
        //ThrowExceptions.writeServerError = WriterEventLog.WriteException;

        //CryptHelper.ApplyCryptData(CryptHelper.RijndaelBytes.Instance, CryptDataWrapper.rijn);
        //_.DatabasesConnections.Reload();
        //_.DatabasesConnections.SetConnToMSDatabaseLayer(Databases.SunamoCzLocal, null);
        #endregion

        #region #3 Init SunamoCzAdmin
        var clpbIsNull = clpb == null;
        var createPercentCalculatorIsNull = createPercentCalculator == null;

        if (clpbIsNull || createPercentCalculatorIsNull)
        {
            //if (clpbIsNull)
            //{
            //    wasNull.Add(nameof(clpb));
            //}
            //if (createPercentCalculatorIsNull)
            //{
            //    wasNull.Add(nameof(createPercentCalculator));
            //}
        }
        else
        {
            clpb.isNotUt = isNotUt;
            clpb.Init(createPercentCalculator());
        }



        //PowershellRunner.ci.clpb = clpb;
        // To tu je zakomentované jen aby se překopírovalo tam kde to potřebuji. 
        // Jinými slovy, je to seznam kde všude je clpb

        // Pokud nějaká třída přestane existovat, číslo už ji zůstane
        //XmlDocumentsCache.clpb = clpb; // 1
        //SunamoCzAdminHelper.clpb = clpb; // 2
        //KaraokeTextyHelper.clpb = clpb; // 3
        //LyricsHelper.clpb = clpb; // 4
        //HttpRequestHelper.clpb = clpb; // 5
        //MigrateDataHelper.clpb = clpb; // 6
        //Program.clpb = clpb; // 7
        //Impl.clpb = clpb; // 8
        #endregion
        #endregion

        if (isDebug)
        {
            if (runInDebug == null)
            {
                wasNull.Add(nameof(runInDebug));
            }
            else
            {
#if ASYNC
                await
#endif
                runInDebug();
            }
        }
        else
        {
            askUser = askUserIfRelease.Value;
        }

        if (AddGroupOfActions != null && pAllActions != null)
        {
            if (args.Length != 0)
            {
                CL.WriteLine($"Was entered some args, askUser was setted from {askUser} to false");
                askUser = false;
            }

            arg =
     // 
#if ASYNC
     await
#endif
 CL.AskUser(askUser, AddGroupOfActions, pAllActions, pAllActionsAsync, groupsOfActionsFromProgramCommon);

            if (askUser)
            {
                CL.WriteLine("App finished its running");
                // Když se mi toto pouštělo ve Win a ne ve VS tak se okno automaticky nezavírá a zbytečně to zdržovalo
                //CL.ReadLine();
            }
        }

        if (wasNull.Count != 0)
        {
            throw new Exception("Was null: " + string.Join(",", wasNull));
        }

        return arg;
    }
}
