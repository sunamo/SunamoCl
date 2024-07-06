namespace SunamoCl;
public partial class CL
{
    public static void WriteColor(TypeOfMessageCl t, string s, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(t, s, p);
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
    public static void Error(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Error, text, p);
    }

    /// <summary>
    ///     In every task - Start
    /// </summary>
    /// <param name="text"></param>
    /// <param name="p"></param>
    public static void Warning(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Warning, text, p);
    }

    public static void Information(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Information, text, p);
    }

    /// <summary>
    ///     In every task - end
    /// </summary>
    /// <param name="text"></param>
    /// <param name="p"></param>
    public static void Success(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Success, text, p);
    }

    /// <summary>
    ///     RunInCycle both
    /// </summary>
    /// <param name="appeal"></param>
    public static void Appeal(string appeal)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessageCl.Appeal, appeal);
    }

    public static void ChangeColorOfConsoleAndWrite(TypeOfMessageCl tz, string text, params object[] args)
    {
        SetColorOfConsole(tz);

        Console.WriteLine(text, args);
        SetColorOfConsole(TypeOfMessageCl.Ordinal);
    }

    public static void SetColorOfConsole(TypeOfMessageCl tz)
    {
        var bk = ConsoleColor.White;

        switch (tz)
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
