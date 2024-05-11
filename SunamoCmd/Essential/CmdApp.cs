namespace SunamoCl;

public partial class CmdApp
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
}
