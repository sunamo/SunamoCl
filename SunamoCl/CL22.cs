namespace SunamoCl;

public partial class CL
{
    /// <summary>
    /// User must type number.
    /// </summary>
    /// <param name="prompt">The prompt.</param>
    /// <param name="max">The max.</param>
    /// <returns>The user must type number result.</returns>
    public static int UserMustTypeNumber(string prompt, int max)
    {
        if (max > 999)
            ThrowEx.Custom("Max can be max 999 (creating serie of number could be too time expensive)");
        var entered = UserMustType(prompt, false, false, Enumerable.Range(0, max + 1).Select(number => number.ToString()).ToArray());
        if (entered == null)
            return int.MinValue;
        if (int.TryParse(entered, out var parsed))
            if (parsed <= max)
                return parsed;
        return UserMustTypeNumber(prompt, max);
    }

    /// <summary>
    /// User must type multi line.
    /// </summary>
    /// <param name="prompt">The prompt.</param>
    /// <param name="breakEnteringAfterEntered">The break entering after entered.</param>
    /// <returns>The user must type multi line result.</returns>
    public static string UserMustTypeMultiLine(string prompt, params string[] breakEnteringAfterEntered)
    {
        string? line = null;
        Information(AskForEnter(prompt, true, ""));
        StringBuilder stringBuilder = new();
        while ((line = Console.ReadLine()) != null)
        {
            stringBuilder.AppendLine(line);
            if (breakEnteringAfterEntered.Contains(line))
                break;
        }

        var trimmedText = stringBuilder.ToString().Trim();
        return trimmedText;
    }

    /// <summary>
    /// Ask for enter write.
    /// </summary>
    /// <param name="prompt">The prompt.</param>
    /// <param name="shouldAppendAfterEnter">The should append after enter.</param>
    public static void AskForEnterWrite(string prompt, bool shouldAppendAfterEnter)
    {
        WriteLine(AskForEnter(prompt, shouldAppendAfterEnter, null));
    }

    /// <summary>
    /// Ask for enter.
    /// </summary>
    /// <param name="prompt">The prompt.</param>
    /// <param name="shouldAppendAfterEnter">The should append after enter.</param>
    /// <param name="returnWhenIsNotNull">The return when is not null.</param>
    /// <returns>The ask for enter result.</returns>
    public static string AskForEnter(string prompt, bool shouldAppendAfterEnter, string? returnWhenIsNotNull)
    {
        if (returnWhenIsNotNull == null)
        {
            var promptBuilder = new StringBuilder();
            prompt = prompt.TrimEnd('.').TrimEnd(':');
            if (shouldAppendAfterEnter)
            {
                promptBuilder.Append($"📝 Enter {prompt}");
            }
            else
            {
                promptBuilder.Append(prompt);
            }

            promptBuilder.Append($" │ 🚫 Press ESC to cancel │ ✅ Press Enter to confirm");
            return promptBuilder.ToString();
        }

        return returnWhenIsNotNull;
    }

    /// <summary>
    /// Clear behind left cursor.
    /// </summary>
    /// <param name="leftCursorAddSpaces">The left cursor add spaces.</param>
    public static void ClearBehindLeftCursor(int leftCursorAddSpaces)
    {
        var currentLineCursor = Console.CursorTop;
        var leftCursor = Console.CursorLeft + leftCursorAddSpaces + 1;
        Console.SetCursorPosition(leftCursor, Console.CursorTop);
        Console.Write(new string (' ', Console.WindowWidth + leftCursorAddSpaces));
        Console.SetCursorPosition(leftCursor, currentLineCursor);
    }

    /// <summary>
    /// Clear current console line.
    /// </summary>
    public static void ClearCurrentConsoleLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        var currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string (' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

    /// <summary>
    /// User must type.
    /// </summary>
    /// <param name="prompt">The prompt.</param>
    /// <param name="prefix">The prefix.</param>
    /// <returns>The user must type result.</returns>
    public static string UserMustType(string prompt, string prefix = "")
    {
        return UserMustType(prompt, true, false, prefix);
    }

    /// <summary>
    /// User can type.
    /// </summary>
    /// <param name="prompt">The prompt.</param>
    /// <param name="acceptableTyping">The acceptable typing.</param>
    /// <returns>The user can type result.</returns>
    public static string UserCanType(string prompt, params string[] acceptableTyping)
    {
        return UserMustType(prompt, true, true, acceptableTyping);
    }

    /// <summary>
    /// User can type.
    /// </summary>
    /// <param name="prompt">The prompt.</param>
    /// <param name="shouldAppendAfterEnter">The should append after enter.</param>
    /// <param name="acceptableTyping">The acceptable typing.</param>
    /// <returns>The user can type result.</returns>
    public static string UserCanType(string prompt, bool shouldAppendAfterEnter, params string[] acceptableTyping)
    {
        return UserMustType(prompt, shouldAppendAfterEnter, false, acceptableTyping);
    }

    private static string UserMustType(string prompt, bool shouldAppendAfterEnter, params string[] acceptableTyping)
    {
        return UserMustType(prompt, shouldAppendAfterEnter, false, acceptableTyping);
    }

    private static string UserMustType(string prompt, bool shouldAppendAfterEnter, bool canBeEmpty, params string[] acceptableTyping)
    {
        return UserMustTypePrefix(prompt, shouldAppendAfterEnter, canBeEmpty, "", acceptableTyping);
    }

    // Core input method. Returns empty string on failure, null when user force-stops.
    // Prompt should not end with colon. Acceptable typing can be null/empty for any input.
    private static string UserMustTypePrefix(string prompt, bool shouldAppendAfterEnter, bool canEnterEmptyText, string prefix = "", params string[] acceptableTyping)
    {
        var userInput = "";
        var fullPrompt = prefix + AskForEnter(prompt, shouldAppendAfterEnter, null);
        Console.WriteLine();
        Console.WriteLine(fullPrompt);
        StringBuilder stringBuilder = new();
        var previousKeyCode = 0;
        var keyCode = 0;
        while (true)
        {
            previousKeyCode = keyCode;
            keyCode = Console.ReadKey().KeyChar;
            if (keyCode == 8)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                    ClearBehindLeftCursor(-1);
                }
            }
            else if (keyCode == 27)
            {
                userInput = "";
                break;
            }
            else if (keyCode == 13)
            {
                if (acceptableTyping != null && acceptableTyping.Length != 0)
                    if (acceptableTyping.Contains(stringBuilder.ToString()))
                    {
                        userInput = stringBuilder.ToString();
                        break;
                    }

                var savedText = stringBuilder.ToString();
                if (savedText != "" || canEnterEmptyText)
                {
                    // Cant call trim or replace \b (any whitespace character), due to situation when insert "/// " for insert xml comments
                    userInput = savedText;
                    break;
                }

                stringBuilder = new StringBuilder();
            }
            else
            {
                stringBuilder.Append((char)keyCode);
            }
        }

        if (previousKeyCode != 32)
            userInput = userInput.Trim();
        userInput = SH.ConvertTypedWhitespaceToString(userInput.Trim('\0'));
        if (!string.IsNullOrWhiteSpace(userInput))
            if (previousKeyCode != 32)
                userInput = userInput.Trim();
        return userInput;
    }

    /// <summary>
    /// Gets or sets the is in clipboard.
    /// </summary>
    public static bool IsInClipboard { get; set; }

    /// <summary>
    /// Gets or sets the source.
    /// </summary>
    public static char Source { get; set; }

    private static void CheckWritingDuringClipboard()
    {
        if (IsInClipboard && Source != ClSources.Clipboard)
            Debugger.Break();
    }

    /// <summary>
    /// Gets the cursor top.
    /// </summary>
    public static int CursorTop => Console.CursorTop;

    /// <summary>
    /// Gets the window width.
    /// </summary>
    public static int WindowWidth => Console.WindowWidth;

    /// <summary>
    /// Gets the cursor left.
    /// </summary>
    public static int CursorLeft => Console.CursorLeft;

    /// <summary>
    /// Gets the error writer.
    /// </summary>
    public static TextWriter ErrorWriter => Console.Error;

    /// <summary>
    /// Gets the out.
    /// </summary>
    public static TextWriter Out => Console.Out;

    /// <summary>
    /// Gets the foreground color.
    /// </summary>
    public static ConsoleColor ForegroundColor { get => Console.ForegroundColor; set => Console.ForegroundColor = value; }

    /// <summary>
    /// Gets the buffer width.
    /// </summary>
    public static int BufferWidth => Console.BufferWidth;

    /// <summary>
    /// Gets the window height.
    /// </summary>
    public static int WindowHeight => Console.WindowHeight;

    private static ConsoleKeyInfo ReadKey()
    {
        return Console.ReadKey();
    }

    /// <summary>
    /// Read line.
    /// </summary>
    /// <returns>The read line result.</returns>
    public static string? ReadLine()
    {
        return Console.ReadLine();
    }

    private static void SetCursorPosition(int leftCursor, int cursorTop)
    {
        Console.SetCursorPosition(leftCursor, cursorTop);
    }

    /// <summary>
    /// Reset color.
    /// </summary>
    public static void ResetColor()
    {
        Console.ResetColor();
    }
}
