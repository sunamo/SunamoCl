namespace SunamoCl;

public partial class CL
{
    public static void WriteColor(TypeOfMessageCl messageType, string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(messageType, text, args);
    }

    // For TextWriter use ErrorWriter
    public static void Error(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Error, text, args);
    }

    // In every task - Start
    public static void Warning(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Warning, text, args);
    }

    public static void Information(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Information, text, args);
    }

    // In every task - end
    public static void Success(string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Success, text, args);
    }

    public static void Appeal(string text)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Appeal, text);
    }

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
