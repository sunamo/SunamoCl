namespace SunamoCl;

partial class CL
{
    /// <summary>
    /// Gets or sets whether to write output to console. When false, all write operations are suppressed
    /// </summary>
    public static bool ShouldWriteToConsole { get; set; } = true;

    /// <summary>
    /// Writes a line to console in a specific color, then resets to default color
    /// </summary>
    /// <param name="color">Color to use for the text</param>
    /// <param name="value">Text to write</param>
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
    /// Writes the current time left value at a fixed position on the console
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
    /// Writes a list of items to console with optional header and formatting
    /// </summary>
    /// <param name="listItems">Items to write</param>
    /// <param name="header">Optional header to display before the list</param>
    /// <param name="arguments">Optional formatting arguments for the list</param>
    public static void WriteList(IEnumerable<string> listItems, string? header = null, WriteListArgs? arguments = null)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }

        if (header != null) Appeal(header);

        if (arguments == null)
        {
            arguments = new WriteListArgs();
        }
        var itemIndex = 0;
        foreach (var item in listItems)
        {
            itemIndex++;
            Console.WriteLine((arguments.ShouldWriteNumber ? itemIndex + ". " : "") + (arguments.WrapInto != null ? SH.WrapWith(item, arguments.WrapInto) : item));
        }
    }

    /// <summary>
    /// Writes a blank line followed by a formatted line to console
    /// </summary>
    /// <param name="text">Format string</param>
    /// <param name="parameters">Format parameters</param>
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
    /// Writes a line of text to console
    /// </summary>
    /// <param name="text">Text to write</param>
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
    /// Writes a number to console as a line
    /// </summary>
    /// <param name="number">Number to write</param>
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
    /// Writes a string to console without a newline
    /// </summary>
    /// <param name="value">Text to write</param>
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
    /// Writes a single character to console without a newline
    /// </summary>
    /// <param name="character">Character to write</param>
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
    /// Writes a blank line to console
    /// </summary>
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
    /// Writes an object as string to console. Uses ToString() which may have lower performance.
    /// </summary>
    /// <param name="value">Object to write.</param>
    public static void WriteLineO(object value)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.WriteLine(value.ToString());
    }

    /// <summary>
    /// Writes formatted output to console using format string and two parameters
    /// </summary>
    /// <param name="format">Format string</param>
    /// <param name="left">First format parameter</param>
    /// <param name="right">Second format parameter</param>
    public static void Write(string format, string left, object right)
    {
        if (!ShouldWriteToConsole)
        {
            return;
        }
        CheckWritingDuringClipboard();
        Console.Write(format, left, right);
    }

    /// <summary>
    /// Logs a formatted message to console
    /// </summary>
    /// <param name="message">Message format string</param>
    /// <param name="objects">Format parameters</param>
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
    /// Writes a formatted line to console
    /// </summary>
    /// <param name="message">Message format string</param>
    /// <param name="objects">Format parameters</param>
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
    /// Writes an exception message to console.
    /// </summary>
    /// <param name="exception">Exception to write.</param>
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