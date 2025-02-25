namespace SunamoCl.SunamoCmd.Essential;
public class CmdApp
{
    /// <summary>
    ///     Dont ask in console, load from Clipboard
    /// </summary>
    //public static bool loadFromClipboard = false;
    public static bool waitOnEnd = false;

    public static bool openAndWaitForChangeContentOfInputFile = true;

    public static bool LoadFromClipboard { get; internal set; }


    /// <summary>
    ///     Create in class where are you calling method without A2 openVsCode
    /// </summary>
    /// <param name="myPositionsHtmlFile"></param>
    /// <param name="openVsCode"></param>
    public static
#if ASYNC
        async Task<string>
#else
    string
#endif
        WaitForSaving(ILogger logger, string myPositionsHtmlFile, Action<ILogger, string, bool, int?> openVsCode)
    {
        Console.WriteLine($"Running WaitForSaving for {myPositionsHtmlFile}, openAndWaitForChangeContentOfInputFile: {openAndWaitForChangeContentOfInputFile}");

        if (openAndWaitForChangeContentOfInputFile)
        {
            openVsCode(logger, myPositionsHtmlFile, false, null);
            CL.WriteLine(
                $"Waiting for insert html to {Path.GetFileName(myPositionsHtmlFile)}, press enter to continue");
            CL.ReadLine();
        }

        Console.WriteLine($"Reading {myPositionsHtmlFile}");

        if (!File.Exists(myPositionsHtmlFile))
        {
            await File.WriteAllTextAsync(myPositionsHtmlFile, string.Empty);
            return string.Empty;
        }

        return
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(myPositionsHtmlFile);
    }

    public static void WaitOnEnd()
    {
#if DEBUG
        if (waitOnEnd) CL.ReadLine();
#endif
    }

    public static void Init()
    {
        // Nev�m zda je dobr� n�pad. Kdy� vznikne nechycen� exception, dostane se do UnhandledExceptionTrapper() ale u� to nem�m v debuggeru VS. Mo�n� to je jen �patn�m nastaven�m IDE.

        //AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
    }

    /// <summary>
    /// Nev�m zda je dobr� n�pad. Kdy� vznikne nechycen� exception, dostane se do UnhandledExceptionTrapper() ale u� to nem�m v debuggeru VS. Mo�n� to je jen �patn�m nastaven�m IDE.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    internal static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
    {
        throw (Exception)e.ExceptionObject;

        //string dump = null;
        ////dump = YamlHelper.DumpAsYaml(e);
        ////dump = SunamoJsonHelper.SerializeObject(e, true);
        //dump = RH.DumpAsString(new DumpAsStringArgs { o = e, d = DumpProvider.Reflection });

        //ThisApp.Error(e.ExceptionObject.ToString());
        ////WriterEventLog.WriteToMainAppLog(dump, System.Diagnostics.EventLogEntryType.Error, Exceptions.CallingMethod());
    }

    public static void EnableConsoleLogging(bool v)
    {
        //if (v)
        //{
        //    // because method was called two times 
        //    ThisApp.StatusSetted -= ThisApp_StatusSetted;
        //    ThisApp.StatusSetted += ThisApp_StatusSetted;
        //}
        //else
        //{
        //    ThisApp.StatusSetted -= ThisApp_StatusSetted;
        //}
    }

    private static void ThisApp_StatusSetted(TypeOfMessageCl t, string message)
    {
        //TypedConsoleLogger.Instance.WriteLine(t, message);
    }

    /// <summary>
    ///     Alternatives are:
    ///     InitApp.SetDebugLogger
    ///     CmdApp.SetLogger
    ///     WpfApp.SetLogger
    /// </summary>
    public static void SetLogger()
    {
        //InitApp.Logger = ConsoleLoggerCmd.Instance;
        //InitApp.TemplateLogger = ConsoleTemplateLogger.Instance;
        //InitApp.TypedLogger = TypedConsoleLogger.Instance;
    }
}