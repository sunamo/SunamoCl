namespace SunamoCl;

partial class CL
{
    /// <summary>
    /// Load from clipboard or console in format.
    /// </summary>
    /// <param name="promptText">The prompt text.</param>
    /// <param name="textFormat">The text format.</param>
    /// <returns>The load from clipboard or console in format result.</returns>
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

    /// <summary>
    /// User must type in format.
    /// </summary>
    /// <param name="promptText">The prompt text.</param>
    /// <param name="textFormat">The text format.</param>
    /// <returns>The user must type in format result.</returns>
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
