namespace SunamoCl;

public partial class CL
{
    /// <summary>
    /// Write color.
    /// </summary>
    /// <param name="messageType">The message type.</param>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    public static void WriteColor(TypeOfMessageCl messageType, string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(messageType, text, args);
    }

    // For TextWriter use ErrorWriter
    /// <summary>
    /// Error.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    public static void Error(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Error, text, args);
    }

    // In every task - Start
    /// <summary>
    /// Warning.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    public static void Warning(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Warning, text, args);
    }

    /// <summary>
    /// Information.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    public static void Information(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Information, text, args);
    }

    // In every task - end
    /// <summary>
    /// Success.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    public static void Success(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Success, text, args);
    }

    /// <summary>
    /// Appeal.
    /// </summary>
    /// <param name="text">The text.</param>
    public static void Appeal(string text)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Appeal, text);
    }

    /// <summary>
    /// Change color of console and write.
    /// </summary>
    /// <param name="messageType">The message type.</param>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    public static void ChangeColorOfConsoleAndWrite(TypeOfMessageCl messageType, string text, params object[] args)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }

        SetColorOfConsole(messageType);

        Console.WriteLine(text, args);
        SetColorOfConsole(TypeOfMessageCl.Ordinal);
    }

    /// <summary>
    /// Set color of console.
    /// </summary>
    /// <param name="messageType">The message type.</param>
    public static void SetColorOfConsole(TypeOfMessageCl messageType)
    {
        if (!ShouldWriteToConsole)
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
