namespace SunamoCl;

partial class CL
{
    public static bool WriteToConsole { get; set; } = true;
    public static void WriteLineWithColor(ConsoleColor color, string value)
    {
        if (!WriteToConsole)
        {
            return;
        }
        ForegroundColor = color;
        WriteLine(value);
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
        Console.Write(_timeLeft);
        Console.SetCursorPosition(currentLineCursorLeft, currentLineCursorTop);
        Console.CursorVisible = true;
        _timeLeft -= 1;
    }
    public static void WriteList(IEnumerable<string> listItems, string? header = null, WriteListArgs? arguments = null)
    {
        if (!WriteToConsole)
        {
            return;
        }

        Appeal(header);

        if (arguments == null)
        {
            arguments = new WriteListArgs();
        }
        var itemIndex = 0;
        foreach (var item in listItems)
        {
            itemIndex++;
            Console.WriteLine((arguments.WriteNumber ? itemIndex + ". " : "") + (arguments.WrapInto != null ? SH.WrapWith(item, arguments.WrapInto) : item));
        }
    }
    public static void WriteLineFormat(string text, params object[] parameters)
    {
        if (!WriteToConsole)
        {
            return;
        }
        Console.WriteLine();
        Console.WriteLine(text, parameters);
    }
    public static void WriteLine(string text)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.WriteLine(text);
    }
    public static void WriteLine(int number)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.WriteLine(number.ToString());
    }
    public static void Write(string value)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.Write(value);
    }
    public static void Write(char character)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.Write(character);
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
    /// <param name="input"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void WriteLineO(object input)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.WriteLine(input.ToString());
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
    public static void Log(string message, params object[] objects)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.WriteLine(message, objects);
    }
    public static void WriteLine(string message, params object[] objects)
    {
        if (!WriteToConsole)
        {
            return;
        }
        IsWritingDuringClbp();
        Console.WriteLine(message, objects);
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