namespace SunamoCl;

partial class CL
{
    /// <summary>
    /// Loads data from clipboard or prompts user for input, validating against the specified format.
    /// </summary>
    /// <param name="promptText">Prompt text to display.</param>
    /// <param name="textFormat">Required text format definition.</param>
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
        return userInput!;
    }


    /// <summary>
    /// Prompts user to type text and validates it against required format, repeating until valid input is provided
    /// </summary>
    /// <param name="promptText">Text to display as prompt</param>
    /// <param name="textFormat">Required format for the text</param>
    /// <returns>User input that matches the required format</returns>
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
                return null!;
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