namespace SunamoCl;
public partial class CL
{
    public static string xPressEnterWhenDataWillBeInClipboard = "xPressEnterWhenDataWillBeInClipboard";
    private static volatile bool exit;
    private static readonly string charOfHeader = "*";
    public static bool perform = true;
    public static string s;
    public static StringBuilder sbToClear = new();
    public static StringBuilder sbToWrite = new();
    #region Nemám čas to předělávat ale do budoucna by se mělo předávat AddGroupOfActions. I když, nejlepší je nic nepředávat!
    /// <summary>
    ///     Bude naplněn ve AskUser
    /// </summary>
    private static Dictionary<string, Func<Task>> allActionsAsync = new();
    /// <summary>
    ///     Bude naplněn ve AskUser
    /// </summary>
    private static Dictionary<string, Action> allActions = new();
    #endregion
    private static bool wasCalledAskUser;
    static CL()
    {
    }
    public static void Timer()
    {
        for (var i = 11; i > 0; i--)
        {
            var t = Task.Delay(i * 1000).ContinueWith(_ => WriteTimeLeft());
        }
    }
    public static void WriteTimeLeft()
    {
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
    public static void OperationWasStopped()
    {
        //ConsoleTemplateLogger.Instance.OperationWasStopped();
    }
    /// <summary>
    ///     First I must ask which is always from console - must prepare user to load data to clipboard.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="textFormat"></param>
    public static string LoadFromClipboardOrConsoleInFormat(string format, TextFormatDataCl textFormat)
    {
        string s = null;
        //if (!CmdApp.loadFromClipboard)
        //{
        //    s = UserMustTypeInFormat(format, textFormat);
        //}
        //else
        //{
        s = ClipboardService.GetText();
        //}
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
        if (imageFile.Trim() == "")
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
    //public void PressEnterToContinue(CancellationToken cancellationToken)
    //{
    //    ConsoleKeyInfo cki = new ConsoleKeyInfo();
    //    do
    //    {
    //        // true hides the pressed character from the console
    //        cki = Console.ReadKey(true);
    //        // Wait for an ESC
    //    } while (cki.Key != ConsoleKey.Enter);
    //    // Cancel the token
    //    cancellationToken.Cancel();
    //}
    /// <summary>
    ///     Tohle můj problém nevyřešilo, po Entru se app vypne bez exc
    /// </summary>
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
    public static string SelectFile(string folder)
    {
        var soubory = Directory.GetFiles(folder).ToList();
        var output = "";
        var selectedFile = SelectFromVariants(soubory, "file which you want to open");
        if (selectedFile == -1) return null;
        output = soubory[selectedFile];
        return output;
    }
    public static void WriteLineFormat(string text, params object[] p)
    {
        Console.WriteLine();
        Console.WriteLine(text, p);
    }
    public static void PressEnterAfterInsertDataToClipboard(string what)
    {
        //if (CmdApp.loadFromClipboard)
        //{
        AppealEnter("Insert " + what + " to clipboard");
        //}
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
    public static void WriteList(IEnumerable<string> l, string header, WriteListArgs a = null)
    {
        Appeal(header);
        WriteList(l);
    }
    public static void WriteList(IEnumerable<string> l, WriteListArgs a = null)
    {
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
        text = i18n("DoYouWantToContinue") + "?";
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
        Appeal(appeal + ". " + i18n("ThenPressEnter") + ".");
        Console.ReadLine();
    }
    /// <summary>
    ///     Let user select action and run with A2 arg
    ///     EventHandler je zde správný protože EventHandler nikdy nemá Task
    /// </summary>
    public static
#if ASYNC
        async Task
#else
        void
#endif
        PerformAction(Dictionary<string, EventHandler> actions, object sender)
    {
        var listOfActions = NamesOfActions(actions);
        var selected = SelectFromVariants(listOfActions, i18n("SelectActionToProceed") + ":");
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
    private static string i18n(string v)
    {
        return v;
    }
    public static void WriteLineWithColor(ConsoleColor c, string v)
    {
        ForegroundColor = c;
        WriteLine(v);
        ResetColor();
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
    /// Může vrátit null když uživatel si nevybral z možností
    /// </summary>
    /// <param name="actions"></param>
    /// <returns></returns>
    public static
#if ASYNC
        async Task<string?>
#else
        string
#endif
        PerformActionAsync(Dictionary<string, object> actions)
    {
        var listOfActions = actions.Keys.ToList();
        return
#if ASYNC
            await
#endif
                PerformActionAsync(actions, listOfActions);
    }
    /// <summary>
    ///     A2 without ending :
    ///     Return index of selected action
    ///     Or int.MinValue when user force stop operation
    /// </summary>
    /// <param name="hodnoty"></param>
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
                whatOrTextWithoutEndingDot = i18n("Enter") + " " + whatOrTextWithoutEndingDot + " ";
            whatOrTextWithoutEndingDot +=
                ". " + i18n("ForExitEnter") + " -1. Is possible enter also nothing - just enter";
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

    /// <summary>
    /// Může vrátit null když uživatel si nevybral z možností
    /// </summary>
    /// <param name="actions"></param>
    /// <param name="listOfActions"></param>
    /// <returns></returns>
    private static
#if ASYNC
        async Task<string?>
#else
        string
#endif
        PerformActionAsync(Dictionary<string, object> actions, List<string> listOfActions)
    {
        if (listOfActions.Count > 1)
        {
            return await AskForActionAndRun(actions, listOfActions);
        }
        else
        {
            var actionName = listOfActions.First();
            if (actions.ContainsKey(actionName))
            {
                await InvokeFuncTaskOrAction(actions[actionName]);
                return actionName;
            }
            else
            {
                return await AskForActionAndRun(actions, listOfActions);
            }
        }
    }

    private static async Task<string?> AskForActionAndRun(Dictionary<string, object> actions, List<string> listOfActions)
    {
        var selected = SelectFromVariants(listOfActions, "Select action to proceed:");
        if (selected != -1)
        {
            var ind = listOfActions[selected];
            var eh = actions[ind];
#if ASYNC
            await
#endif
            InvokeFuncTaskOrAction(eh);
            return ind;
        }
        return null;
    }

    /// <summary>
    /// </summary>
    /// <param name="actions"></param>
    /// <param name="listOfActions"></param>
    /// <returns></returns>
    private static
#if ASYNC
        async Task<string>
#else
        string
#endif
        PerformAction(Dictionary<string, object> actions, List<string> listOfActions)
    {
        var selected = SelectFromVariants(listOfActions, "Select action to proceed:");
        if (selected != -1)
        {
            var ind = listOfActions[selected];
            var eh = actions[ind];
#if ASYNC
            await
#endif
            InvokeFuncTaskOrAction(eh);
            return ind;
        }
        return null;
    }
    public static void ClearCurrentConsoleLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        var currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

    internal static async Task AddToActions(Func<Dictionary<string, Func<Task<Dictionary<string, object>>>>> AddGroupOfActions)
    {
        var groupsOfActionsFromProgramCommon = AddGroupOfActions();
        perform = false;
        //AddGroupOfActions();
        foreach (var item in groupsOfActionsFromProgramCommon)
        {
            // zde pokud bude CL.perform == false, jen mi získá módy
            // jinak 
            var itemValue = item.Value();
            var s = await itemValue;
            //Console.WriteLine("groupsOfActionsFromProgramCommon.item.Key: " + item.Key);
            //Console.WriteLine("groupsOfActionsFromProgramCommon.item.Value.Count: " + s.Count);
            foreach (var item2 in s)
            {
                var o = item2.Value;
                var t = o.GetType();
                if (t == TypesDelegates.tAction)
                {
                    var oAction = o as Action;
                    // Nevím jak jsem mohl být takový blb. Tu byla ta chyba - toto nemůžu volat protože v tom delegátu už nekontroluji na CL.perform! Dictionary jsem si rozbalil už v await itemValue o pár řádků výše!
                    //oAction.Invoke();
                    if (item2.Key != "None")
                    {
                        ThrowEx.KeyAlreadyExists(allActions, item2.Key, nameof(allActions));
                        allActions.Add(item2.Key, oAction);
                    }
                }
                else if (t == TypesDelegates.tFuncTask)
                {
                    var taskVoid = o as Func<Task>;
                    // Nevím jak jsem mohl být takový blb. Tu byla ta chyba - toto nemůžu volat protože v tom delegátu už nekontroluji na CL.perform! Dictionary jsem si rozbalil už v await itemValue o pár řádků výše!
                    //await taskVoid();
                    if (item2.Key != "None")
                    {
                        ThrowEx.KeyAlreadyExists(allActionsAsync, item2.Key, nameof(allActionsAsync));
                        allActionsAsync.Add(item2.Key, taskVoid);
                    }
                }
            }
            //#if ASYNC
            //                    await
            //#endif
            //                        InvokeFuncTaskOrAction(item.Value);
        }

        perform = true;
    }

    internal static async Task<string> RunActionWithName(string whatUserNeed)
    {
        string mode = string.Empty;
        var potentiallyValid = new Dictionary<string, Action>();
        var potentiallyValidAsync = new Dictionary<string, Func<Task>>();

        foreach (var item in allActions)
            if (SH.ContainsCl(item.Key, whatUserNeed, SearchStrategy.AnySpaces))
                potentiallyValid.Add(item.Key, item.Value);
        foreach (var item in allActionsAsync)
            if (SH.ContainsCl(item.Key, whatUserNeed, SearchStrategy.AnySpaces))
                potentiallyValidAsync.Add(item.Key, item.Value);

        if (potentiallyValid.Count == 0 && potentiallyValidAsync.Count == 0)
        {
            Information(i18n(XlfKeys.NoActionWasFound));
            WriteList(allActions.Keys.ToList(), "Available Actions");
            WriteList(allActionsAsync.Keys.ToList(), "Available Async Actions");
        }
        else
        {
            WriteList(potentiallyValid.Keys.ToList(), "potentiallyValid");
            WriteList(potentiallyValidAsync.Keys.ToList(), "potentiallyValidAsync");

            var actionsMerge = AsyncHelper.MergeDictionaries(potentiallyValid, potentiallyValidAsync);
            mode = await PerformActionAsync(actionsMerge);
        }

        return mode;
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
    public static void WriteLine(string a)
    {
        IsWritingDuringClbp();
        Console.WriteLine(a);
    }
    public static void WriteLine(int a)
    {
        IsWritingDuringClbp();
        Console.WriteLine(a.ToString());
    }
    public static void Write(string v)
    {
        IsWritingDuringClbp();
        Console.Write(v);
    }
    public static void Write(char v)
    {
        IsWritingDuringClbp();
        Console.Write(v);
    }
    public static void WriteLine()
    {
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
        IsWritingDuringClbp();
        Console.WriteLine(correlationId.ToString());
    }
    public static void Write(string format, string left, object right)
    {
        IsWritingDuringClbp();
        Console.Write(format, left, right);
    }
    public static void Log(string a, params object[] o)
    {
        IsWritingDuringClbp();
        Console.WriteLine(a, o);
    }
    public static void WriteLine(string a, params object[] o)
    {
        IsWritingDuringClbp();
        Console.WriteLine(a, o);
    }
    /// <summary>
    ///     Good to be in CLConsole even if dont just call Console
    /// </summary>
    /// <param name="ex"></param>
    public static void WriteLine(Exception ex)
    {
        IsWritingDuringClbp();
        //Console.WriteLine(Exceptions.TextOfExceptions(ex));
        Console.WriteLine(ex.Message);
    }
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
    public static string ReadLine()
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
    /// <summary>
    ///     for compatibility with CL.WriteLine
    /// </summary>
    //public static void WriteLine(string what)
    //{
    //    if (what != null)
    //    {
    //        // musí tu být CL, protože this.WriteLine bere object takže mi zavolá sebe sama 
    //        // jinak ale CL dědí od CL kde je WriteLine
    //        CL.WriteLine(what);
    //    }
    //}
    #region Progress bar
    private const char _block = '■';
    private const string _back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";
    private static int _backL;
    private const string _twirl = "-\\|/";
    /// <summary>
    ///     1
    /// </summary>
    public static void WriteProgressBarInit()
    {
        _backL = _back.Length;
    }
    /// <summary>
    ///     2
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="a"></param>
    public static void WriteProgressBar(double percent, WriteProgressBarArgs a = null)
    {
        WriteProgressBar((int)percent, a);
    }
    static object sbToClearLock = new object();
    /// <summary>
    ///     3
    ///     Usage:
    ///     WriteProgressBar(0);
    ///     WriteProgressBar(i, true);
    /// </summary>
    public static void WriteProgressBar(int percent, WriteProgressBarArgs a = null)
    {
        if (a == null) a = WriteProgressBarArgs.Default;
        if (a.update)
        {
            lock (sbToClearLock)
            {
                sbToClear.Clear();
                //sbToClear.Append( string.Empty.PadRight(s.Length, '\b'));
                sbToClear.Append(_back);
                sbToClear.Append(string.Empty.PadRight(s.Length - _backL, '\b'));
                var ts = sbToClear.ToString();
                Write(ts);
            }
        }
        Write("[");
        var p = (int)(percent / 10f + .5f);
        for (var i = 0; i < 10; ++i)
            if (i >= p)
                Write(' ');
            else
                Write(_block);
        if (a.writePieces)
            s = "] {0,3:##0}%" + $" {a.actual} / {a.overall}";
        else
            s = "] {0,3:##0}%";
        var fr = string.Format(s, percent);
        Write(fr);
    }
    //private static void Write(char v)
    //{
    //    CL.Write(v);
    //}
    /// <summary>
    ///     4
    /// </summary>
    public static void WriteProgressBarEnd()
    {
        WriteProgressBar(100, new WriteProgressBarArgs(true));
        WriteLine();
    }
    #endregion
}