namespace SunamoCl;

partial class CL
{
    public static bool ShouldWriteToConsole { get; set; } = true;

    public static void WriteLineWithColor(ConsoleColor color, string value)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        ForegroundColor = color;
        WriteLine(value);
        ResetColor();
    }

    public static void WriteTimeLeft()
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        var currentLineCursorTop = Console.CursorTop;
        var currentLineCursorLeft = Console.CursorLeft;
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 1);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, 1);
        Console.Write(timeLeft);
        Console.SetCursorPosition(currentLineCursorLeft, currentLineCursorTop);
        Console.CursorVisible = true;
        timeLeft -= 1;
    }

    public static void WriteList(IEnumerable<string> listItems, string? header = null, WriteListArgs? arguments = null)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }

        if (header != null) Appeal(header);

        arguments ??= new WriteListArgs();
        var itemIndex = 0;
        foreach (var item in listItems)
        {
            itemIndex++;
            Console.WriteLine((arguments.ShouldWriteNumber ? itemIndex + ". " : "") + (arguments.WrapInto != null ? SH.WrapWith(item, arguments.WrapInto) : item));
        }
    }

    public static void WriteLineFormat(string text, params object[] parameters)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        Console.WriteLine();
        Console.WriteLine(text, parameters);
    }

    public static void WriteLine(string text)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.WriteLine(text);
    }

    public static void WriteLine(int number)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.WriteLine(number.ToString());
    }

    public static void Write(string value)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.Write(value);
    }

    public static void Write(char character)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.Write(character);
    }

    public static void WriteLine()
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.WriteLine();
    }
    public static void WriteLineObject(object value)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.WriteLine(value.ToString());
    }

    public static void Write(string format, string firstArgument, object secondArgument)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.Write(format, firstArgument, secondArgument);
    }

    public static void Log(string message, params object[] objects)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.WriteLine(message, objects);
    }

    public static void WriteLine(string message, params object[] objects)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.WriteLine(message, objects);
    }
    public static void WriteLine(Exception exception)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.WriteLine(exception.Message);
    }
}
