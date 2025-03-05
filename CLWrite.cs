namespace SunamoCl;

partial class CL
{
    public static bool WriteToConsole { get; set; } = true;
    public static void WriteLineWithColor(ConsoleColor c, string v)
    {
        if (!WriteToConsole)
        {
            return;
        }
        ForegroundColor = c;
        WriteLine(v);
        ResetColor();
    }
    public static void WriteTimeLeft()
    {
        if (!WriteToConsole)
        {
            return;
        }
        var currentLineCursorTop = Console.CursorTop;
        var currentLineCursorLeft = Console.CursorLeft;
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 1);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, 1);
        Console.Write(time_left);
        Console.SetCursorPosition(currentLineCursorLeft, currentLineCursorTop);
        Console.CursorVisible = true;
        time_left -= 1;
    }
    public static void WriteList(IEnumerable<string> l, string header, WriteListArgs a = null)
    {
        if (!WriteToConsole)
        {
            return;
        }
        Appeal(header);
        WriteList(l);
    }
    public static void WriteLineFormat(string text, params object[] p)
    {
        if (!WriteToConsole)
        {
            return;
        }
        Console.WriteLine();
        Console.WriteLine(text, p);
    }
    public static void WriteList(IEnumerable<string> l, WriteListArgs a = null)
    {
        if (!WriteToConsole)
        {
            return;
        }
        if (a == null)
        {
            a = new WriteListArgs();
        }
        var i = 0;
        foreach (var item in l)
        {
            i++;
            Console.WriteLine((a.WriteNumber ? i + ". " : "") + (a.WrapInto != null ? SH.WrapWith(item, a.WrapInto) : item));
        }
    }
    public static void WriteLine(string a)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.WriteLine(a);
    }
    public static void WriteLine(int a)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.WriteLine(a.ToString());
    }
    public static void Write(string v)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.Write(v);
    }
    public static void Write(char v)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.Write(v);
    }
    public static void WriteLine()
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.WriteLine();
    }
    /// <summary>
    ///     Must be O to express I'm counting with lover performance.
    /// </summary>
    /// <param name="correlationId"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void WriteLineO(object correlationId)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.WriteLine(correlationId.ToString());
    }
    public static void Write(string format, string left, object right)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.Write(format, left, right);
    }
    public static void Log(string a, params object[] o)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.WriteLine(a, o);
    }
    public static void WriteLine(string a, params object[] o)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.WriteLine(a, o);
    }
    /// <summary>
    ///     Good to be in CLConsole even if dont just call Console
    /// </summary>
    /// <param name="ex"></param>
    public static void WriteLine(Exception ex)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        //Console.WriteLine(Exceptions.TextOfExceptions(ex));
        Console.WriteLine(ex.Message);
    }
}