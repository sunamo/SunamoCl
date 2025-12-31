namespace SunamoCl;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public partial class CL
{
    /// <summary>
    ///     Return int.MinValue when user force stop operation
    ///     A1 without ending :
    /// </summary>
    /// <param name = "whatMustEnterWithoutEndingDot"></param>
    /// <param name = "max"></param>
    public static int UserMustTypeNumber(string whatMustEnterWithoutEndingDot, int max)
    {
        if (max > 999)
            ThrowEx.Custom("Max can be max 999 (creating serie of number could be too time expensive)");
        var entered = UserMustType(whatMustEnterWithoutEndingDot, false, false, Enumerable.Range(0, max + 1).OfType<string>().ToList().ToArray());
        if (whatMustEnterWithoutEndingDot == null)
            return int.MinValue;
        if (int.TryParse(entered, out var parsed))
            if (parsed <= max)
                return parsed;
        return UserMustTypeNumber(whatMustEnterWithoutEndingDot, max);
    }

    public static string UserMustTypeMultiLine(string promptText, params string[] breakEnteringAfterEntered)
    {
        string line = null;
        Information(AskForEnter(promptText, true, ""));
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

    public static void AskForEnterWrite(string what, bool shouldAppendAfterEnter)
    {
        WriteLine(AskForEnter(what, shouldAppendAfterEnter, null));
    }

    public static string AskForEnter(string whatMustEnterWithoutEndingDot, bool shouldAppendAfterEnter, string returnWhenIsNotNull)
    {
        if (returnWhenIsNotNull == null)
        {
            var prompt = new StringBuilder();
            if (shouldAppendAfterEnter)
            {
                prompt.Append($"📝 Enter {whatMustEnterWithoutEndingDot}");
            }
            else
            {
                prompt.Append(whatMustEnterWithoutEndingDot);
            }

            prompt.Append($" │ 🚫 Press ESC to cancel │ ✅ Press Enter to confirm");
            return prompt.ToString();
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
    /// <param name = "whatMustEnterWithoutEndingDot"></param>
    public static string UserMustType(string whatMustEnterWithoutEndingDot, string prefix = "")
    {
        return UserMustType(whatMustEnterWithoutEndingDot, true, false, prefix);
    }

    public static string UserCanType(string whatMustEnterWithoutEndingDot, params string[] acceptableTyping)
    {
        return UserMustType(whatMustEnterWithoutEndingDot, true, true, acceptableTyping);
    }

    public static string UserCanType(string whatMustEnterWithoutEndingDot, bool shouldAppend, params string[] acceptableTyping)
    {
        return UserMustType(whatMustEnterWithoutEndingDot, shouldAppend, false, acceptableTyping);
    }

    private static string UserMustType(string whatMustEnterWithoutEndingDot, bool shouldAppend, params string[] acceptableTyping)
    {
        return UserMustType(whatMustEnterWithoutEndingDot, shouldAppend, false, acceptableTyping);
    }

    private static string UserMustType(string whatMustEnterWithoutEndingDot, bool shouldAppend, bool canBeEmpty, params string[] acceptableTyping)
    {
        return UserMustTypePrefix(whatMustEnterWithoutEndingDot, shouldAppend, canBeEmpty, "", acceptableTyping);
    }

    /// <summary>
    ///     if fail, return empty string.
    ///     In A1 not end with :
    ///     Return null when user force stop
    ///     A2 are acceptable chars. Can be null/empty for anything
    /// </summary>
    private static string UserMustTypePrefix(string whatMustEnterWithoutEndingDot, bool shouldAppendAfterEnter, bool canEnterEmptyText, string prefix = "", params string[] acceptableTyping)
    {
        var userInput = "";
        whatMustEnterWithoutEndingDot = prefix + AskForEnter(whatMustEnterWithoutEndingDot, shouldAppendAfterEnter, null);
        Console.WriteLine();
        Console.WriteLine(whatMustEnterWithoutEndingDot);
        StringBuilder stringBuilder = new();
        var zadBefore = 0;
        var zad = 0;
        while (true)
        {
            zadBefore = zad;
            zad = Console.ReadKey().KeyChar;
            if (zad == 8)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                    // not delete visually, only move cursor about two back
                    //Console.Write('\b');
                    ClearBehindLeftCursor(-1);
                }
            }
            else if (zad == 27)
            {
                userInput = "";
                break;
            }
            else if (zad == 13)
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
                stringBuilder.Append((char)zad);
            }
        }

        // Tohle jsem nepochopil, jak mi to může načítat ze schránky v nugety který je jen pro cmd? 
        //if (z == string.Empty)
        //{
        //    z = ClipboardService.GetText();
        //    Information(i18n("AppLoadedFromClipboard") + " : " + z);
        //}
        if (zadBefore != 32)
            userInput = userInput.Trim();
        userInput = SH.ConvertTypedWhitespaceToString(userInput.Trim('\0'));
        if (!string.IsNullOrWhiteSpace(userInput))
            if (zadBefore != 32)
                userInput = userInput.Trim();
        return userInput;
    }

    public static bool InClpb { get; set; }
    public static char Src { get; set; }
    private static void IsWritingDuringClbp()
    {
        if (InClpb && Src != ClSources.A)
            Debugger.Break();
    }

    public static int CursorTop => Console.CursorTop;
    public static int WindowWidth => Console.WindowWidth;
    public static int CursorLeft => Console.CursorLeft;
    public static TextWriter Error2 => Console.Error;
    public static TextWriter Out => Console.Out;
    public static ConsoleColor ForegroundColor { get => Console.ForegroundColor; set => Console.ForegroundColor = value; }
    public static int BufferWidth => Console.BufferWidth;
    public static int WindowHeight => Console.WindowHeight;

    //public static bool inClpb { get => cl.Console.inClpb; set => cl.Console.inClpb = value; }
    //public static char src { get => cl.Console.src; set => cl.Console.src = value; }
    private static ConsoleKeyInfo ReadKey()
    {
        return Console.ReadKey();
    }

    public static string? ReadLine()
    {
        return Console.ReadLine();
    }

    private static void SetCursorPosition(int leftCursor, int cursorTop)
    {
        Console.SetCursorPosition(leftCursor, cursorTop);
    }

    public static void ResetColor()
    {
        Console.ResetColor();
    }
}