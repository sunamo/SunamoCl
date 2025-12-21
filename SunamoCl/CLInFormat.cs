namespace SunamoCl;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
partial class CL
{
    /// <summary>
    ///     First I must ask which is always from console - must prepare user to load data to clipboard.
    /// </summary>
    /// <param name="promptText"></param>
    /// <param name="textFormat"></param>
    public static string LoadFromClipboardOrConsoleInFormat(string promptText, TextFormatDataCl textFormat)
    {
        string? userInput = null;
        if (!CmdApp.LoadFromClipboard)
        {
            userInput = UserMustTypeInFormat(promptText, textFormat);
        }
        else
        {
            userInput = ClipboardService.GetText();
        }
        return userInput;
    }


    // toto bude lepší řešit v každé app zvlášť. Je to proto že bych musel do každé metody vkládat TextFormatDataString který nemám
    public static string UserMustTypeInFormat(string promptText, TextFormatDataCl textFormat)
    {
        //return UserMustType(promptText);
        #region Must be repaired first. DateToShort in ConsoleApp1 failed while parsing.
        string entered = "";
        while (true)
        {
            entered = UserMustType(promptText);
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