public partial class CL
{
    public static void WriteColor(TypeOfMessage t, string s, params string[] p)
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
        ChangeColorOfConsoleAndWrite(TypeOfMessage.Error, text, p);
    }

    /// <summary>
    ///     In every task - Start
    /// </summary>
    /// <param name="text"></param>
    /// <param name="p"></param>
    public static void Warning(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessage.Warning, text, p);
    }

    public static void Information(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessage.Information, text, p);
    }

    /// <summary>
    ///     In every task - end
    /// </summary>
    /// <param name="text"></param>
    /// <param name="p"></param>
    public static void Success(string text, params string[] p)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessage.Success, text, p);
    }

    /// <summary>
    ///     RunInCycle both
    /// </summary>
    /// <param name="appeal"></param>
    public static void Appeal(string appeal)
    {
        ChangeColorOfConsoleAndWrite(TypeOfMessage.Appeal, appeal);
    }

    public static void ChangeColorOfConsoleAndWrite(TypeOfMessage tz, string text, params object[] args)
    {
        SetColorOfConsole(tz);

        Console.WriteLine(text, args);
        SetColorOfConsole(TypeOfMessage.Ordinal);
    }

    public static void SetColorOfConsole(TypeOfMessage tz)
    {
        var bk = ConsoleColor.White;

        switch (tz)
        {
            case TypeOfMessage.Error:
                bk = ConsoleColor.Red;
                break;
            case TypeOfMessage.Warning:
                bk = ConsoleColor.Yellow;
                break;
            case TypeOfMessage.Information:

            case TypeOfMessage.Ordinal:
                bk = ConsoleColor.White;
                break;
            case TypeOfMessage.Appeal:
                bk = ConsoleColor.Magenta;
                break;
            case TypeOfMessage.Success:
                bk = ConsoleColor.Green;
                break;
        }

        if (bk != ConsoleColor.Black)
            Console.ForegroundColor = bk;
        else
            Console.ResetColor();
    }
}
