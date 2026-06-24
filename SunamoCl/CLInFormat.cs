namespace SunamoCl;

partial class CL
{
    public static string LoadFromClipboardOrConsoleInFormat(string promptText, TextFormatDataCl textFormat)
    {
        string? userInput = null;
        if (!CmdApp.ShouldLoadFromClipboard)
        {
            userInput = UserMustTypeInFormat(promptText, textFormat);
        }
        else
        {
            userInput = ClipboardService.GetText();
        }
        return userInput!;
    }

    public static string UserMustTypeInFormat(string promptText, TextFormatDataCl textFormat)
    {
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
        }
    }
}
