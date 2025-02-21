namespace SunamoCl;
public partial class CL
{
    public static string xPressEnterWhenDataWillBeInClipboard = "xPressEnterWhenDataWillBeInClipboard";
    private static volatile bool exit;
    private static readonly string charOfHeader = "*";
    public static bool perform = true;

    public static void Timer()
    {
        for (var i = 11; i > 0; i--)
        {
            var t = Task.Delay(i * 1000).ContinueWith(_ => WriteTimeLeft());
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
        var appeal = xSelectAction + ":";
        var i = 0;
        foreach (var kvp in actions)
        {
            WriteLine("[" + i + "]" + "  " + kvp.Key);
            i++;
        }
        var entered = UserMustTypeNumber(appeal, actions.Count - 1);
        if (entered == -1)
        {
            OperationWasStopped();
            return;
        }
        i = 0;
        string operation = null;
        foreach (var var in actions.Keys)
        {
            if (i == entered)
            {
                operation = var;
                break;
            }
            i++;
        }
        var act = actions[operation];
        act.Invoke();
    }

    private static void OperationWasStopped()
    {
        WriteLine("Operation was stopped.");
    }

    /// <summary>
    ///     First I must ask which is always from console - must prepare user to load data to clipboard.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="textFormat"></param>
    public static string LoadFromClipboardOrConsoleInFormat(string format, TextFormatDataCl textFormat)
    {
        string? s = null;
        if (!CmdApp.LoadFromClipboard)
        {
            s = UserMustTypeInFormat(format, textFormat);
        }
        else
        {
            s = ClipboardService.GetText();
        }
        return s;
    }

    /// <summary>
    ///     Will ask before getting data
    ///     First I must ask which is always from console - must prepare user to load data to clipboard.
    /// </summary>
    /// <param name="what"></param>
    public static string LoadFromClipboardOrConsole(string what, string prefix = "")
    {
        var imageFile = @"";
        AskForEnterWrite(what, true);
        WriteLine(xPressEnterWhenDataWillBeInClipboard);
        ReadLine();
        imageFile = ClipboardService.GetText();
        if (string.IsNullOrWhiteSpace(imageFile))
            imageFile = LoadFromClipboardOrConsole(what, "Entered text was empty or only whitespace. ");
        return imageFile;
    }
    /// <summary>
    ///     Return null when user force stop
    /// </summary>
    /// <param name="what"></param>
    /// <param name="textFormat"></param>
    public static string UserMustTypeInFormat(string what, TextFormatDataCl textFormat)
    {
        return UserMustType(what);
        #region Must be repaired first. DateToShort in ConsoleApp1 failed while parsing.
        //string entered = "";
        //while (true)
        //{
        //    entered = UserMustType(what);
        //    if (entered == null)
        //    {
        //        return null;
        //    }
        //    if (SH.HasTextRightFormat(entered, textFormat))
        //    {
        //        return entered;
        //    }
        //    else
        //    {
        //        ConsoleTemplateLogger.Instance.UnfortunatelyBadFormatPleaseTryAgain();
        //    }
        //}
        //return null; 
        #endregion
    }
    public static string SelectFromBrowsers(Action addBrowser)
    {
        ThrowEx.Custom("DUe to missing enum");
        return "";
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
        using (var s = Console.OpenStandardInput())
        using (var sr = new StreamReader(s))
        {
            Task readLineTask = sr.ReadLineAsync();
            Debug.WriteLine("hi");
            Console.WriteLine("hello");
            readLineTask.Wait(); // When not in Main method, you can use await. 
            // Waiting must happen in the curly brackets of the using directive.
        }
        Console.WriteLine("Bye Bye");
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
        StringBuilder sb = new();
        sb.AppendLine(stars);
        sb.AppendLine(text);
        sb.AppendLine(stars);
        var result = sb.ToString();
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
        var soubory = Directory.GetFiles(folder).ToList();
        var output = "";
        var selectedFile = SelectFromVariants(soubory, "file which you want to open");
        if (selectedFile == -1) return null;
        output = soubory[selectedFile];
        return output;
    }

    public static void PressEnterAfterInsertDataToClipboard(string what)
    {
        if (CmdApp.LoadFromClipboard)
        {
            AppealEnter("Insert " + what + " to clipboard");
        }
    }
    public static void Clear()
    {
        Console.Clear();
    }
    public static void CmdTable(IEnumerable<List<string>> last)
    {
        StringBuilder formattingString = new();
        var f = last.First();
        for (var i = 0; i < f.Count; i++) formattingString.Append("{" + i + ",5}|");
        formattingString.Append("|");
        var fs = formattingString.ToString();
        foreach (var item in last) Console.WriteLine(fs, item.ToArray());
    }

    public static void Pair(string v, string formatTo)
    {
        Console.WriteLine(v + ": " + formatTo);
    }
    public static void PressAnyKeyToContinue()
    {
        Console.WriteLine("Press any key to continue ...");
        Console.ReadLine();
    }
    /// <summary>
    ///     Ask user whether want to continue
    /// </summary>
    /// <param name="text"></param>
    public static DialogResult DoYouWantToContinue(string text)
    {
        text = FromKey("DoYouWantToContinue") + "?";
        Warning(text);
        var z = UserMustTypeYesNo(text).GetValueOrDefault();
        if (z) return DialogResult.Yes;
        return DialogResult.No;
    }
    /// <summary>
    ///     Print
    /// </summary>
    /// <param name="appeal"></param>
    public static void AppealEnter(string appeal)
    {
        Appeal(appeal + ". " + FromKey("ThenPressEnter") + ".");
        Console.ReadLine();
    }
    /// <summary>
    ///     Let user select action and run with A2 arg
    ///     EventHandler je zde správný protože EventHandler nikdy nemá Task
    /// </summary>
    public static void PerformAction(Dictionary<string, EventHandler> actions, object sender)
    {
        var listOfActions = NamesOfActions(actions);
        var selected = SelectFromVariants(listOfActions, FromKey("SelectActionToProceed") + ":");
        var ind = listOfActions[selected];
        var eh = actions[ind];
        if (sender == null) sender = selected;
        eh.Invoke(sender, EventArgs.Empty);
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

        StringBuilder sbAllActions = new();

        if (printAllActions)
        {
            sbAllActions = new();
            sbAllActions.AppendLine("All actions");
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
                    sbAllActions.AppendLine(item2.Key);
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
            Console.WriteLine(sbAllActions.ToString());
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
        return v;
    }

    /// <summary>
    ///     Return names of actions passed from keys
    /// </summary>
    /// <param name="actions"></param>
    private static List<string> NamesOfActions(Dictionary<string, EventHandler> actions)
    {
        List<string> ss = new();
        foreach (var var in actions) ss.Add(var.Key);
        return ss;
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
        AppealEnter(Messages.NoData);
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
        if (entered == "-1") return null;
        var znak = entered[0];
        if (char.ToLower(entered[0]) == 'y' || znak == '1') return true;
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
        for (var i = 0; i < variants.Count; i++)
            Console.WriteLine("[" + i + "]" + "  " + variants[i]);
        return UserMustTypeNumber(what, variants.Count - 1);
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
        StringBuilder sb = new();
        //string lastAdd = null;
        while ((line = Console.ReadLine()) != null)
        {
            if (line == "-1") break;
            sb.AppendLine(line);
            if (anotherPossibleAftermOne.Contains(line)) break;
            //lastAdd = line;
        }
        //sb.AppendLine(line);
        var s2 = sb.ToString().Trim();
        return s2;
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
            if (appendAfterEnter)
                whatOrTextWithoutEndingDot = FromKey("Enter") + " " + whatOrTextWithoutEndingDot + " ";
            whatOrTextWithoutEndingDot +=
                ". " + FromKey("ForExitEnter") + " -1. Is possible enter also nothing - just enter";
            return whatOrTextWithoutEndingDot;
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
        var z = "";
        whatOrTextWithoutEndingDot = prefix + AskForEnter(whatOrTextWithoutEndingDot, append, null);
        Console.WriteLine();
        Console.WriteLine(whatOrTextWithoutEndingDot);
        StringBuilder sb = new();
        var zadBefore = 0;
        var zad = 0;
        while (true)
        {
            zadBefore = zad;
            zad = Console.ReadKey().KeyChar;
            if (zad == 8)
            {
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                    // not delete visually, only move cursor about two back
                    //Console.Write('\b');
                    ClearBehindLeftCursor(-1);
                }
            }
            else if (zad == 27)
            {
                z = null;
                break;
            }
            else if (zad == 13)
            {
                if (acceptableTyping != null && acceptableTyping.Length != 0)
                    if (acceptableTyping.Contains(sb.ToString()))
                    {
                        z = sb.ToString();
                        break;
                    }
                var ulozit = sb.ToString();
                if (ulozit != "" || canBeEmpty)
                {
                    // Cant call trim or replace \b (any whitespace character), due to situation when insert "/// " for insert xml comments

                    z = ulozit;
                    break;
                }
                sb = new StringBuilder();
            }
            else
            {
                sb.Append((char)zad);
            }
        }
        // Tohle jsem nepochopil, jak mi to může načítat ze schránky v nugety který je jen pro cmd? 
        //if (z == string.Empty)
        //{
        //    z = ClipboardService.GetText();
        //    Information(i18n("AppLoadedFromClipboard") + " : " + z);
        //}
        if (zadBefore != 32) z = z.Trim();
        z = SH.ConvertTypedWhitespaceToString(z.Trim('\0'));
        if (!string.IsNullOrWhiteSpace(z))
            if (zadBefore != 32)
                z = z.Trim();
        return z;
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