namespace SunamoCl;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public partial class CL
{
    /// <summary>
    ///     Return int.MinValue when user force stop operation
    ///     A1 without ending :
    /// </summary>
    /// <param name = "prompt">Text to display as prompt (without ending dot or colon)</param>
    /// <param name = "max"></param>
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
    /// Prompts user to enter multiple lines of text, stopping when a specific line is entered
    /// </summary>
    /// <param name="prompt">Text to display as prompt</param>
    /// <param name="breakEnteringAfterEntered">Lines that will stop input when entered</param>
    /// <returns>All entered lines combined as a single trimmed string</returns>
    public static string UserMustTypeMultiLine(string prompt, params string[] breakEnteringAfterEntered)
    {
        string? line = null;
        Information(AskForEnter(prompt, true, ""));
        StringBuilder stringBuilder = new();
        //string lastAdd = null;
        while ((line = Console.ReadLine()) != null)
        {
            // -1 removed - only ESC cancels operation
            stringBuilder.AppendLine(line);
            if (breakEnteringAfterEntered.Contains(line))
                break;
        //lastAdd = line;
        }

        //stringBuilder.AppendLine(line);
        var trimmedText = stringBuilder.ToString().Trim();
        return trimmedText;
    }

    /// <summary>
    /// Writes a prompt asking user to press Enter
    /// </summary>
    /// <param name="prompt">Text to display as prompt (without ending dot or colon)</param>
    /// <param name="shouldAppendAfterEnter">Whether to append "Enter" text after the prompt</param>
    public static void AskForEnterWrite(string prompt, bool shouldAppendAfterEnter)
    {
        WriteLine(AskForEnter(prompt, shouldAppendAfterEnter, null));
    }

    /// <summary>
    /// Constructs a formatted prompt string asking user to enter data
    /// </summary>
    /// <param name="prompt">Text to display as prompt (without ending dot or colon)</param>
    /// <param name="shouldAppendAfterEnter">Whether to append "Enter" prefix to the prompt</param>
    /// <param name="returnWhenIsNotNull">If not null, returns this value instead of constructing prompt</param>
    /// <returns>Formatted prompt string with instructions</returns>
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
    ///     Is A1 is negative => chars to remove
    /// </summary>
    /// <param name = "leftCursorAddSpaces"></param>
    public static void ClearBehindLeftCursor(int leftCursorAddSpaces)
    {
        var currentLineCursor = Console.CursorTop;
        var leftCursor = Console.CursorLeft + leftCursorAddSpaces + 1;
        Console.SetCursorPosition(leftCursor, Console.CursorTop);
        Console.Write(new string (' ', Console.WindowWidth + leftCursorAddSpaces));
        Console.SetCursorPosition(leftCursor, currentLineCursor);
    }

    /// <summary>
    /// Clears the current console line by writing spaces
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
    ///     if fail, return empty string.
    ///     Cant load multi line
    ///     Use Load
    ///     Vrátí to co skutečně zadá uživatel - "", -1, atd.
    ///     Musí se o zbytek postarat volající aplikace
    /// </summary>
    /// <param name = "prompt">Text to display as prompt (without ending dot or colon)</param>
    /// <param name = "prefix">Optional prefix text to prepend before the prompt</param>
    public static string UserMustType(string prompt, string prefix = "")
    {
        return UserMustType(prompt, true, false, prefix);
    }

    /// <summary>
    /// Prompts user to type text, accepting only specific values or allowing empty input
    /// </summary>
    /// <param name="prompt">Text to display as prompt (without ending dot or colon)</param>
    /// <param name="acceptableTyping">Acceptable values user can enter</param>
    /// <returns>User input string</returns>
    public static string UserCanType(string prompt, params string[] acceptableTyping)
    {
        return UserMustType(prompt, true, true, acceptableTyping);
    }

    /// <summary>
    /// Prompts user to type text with control over prompt formatting
    /// </summary>
    /// <param name="prompt">Text to display as prompt (without ending dot or colon)</param>
    /// <param name="shouldAppend">Whether to append "Enter" text to the prompt</param>
    /// <param name="acceptableTyping">Acceptable values user can enter</param>
    /// <returns>User input string</returns>
    public static string UserCanType(string prompt, bool shouldAppend, params string[] acceptableTyping)
    {
        return UserMustType(prompt, shouldAppend, false, acceptableTyping);
    }

    private static string UserMustType(string prompt, bool shouldAppend, params string[] acceptableTyping)
    {
        return UserMustType(prompt, shouldAppend, false, acceptableTyping);
    }

    private static string UserMustType(string prompt, bool shouldAppend, bool canBeEmpty, params string[] acceptableTyping)
    {
        return UserMustTypePrefix(prompt, shouldAppend, canBeEmpty, "", acceptableTyping);
    }

    /// <summary>
    ///     if fail, return empty string.
    ///     In A1 not end with :
    ///     Return null when user force stop
    ///     A2 are acceptable chars. Can be null/empty for anything
    /// </summary>
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
                    // not delete visually, only move cursor about two back
                    //Console.Write('\b');
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

        // Tohle jsem nepochopil, jak mi to může načítat ze schránky v nugety který je jen pro cmd?
        //if (z == string.Empty)
        //{
        //    z = ClipboardService.GetText();
        //    Information(i18n("AppLoadedFromClipboard") + " : " + z);
        //}
        if (previousKeyCode != 32)
            userInput = userInput.Trim();
        userInput = SH.ConvertTypedWhitespaceToString(userInput.Trim('\0'));
        if (!string.IsNullOrWhiteSpace(userInput))
            if (previousKeyCode != 32)
                userInput = userInput.Trim();
        return userInput;
    }

    /// <summary>
    /// Gets or sets whether writing to clipboard is in progress
    /// </summary>
    public static bool InClpb { get; set; }

    /// <summary>
    /// Gets or sets the current console source character
    /// </summary>
    public static char Src { get; set; }
    private static void IsWritingDuringClbp()
    {
        if (InClpb && Src != ClSources.A)
            Debugger.Break();
    }

    /// <summary>
    /// Gets the row position of the cursor within the buffer area
    /// </summary>
    public static int CursorTop => Console.CursorTop;

    /// <summary>
    /// Gets the width of the console window
    /// </summary>
    public static int WindowWidth => Console.WindowWidth;

    /// <summary>
    /// Gets the column position of the cursor within the buffer area
    /// </summary>
    public static int CursorLeft => Console.CursorLeft;

    /// <summary>
    /// Gets the standard error output stream
    /// </summary>
    public static TextWriter Error2 => Console.Error;

    /// <summary>
    /// Gets the standard output stream
    /// </summary>
    public static TextWriter Out => Console.Out;

    /// <summary>
    /// Gets or sets the foreground color of the console
    /// </summary>
    public static ConsoleColor ForegroundColor { get => Console.ForegroundColor; set => Console.ForegroundColor = value; }

    /// <summary>
    /// Gets the width of the buffer area
    /// </summary>
    public static int BufferWidth => Console.BufferWidth;

    /// <summary>
    /// Gets the height of the console window area
    /// </summary>
    public static int WindowHeight => Console.WindowHeight;

    //public static bool inClpb { get => cl.Console.inClpb; set => cl.Console.inClpb = value; }
    //public static char src { get => cl.Console.src; set => cl.Console.src = value; }
    private static ConsoleKeyInfo ReadKey()
    {
        return Console.ReadKey();
    }

    /// <summary>
    /// Reads the next line of characters from the standard input stream
    /// </summary>
    /// <returns>The next line of characters, or null if no more lines are available</returns>
    public static string? ReadLine()
    {
        return Console.ReadLine();
    }

    private static void SetCursorPosition(int leftCursor, int cursorTop)
    {
        Console.SetCursorPosition(leftCursor, cursorTop);
    }

    /// <summary>
    /// Sets the foreground and background console colors to their defaults
    /// </summary>
    public static void ResetColor()
    {
        Console.ResetColor();
    }
}