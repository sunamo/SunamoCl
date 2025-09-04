// Instance variables refactored according to C# conventions

namespace SunamoCl;
public partial class CL
{
    public static string xPressEnterWhenDataWillBeInClipboard = "📋 Press Enter when data will be copied to clipboard";
    private static volatile bool exit;
    private static readonly string charOfHeader = "*";
    public static bool perform = true;

    public static void Timer()
    {
        for (var index = 11; index > 0; index--)
        {
            var task = Task.Delay(index * 1000).ContinueWith(_ => WriteTimeLeft());
        }
    }

    /// <summary>
    /// Pokud zadaný soubor / složka neexistují, vrátí ""
    /// </summary>
    /// <param name="args"></param>
    /// <param name="takeSecondIfHaveMoreThanTwoParams"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string WorkingDirectoryFromArgs(string[] args, bool takeSecondIfHaveMoreThanTwoParams)
    {
        string csprojFolderInput = string.Empty;

        // PRVNÍ JE VŽDY MÓD
        if (args.Count() == 1)
        {
            csprojFolderInput = Environment.CurrentDirectory;
        }
        // mód + argument (např. PushToGitAndNuget {commit_msg}
        // tohle není dobrá ukázka protože commit_msg se zadává až poté. 
        // nedošlo mi to a kvůli tohoto toto celé vzniklo
        // pokud chci zadat složku ve které to poběží, pokud aplikace není dělaná "mód složka" musím --RunInDebug ve CommonArgs
        // nechám oba přístupy
        else if (args.Count() == 2)
        {
            if (Directory.Exists(args[1]) || File.Exists(args[1]))
            {
                csprojFolderInput = args[1];
            }
            else
            {
                csprojFolderInput = Environment.CurrentDirectory;
            }
            // už není potřeba
            //if (!Directory.Exists(csprojFolderInput))
            //{
            //    CL.WriteList(args, "args");
            //    throw new Exception("Folder does not exists!");
            //}
        }
        else if (args.Length == 0)
        {
            throw new Exception("Was not entered mode, args is empty");
        }
        else
        {
            if (takeSecondIfHaveMoreThanTwoParams)
            {
                if (Directory.Exists(args[1]) || File.Exists(args[1]))
                {
                    csprojFolderInput = args[1];
                }
                else
                {
                    csprojFolderInput = Environment.CurrentDirectory;
                }
            }

            // Toto by asi neměl být problém 
            //throw new Exception("args.Count have elements " + args.Count());
        }

        return FS.WithEndSlash(csprojFolderInput);
    }


    public static void SelectFromVariants(Dictionary<string, Action> actions, string xSelectAction)
    {
        var appealMessage = xSelectAction + ":";
        var index = 0;
        foreach (var kvp in actions)
        {
            WriteLine($"  [{index:D2}] 📌 {kvp.Key}");
            index++;
        }
        var enteredValue = UserMustTypeNumber(appealMessage, actions.Count - 1);
        if (enteredValue == -1)
        {
            OperationWasStopped();
            return;
        }
        index = 0;
        string operationName = null;
        foreach (var actionKey in actions.Keys)
        {
            if (index == enteredValue)
            {
                operationName = actionKey;
                break;
            }
            index++;
        }
        var selectedAction = actions[operationName];
        selectedAction.Invoke();
    }

    private static void OperationWasStopped()
    {
        WriteLine("❌ Operation was cancelled.");
    }



    /// <summary>
    ///     Will ask before getting data
    ///     First I must ask which is always from console - must prepare user to load data to clipboard.
    /// </summary>
    /// <param name="what"></param>
    public static string LoadFromClipboardOrConsole(string what)
    {
        var inputData = @"";

        // Display formatted prompt with icons
        Console.WriteLine();
        Console.WriteLine($"╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  📥 Input Required: {what.PadRight(33)} ║");
        Console.WriteLine($"╠═══════════════════════════════════════════════════════╣");
        Console.WriteLine($"║  Options:                                             ║");
        Console.WriteLine($"║  • 📋 Copy data to clipboard, then press Enter       ║");
        Console.WriteLine($"║  • ⌨️  Type directly in console                       ║");
        Console.WriteLine($"║  • ❌ Press ESC to cancel                             ║");
        Console.WriteLine($"╚═══════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.Write($"⏳ Waiting for clipboard data... ");

        ReadLine();
        inputData = ClipboardService.GetText();

        if (string.IsNullOrWhiteSpace(inputData))
        {
            Console.WriteLine();
            Console.WriteLine($"⚠️  Clipboard is empty or contains only whitespace");
            Console.Write($"✏️  Please type {what} manually: ");
            inputData = CL.UserMustType(what, "");
        }
        else
        {
            Console.WriteLine($"✅ Data loaded from clipboard");
        }

        return inputData;
    }

    public static string AskForFolder(string folderDbg, bool isDebug)
    {
        string folder = null;
        if (isDebug)
            folder = folderDbg;
        else
            folder = LoadFromClipboardOrConsole("folder");
        return folder;
    }
    public static List<string> AskForFolderMascRecFiles(string folderDbg, string mascDbg, bool recDbg, bool isDebug)
    {
        var (folder, masc, rec) = AskForFolderMascRec(folderDbg, mascDbg, recDbg, isDebug);
        return Directory.GetFiles(folder, masc, rec.Value ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
            .ToList();
    }
    public static (string folder, string masc, bool? rec) AskForFolderMascRec(string folderDbg, string mascDbg,
        bool? recDbg, bool isDebug)
    {
        string folder = null;
        string masc = null;
        bool? rec = false;
        if (isDebug)
        {
            folder = folderDbg;
            masc = mascDbg;
            rec = recDbg;
        }
        else
        {
            folder = LoadFromClipboardOrConsole("folder");
            masc = UserMustType("masc");
            recDbg = UserMustTypeYesNo("recursive");
        }
        return (folder, masc, rec);
    }

    public static void PressEnterToContinue2()
    {
        using (var standardInput = Console.OpenStandardInput())
        using (var streamReader = new StreamReader(standardInput))
        {
            Task readLineTask = streamReader.ReadLineAsync();
            Debug.WriteLine("hi");
            Console.WriteLine("✅ Process started successfully");
            readLineTask.Wait(); // When not in Main method, you can use await. 
            // Waiting must happen in the curly brackets of the using directive.
        }
        Console.WriteLine("👋 Goodbye!");
    }
    public static void PressEnterToContinue3()
    {
        Task.Factory.StartNew(() =>
        {
            while (Console.ReadKey().Key != ConsoleKey.Q) ;
            exit = true;
        });
        while (!exit)
        {
            // Do stuff
        }
    }
    /// <summary>
    ///     Return printed text
    /// </summary>
    /// <param name="text"></param>
    public static string StartRunTime(string text)
    {
        var textLength = text.Length;
        var stars = "";
        stars = new string(charOfHeader[0], textLength);
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine(stars);
        stringBuilder.AppendLine(text);
        stringBuilder.AppendLine(stars);
        var result = stringBuilder.ToString();
        Information(result);
        return result;
    }
    /// <summary>
    ///     Print and wait
    /// </summary>
    public static void EndRunTime(bool attempToRepairError = false)
    {
        if (attempToRepairError) Information(Messages.RepairErrors);
        Information(Messages.AppWillBeTerminated);
        Console.ReadLine();
    }
    /// <summary>
    ///     Return full path of selected file
    ///     or null when operation will be stopped
    /// </summary>
    /// <param name="folder"></param>
    public static string? SelectFile(string folder)
    {
        var filesList = Directory.GetFiles(folder).ToList();
        var outputPath = "";
        var selectedFile = SelectFromVariants(filesList, "file which you want to open");
        if (selectedFile == -1) return null;
        outputPath = filesList[selectedFile];
        return outputPath;
    }

    public static async Task PressEnterAfterInsertDataToClipboard(ILogger logger, string what)
    {
        if (CmdApp.LoadFromClipboard)
        {
            await AppealEnter(logger, "Insert " + what + " to clipboard");
        }
    }
    public static void Clear()
    {
        Console.Clear();
    }
    public static void CmdTable(IEnumerable<List<string>> last)
    {
        StringBuilder formattingString = new();
        var firstRow = last.First();
        for (var index = 0; index < firstRow.Count; index++) formattingString.Append("{" + index + ",5}|");
        formattingString.Append("|");
        var formatString = formattingString.ToString();
        foreach (var item in last) Console.WriteLine(formatString, item.ToArray());
    }

    public static void Pair(string v, string formatTo)
    {
        Console.WriteLine($"📊 {v}: {formatTo}");
    }
    public static void PressAnyKeyToContinue()
    {
        Console.WriteLine();
        Console.WriteLine("╔═══════════════════════════════════════════════╗");
        Console.WriteLine("║  ⏸️  Press any key to continue...             ║");
        Console.WriteLine("╚═══════════════════════════════════════════════╝");
        Console.ReadLine();
    }
    /// <summary>
    ///     Ask user whether want to continue
    /// </summary>
    /// <param name="text"></param>
    public static DialogResult DoYouWantToContinue(string? text)
    {
        if (text == null)
        {
            text = FromKey("DoYouWantToContinue") + "?";
        }

        Warning(text);
        var userChoice = UserMustTypeYesNo(text).GetValueOrDefault();
        if (userChoice) return DialogResult.Yes;
        return DialogResult.No;
    }
    /// <summary>
    ///     Print
    /// </summary>
    /// <param name="appeal"></param>
    public static async Task AppealEnter(ILogger logger, string appeal)
    {
        Appeal(appeal + ". " + FromKey("ThenPressEnter") + ".");
        await ClNotify.FlashConsoleTitle(logger);
    }
    /// <summary>
    /// Toto je potřeba pouze pokud aplikace má vlastní Mode.cs
    /// V opačném případě autorun při release řeší RunWithRunArgs
    /// 
    ///     Let user select action and run with A2 arg
    ///     EventHandler je zde správný protože EventHandler nikdy nemá Task
    /// </summary>
    public static void PerformAction(Dictionary<string, EventHandler> actions, object sender)
    {
        var listOfActions = NamesOfActions(actions);
        var selected = SelectFromVariants(listOfActions, FromKey("SelectActionToProceed") + ":");
        var actionIndex = listOfActions[selected];
        var eventHandler = actions[actionIndex];
        if (sender == null) sender = selected;
        eventHandler.Invoke(sender, EventArgs.Empty);
    }
    public static
#if ASYNC
        async Task
#else
    void
#endif
        PerformActionAfterRunCalling(
            object mode, Func<Dictionary<string, Func<Task<Dictionary<string, object>>>>> AddGroupOfActions, bool printAllActions)
    {
        if (mode == null) return;
        if (mode.ToString().Trim() == "") return;
        perform = false;

        var addGroupOfActions = AddGroupOfActions();
        WriteLine("addGroupOfActions.Count: " + addGroupOfActions.Count);

        StringBuilder allActionsStringBuilder = new();

        if (printAllActions)
        {
            allActionsStringBuilder = new();
            allActionsStringBuilder.AppendLine("All actions");
        }

        bool running = false;
        foreach (var item in addGroupOfActions)
        {
            //foreach (var item2 in item.Value)
            //{
            var actions = await item.Value();
            foreach (var item2 in actions)
            {
                if (printAllActions)
                {
                    allActionsStringBuilder.AppendLine(item2.Key);
                }

                if (item2.Key == mode.ToString().Trim())
                {
                    running = true;

                    var val = item2.Value;
                    await InvokeFuncTaskOrAction(val);
                    if (!printAllActions)
                    {
                        return;
                    }
                }
            }
        }

        if (printAllActions)
        {
            Console.WriteLine(allActionsStringBuilder.ToString());
        }

        if (!running)
        {
            //ThisApp.Error("No method to call was founded");
            Error("No method to call was founded");
        }

        perform = true;
    }
    private static string FromKey(string v)
    {
        // Map keys to user-friendly messages
        return v switch
        {
            "Enter" => "Enter",
            "ForExitEnter" => "To exit, enter",
            "DoYouWantToContinue" => "Do you want to continue",
            "ThenPressEnter" => "Then press Enter",
            "SelectActionToProceed" => "Select action to proceed",
            _ => v
        };
    }

    /// <summary>
    ///     Return names of actions passed from keys
    /// </summary>
    /// <param name="actions"></param>
    private static List<string> NamesOfActions(Dictionary<string, EventHandler> actions)
    {
        List<string> actionsList = new();
        foreach (var actionItem in actions) actionsList.Add(actionItem.Key);
        return actionsList;
    }

    /// <summary>
    ///     Return int.MinValue when user force stop operation
    /// </summary>
    public static int UserMustTypeNumber(string what, int max, int min)
    {
        if (max > 999) ThrowEx.Custom("Max can be max 999 (creating serie of number could be too time expensive)");
        string entered = null;
        var isNumber = false;
        entered = UserMustType(what, false);
        if (entered == null) return int.MinValue;
        isNumber = int.TryParse(entered, out var parsed);
        while (!isNumber)
        {
            entered = UserMustType(what, false);
            isNumber = int.TryParse(entered, out parsed);
            if (parsed <= max && parsed >= min) break;
        }
        return parsed;
    }
    /// <summary>
    ///     Return int.MinValue when user force stop operation
    /// </summary>
    public static int UserMustTypeNumber(int max)
    {
        const string whatUserMustEnter = "your choice as number";
        var entered = UserMustType(whatUserMustEnter, true);
        if (entered == null) return int.MinValue;
        if (int.TryParse(entered, out var parsed))
            if (parsed <= max)
                return parsed;
        return UserMustTypeNumber(whatUserMustEnter, max);
    }
    // Ty co jsou dal musí být ve cmd ale ještě to ověřit, TypedConsoleLogger by šlo zaměnit za metody Appeal atd.
    /// <summary>
    ///     Just print and wait
    /// </summary>
    public static void NoData()
    {
        Appeal(Messages.NoData);
        //ConsoleTemplateLogger.Instance.NoData();
    }
    /// <summary>
    ///     Pokud uz. zada Y,GT.
    ///     When N, return false.
    ///     When -1, return null
    /// </summary>
    /// <param name="text"></param>
    public static bool? UserMustTypeYesNo(string text)
    {
        var entered = UserMustType(text + " (Yes/No) ", false);
        // was pressed esc etc.
        if (entered == null) return false;
        // -1 removed - only ESC cancels operation
        var character = entered[0];
        if (char.ToLower(entered[0]) == 'y' || character == '1') return true;
        return false;
    }

    /// <summary>
    ///     A2 without ending :
    ///     Return index of selected action
    ///     Or int.MinValue when user force stop operation
    /// </summary>
    /// <param name="variants"></param>
    /// <param name="what"></param>
    public static int SelectFromVariants(List<string> variants, string what)
    {
        Console.WriteLine();
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║  📋 Select an option:                                  ║");
        Console.WriteLine("╠═══════════════════════════════════════════════════════╣");
        for (var index = 0; index < variants.Count; index++)
            Console.WriteLine($"║  [{index:D2}] {variants[index].PadRight(48)} ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
        return UserMustTypeNumber(what, variants.Count - 1);
    }

    public static string SelectFromVariantsString(List<string> variants, string what)
    {
        var selected = SelectFromVariants(variants, what);

        return variants[selected];
    }

    /// <summary>
    ///     Return int.MinValue when user force stop operation
    ///     A1 without ending :
    /// </summary>
    /// <param name="what"></param>
    /// <param name="max"></param>
    public static int UserMustTypeNumber(string what, int max)
    {
        if (max > 999) ThrowEx.Custom("Max can be max 999 (creating serie of number could be too time expensive)");
        var entered = UserMustType(what, false, false,
            Enumerable.Range(0, max + 1).OfType<string>().ToList().ToArray());
        if (what == null) return int.MinValue;
        if (int.TryParse(entered, out var parsed))
            if (parsed <= max)
                return parsed;
        return UserMustTypeNumber(what, max);
    }
    public static string UserMustTypeMultiLine(string v, params string[] anotherPossibleAftermOne)
    {
        string line = null;
        Information(AskForEnter(v, true, ""));
        StringBuilder stringBuilder = new();
        //string lastAdd = null;
        while ((line = Console.ReadLine()) != null)
        {
            // -1 removed - only ESC cancels operation
            stringBuilder.AppendLine(line);
            if (anotherPossibleAftermOne.Contains(line)) break;
            //lastAdd = line;
        }
        //sb.AppendLine(line);
        var trimmedText = stringBuilder.ToString().Trim();
        return trimmedText;
    }
    public static void AskForEnterWrite(string what, bool v)
    {
        WriteLine(AskForEnter(what, v, null));
    }
    public static string AskForEnter(string whatOrTextWithoutEndingDot, bool appendAfterEnter,
        string returnWhenIsNotNull)
    {
        if (returnWhenIsNotNull == null)
        {
            var prompt = new StringBuilder();

            if (appendAfterEnter)
            {
                prompt.Append($"📝 Enter {whatOrTextWithoutEndingDot}");
            }
            else
            {
                prompt.Append(whatOrTextWithoutEndingDot);
            }

            prompt.Append($" │ 🚫 Press ESC to cancel │ ✅ Press Enter to confirm");

            return prompt.ToString();
        }
        return returnWhenIsNotNull;
    }
    /// <summary>
    ///     Is A1 is negative => chars to remove
    /// </summary>
    /// <param name="leftCursorAdd"></param>
    public static void ClearBehindLeftCursor(int leftCursorAdd)
    {
        var currentLineCursor = Console.CursorTop;
        var leftCursor = Console.CursorLeft + leftCursorAdd + 1;
        Console.SetCursorPosition(leftCursor, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth + leftCursorAdd));
        Console.SetCursorPosition(leftCursor, currentLineCursor);
    }

    public static void ClearCurrentConsoleLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        var currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

    #region UserMustTypePrefix
    /// <summary>
    ///     if fail, return empty string.
    ///     Cant load multi line
    ///     Use Load
    ///     Vrátí to co skutečně zadá uživatel - "", -1, atd.
    ///     Musí se o zbytek postarat volající aplikace
    /// </summary>
    /// <param name="what"></param>
    public static string UserMustType(string what, string prefix = "")
    {
        return UserMustType(what, true, false, prefix);
    }
    public static string UserCanType(string whatOrTextWithoutEndingDot, params string[] acceptableTyping)
    {
        return UserMustType(whatOrTextWithoutEndingDot, true, true, acceptableTyping);
    }
    public static string UserCanType(string whatOrTextWithoutEndingDot, bool append, params string[] acceptableTyping)
    {
        return UserMustType(whatOrTextWithoutEndingDot, append, false, acceptableTyping);
    }
    private static string UserMustType(string whatOrTextWithoutEndingDot, bool append, params string[] acceptableTyping)
    {
        return UserMustType(whatOrTextWithoutEndingDot, append, false, acceptableTyping);
    }
    private static string UserMustType(string whatOrTextWithoutEndingDot, bool append, bool canBeEmpty,
        params string[] acceptableTyping)
    {
        return UserMustTypePrefix(whatOrTextWithoutEndingDot, append, canBeEmpty, "", acceptableTyping);
    }
    /// <summary>
    ///     if fail, return empty string.
    ///     In A1 not end with :
    ///     Return null when user force stop
    ///     A2 are acceptable chars. Can be null/empty for anything
    /// </summary>
    private static string UserMustTypePrefix(string whatOrTextWithoutEndingDot, bool append, bool canBeEmpty,
        string prefix = "", params string[] acceptableTyping)
    {
        var userInput = "";
        whatOrTextWithoutEndingDot = prefix + AskForEnter(whatOrTextWithoutEndingDot, append, null);
        Console.WriteLine();
        Console.WriteLine(whatOrTextWithoutEndingDot);
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
                if (savedText != "" || canBeEmpty)
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
        if (zadBefore != 32) userInput = userInput.Trim();
        userInput = SH.ConvertTypedWhitespaceToString(userInput.Trim('\0'));
        if (!string.IsNullOrWhiteSpace(userInput))
            if (zadBefore != 32)
                userInput = userInput.Trim();
        return userInput;
    }
    #endregion
    #region For easy copy from cl project
    public static bool inClpb;
    public static char src;

    private static void IsWritingDuringClbp()
    {
        if (inClpb && src != ClSources.a) Debugger.Break();
    }
    public static int CursorTop => Console.CursorTop;
    public static int WindowWidth => Console.WindowWidth;
    public static int CursorLeft => Console.CursorLeft;
    public static TextWriter Error2 => Console.Error;
    public static TextWriter Out => Console.Out;
    public static ConsoleColor ForegroundColor
    {
        get => Console.ForegroundColor;
        set => Console.ForegroundColor = value;
    }
    public static int BufferWidth => Console.BufferWidth;
    public static int WindowHeight => Console.WindowHeight;
    #endregion
    #region For easy copy from cl project
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
    #endregion


}