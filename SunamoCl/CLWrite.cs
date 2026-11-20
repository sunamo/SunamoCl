namespace SunamoCl;

partial class CL
{
    /// <summary>
    /// Gets or sets the should write to console.
    /// </summary>
    public static bool ShouldWriteToConsole { get; set; } = true;

    /// <summary>
    /// Write line with color.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <param name="value">The value.</param>
    /// <returns>The write line with color result.</returns>
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

    /// <summary>
    /// Initializes a new instance of the write time left class.
    /// </summary>
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

    /// <summary>
    /// Write list.
    /// </summary>
    /// <param name="listItems">The list items.</param>
    /// <param name="header">The header.</param>
    /// <param name="arguments">The arguments.</param>
    /// <returns>The write list result.</returns>
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

    /// <summary>
    /// Write line format.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>The write line format result.</returns>
    public static void WriteLineFormat(string text, params object[] parameters)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        Console.WriteLine();
        Console.WriteLine(text, parameters);
    }

    /// <summary>
    /// Initializes a new instance of the write line class.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="parameters">The parameters.</param>
    public static void WriteLine(string text)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.WriteLine(text);
    }

    /// <summary>
    /// Initializes a new instance of the write line class.
    /// </summary>
    /// <param name="text">The text.</param>
    public static void WriteLine(int number)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.WriteLine(number.ToString());
    }

    /// <summary>
    /// Initializes a new instance of the write class.
    /// </summary>
    /// <param name="number.ToString()">The number to string.</param>
    public static void Write(string value)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.Write(value);
    }

    /// <summary>
    /// Initializes a new instance of the write class.
    /// </summary>
    /// <param name="value">The value.</param>
    public static void Write(char character)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.Write(character);
    }

    /// <summary>
    /// Initializes a new instance of the write line class.
    /// </summary>
    /// <param name="character">The character.</param>
    public static void WriteLine()
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.WriteLine();
    }
    /// <summary>
    /// Initializes a new instance of the write line object class.
    /// </summary>
    public static void WriteLineObject(object value)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.WriteLine(value.ToString());
    }

    /// <summary>
    /// Initializes a new instance of the write class.
    /// </summary>
    /// <param name="value.ToString()">The value to string.</param>
    public static void Write(string format, string firstArgument, object secondArgument)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.Write(format, firstArgument, secondArgument);
    }

    /// <summary>
    /// Initializes a new instance of the log class.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="firstArgument">The first argument.</param>
    /// <param name="secondArgument">The second argument.</param>
    public static void Log(string message, params object[] objects)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.WriteLine(message, objects);
    }

    /// <summary>
    /// Initializes a new instance of the write line class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="objects">The objects.</param>
    public static void WriteLine(string message, params object[] objects)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.WriteLine(message, objects);
    }
    /// <summary>
    /// Initializes a new instance of the write line class.
    /// </summary>
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
