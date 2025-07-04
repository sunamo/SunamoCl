namespace SunamoCl;
partial class CL
{
    /// <summary>
    ///     First I must ask which is always from console - must prepare user to load data to clipboard.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="textFormat"></param>
    public static string LoadFromClipboardOrConsoleInFormat(string format, TextFormatDataCl textFormat)
    {
        string? s = null;
        if (!CmdApp.LoadFromClipboard)
        {
            s = UserMustTypeInFormat(format, textFormat);
        }
        else
        {
            s = ClipboardService.GetText();
        }
        return s;
    }


    // toto bude lepší řešit v každé app zvlášť. Je to proto že bych musel do každé metody vkládat TextFormatDataString který nemám 
    public static string UserMustTypeInFormat(string what, TextFormatDataCl textFormat)
    {
        //return UserMustType(what);
        #region Must be repaired first. DateToShort in ConsoleApp1 failed while parsing.
        string entered = "";
        while (true)
        {
            entered = UserMustType(what);
            if (entered == null)
            {
                return null;
            }
            if (SH.HasTextRightFormat(entered, textFormat))
            {
                return entered;
            }
            else
            {
                ConsoleTemplateLogger.Instance.UnfortunatelyBadFormatPleaseTryAgain();
            }
            //}
            //return null; 
            #endregion
        }
        //public static string SelectFromBrowsers(Action phWinAddBrowser, List<string> browsers)
        //{
        //    return SelectFromVariants(browsers, "browser");
        //    return "";
        //}
    }
}
