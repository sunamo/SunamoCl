namespace SunamoCl;

public partial class CL
{
    /// <summary>
    /// Writes colored text to console based on message type
    /// </summary>
    /// <param name="messageType">Type of message determining the color</param>
    /// <param name="text">Text to write, can contain format placeholders</param>
    /// <param name="args">Format arguments for the text</param>
    public static void WriteColor(TypeOfMessageCl messageType, string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(messageType, text, args);
    }

    /// <summary>
    ///     For TextWriter use Error2
    /// </summary>
    /// <param name="text"></param>
    /// <param name="p"></param>
    public static void Error(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Error, text, args);
    }

    /// <summary>
    ///     In every task - Start
    /// </summary>
    /// <param name="text"></param>
    /// <param name="p"></param>
    public static void Warning(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Warning, text, args);
    }

    /// <summary>
    /// Writes an informational message to console in white color
    /// </summary>
    /// <param name="text">Text to write, can contain format placeholders</param>
    /// <param name="args">Format arguments for the text</param>
    public static void Information(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Information, text, args);
    }

    /// <summary>
    ///     In every task - end
    /// </summary>
    /// <param name="text"></param>
    /// <param name="p"></param>
    public static void Success(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Success, text, args);
    }

    /// <summary>
    ///     RunInCycle both
    /// </summary>
    /// <param name="text"></param>
    public static void Appeal(string text)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Appeal, text);
    }

    /// <summary>
    /// Changes console color based on message type, writes text, then resets color to white
    /// </summary>
    /// <param name="messageType">Type of message determining the color</param>
    /// <param name="text">Text to write, can contain format placeholders</param>
    /// <param name="args">Format arguments for the text</param>
    public static void ChangeColorOfConsoleAndWrite(TypeOfMessageCl messageType, string text, params object[] args)
    {
        if (!WriteToConsole)
        {
            return;
        }

        SetColorOfConsole(messageType);

        Console.WriteLine(text, args);
        SetColorOfConsole(TypeOfMessageCl.Ordinal);
    }

    /// <summary>
    /// Sets console foreground color based on message type
    /// </summary>
    /// <param name="messageType">Type of message determining the color</param>
    public static void SetColorOfConsole(TypeOfMessageCl messageType)
    {
        if (!WriteToConsole)
        {
            return;
        }

        var foregroundColor = ConsoleColor.White;

        switch (messageType)
        {
            case TypeOfMessageCl.Error:
                foregroundColor = ConsoleColor.Red;
                break;
            case TypeOfMessageCl.Warning:
                foregroundColor = ConsoleColor.Yellow;
                break;
            case TypeOfMessageCl.Information:

            case TypeOfMessageCl.Ordinal:
                foregroundColor = ConsoleColor.White;
                break;
            case TypeOfMessageCl.Appeal:
                foregroundColor = ConsoleColor.Magenta;
                break;
            case TypeOfMessageCl.Success:
                foregroundColor = ConsoleColor.Green;
                break;
        }

        if (foregroundColor != ConsoleColor.Black)
            Console.ForegroundColor = foregroundColor;
        else
            Console.ResetColor();
    }
}