namespace SunamoCl;

public partial class CL
{
    public static void WriteColor(TypeOfMessageCl messageType, string text, params string[] args)
    {
        ChangeColorOfConsoleAndWrite(messageType, text, args);
    }

    ///// <summary>
    ///// Mohl bych užívat TypedConsoleLogger ale ten je ve sunamo a aby to mohlo být ve cl namísto cmd a mělo více projektů přístup k tomu, musím to dělat takto 
    ///// </summary>
    ///// <param name="v"></param>
    //public static void Information(string v)
    //{
    //    WriteColor(TypeOfMessage.Information, v);
    //}

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

    public static void SetColorOfConsole(TypeOfMessageCl messageType)
    {
        if (!WriteToConsole)
        {
            return;
        }

        var bk = ConsoleColor.White;

        switch (messageType)
        {
            case TypeOfMessageCl.Error:
                bk = ConsoleColor.Red;
                break;
            case TypeOfMessageCl.Warning:
                bk = ConsoleColor.Yellow;
                break;
            case TypeOfMessageCl.Information:

            case TypeOfMessageCl.Ordinal:
                bk = ConsoleColor.White;
                break;
            case TypeOfMessageCl.Appeal:
                bk = ConsoleColor.Magenta;
                break;
            case TypeOfMessageCl.Success:
                bk = ConsoleColor.Green;
                break;
        }

        if (bk != ConsoleColor.Black)
            Console.ForegroundColor = bk;
        else
            Console.ResetColor();
    }
}