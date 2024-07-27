namespace SunamoCl.SunamoCmd.Essential;

public class CmdApp
{
    //public static TypedLoggerBase ConsoleOrDebugTyped()
    //{
    //    //        // toto mi fungovalo při projectreference. ale při packagereference v release není žádný TypedDebugLogger
    //    //#if DEBUG
    //    //        return TypedDebugLogger.Instance;
    //    //#elif !DEBUG
    //    //        return TypedConsoleLogger.Instance;
    //    //#endif

    //    return TypedConsoleLogger.Instance;
    //}

    /// <summary>
    /// Create in class where are you calling method without A2 openVsCode
    /// </summary>
    /// <param name="myPositionsHtmlFile"></param>
    /// <param name="openVsCode"></param>
    public static
#if ASYNC
        async Task<string>
#else
    string  
#endif
        WaitForSaving(string myPositionsHtmlFile, Func<string, Task> openVsCode)
    {
        if (openAndWaitForChangeContentOfInputFile)
        {
            await openVsCode(myPositionsHtmlFile);
            CL.WriteLine($"Waiting for insert html to {Path.GetFileName(myPositionsHtmlFile)}, press enter to continue");
            CL.ReadLine();
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
        if (waitOnEnd)
        {
            CL.ReadLine();
        }
#endif
    }

    public static void Init()
    {
        AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
    }

    static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
    {
        throw (Exception)e.ExceptionObject;

        //string dump = null;
        ////dump = YamlHelper.DumpAsYaml(e);
        ////dump = SunamoJsonHelper.SerializeObject(e, true);
        //dump = RH.DumpAsString(new DumpAsStringArgs { o = e, d = DumpProvider.Reflection });

        //ThisApp.Error(e.ExceptionObject.ToString());
        ////WriterEventLog.WriteToMainAppLog(dump, System.Diagnostics.EventLogEntryType.Error, Exc.CallingMethod());
    }

    /// <summary>
    /// Dont ask in console, load from Clipboard
    /// </summary>
    //public static bool loadFromClipboard = false;
    public static bool waitOnEnd = false;
    public static bool openAndWaitForChangeContentOfInputFile = true;

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
    /// Alternatives are:
    /// InitApp.SetDebugLogger
    /// CmdApp.SetLogger
    /// WpfApp.SetLogger
    /// </summary>
    public static void SetLogger()
    {
        //InitApp.Logger = ConsoleLoggerCmd.Instance;
        //InitApp.TemplateLogger = ConsoleTemplateLogger.Instance;
        //InitApp.TypedLogger = TypedConsoleLogger.Instance;
    }
}
