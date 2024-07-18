
namespace SunamoCl;
public partial class CL
{
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
        string appeal = xSelectAction + ":";
        int i = 0;
        foreach (KeyValuePair<string, Action> kvp in actions)
        {
            CL.WriteLine(AllStrings.lsqb + i + AllStrings.rsqb + "  " + kvp.Key);
            i++;
        }

        int entered = UserMustTypeNumber(appeal, actions.Count - 1);
        if (entered == -1)
        {
            OperationWasStopped();
            return;
        }

        i = 0;
        string operation = null;
        foreach (string var in actions.Keys)
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
    /// First I must ask which is always from console - must prepare user to load data to clipboard. 
    /// </summary>
    /// <param name="format"></param>
    /// <param name="textFormat"></param>
    public static string LoadFromClipboardOrConsoleInFormat(string format, TextFormatDataCl textFormat)
    {
        string s = null;

        if (!CmdApp.loadFromClipboard)
        {
            s = UserMustTypeInFormat(format, textFormat);
        }
        else
        {
            s = ClipboardHelper.GetText();
        }

        return s;
    }

    /// <summary>
    /// Will ask before getting data
    /// First I must ask which is always from console - must prepare user to load data to clipboard. 
    /// </summary>
    /// <param name="what"></param>
    public static string LoadFromClipboardOrConsole(string what, string prefix = "")
    {
        string imageFile = @"";

        if (!CmdApp.loadFromClipboard)
        {
            imageFile = UserMustType(what, prefix);
        }
        else
        {
            AskForEnterWrite(what, true);
            CL.WriteLine(xPressEnterWhenDataWillBeInClipboard);
            ReadLine();
            imageFile = ClipboardHelper.GetText();
        }

        if (imageFile.Trim() == "")
        {
            imageFile = LoadFromClipboardOrConsole(what, "Entered text was empty or only whitespace. ");
        }

        return imageFile;
    }


    /// <summary>
    /// Return null when user force stop 
    /// </summary>
    /// <param name = "what"></param>
    /// <param name = "textFormat"></param>
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
        {
            folder = folderDbg;
        }
        else
        {
            folder = LoadFromClipboardOrConsole("folder");
        }

        return folder;
    }

    public static List<string> AskForFolderMascRecFiles(string folderDbg, string mascDbg, bool recDbg, bool isDebug)
    {
        var (folder, masc, rec) = AskForFolderMascRec(folderDbg, mascDbg, recDbg, isDebug);
        return Directory.GetFiles(folder, masc, rec.Value ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList();
    }

    public static string xPressEnterWhenDataWillBeInClipboard = "xPressEnterWhenDataWillBeInClipboard";

    public static (string folder, string masc, bool? rec) AskForFolderMascRec(string folderDbg, string mascDbg, bool? recDbg, bool isDebug)
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

    private static volatile bool exit;

    private static readonly string charOfHeader = AllStrings.asterisk;

    public static bool perform = true;
    public static string s = null;
    public static StringBuilder sbToClear = new();
    public static StringBuilder sbToWrite = new();


    static CL()
    {
    }


    /// <summary>
    /// </summary>
    public static Func<string, string> i18n { get; set; }

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

    public static void WriteList(IEnumerable<string> l, string header)
    {
        WriteLine(header);
        WriteList(l);
    }

    public static void WriteList(IEnumerable<string> l)
    {
        foreach (var item in l) Console.WriteLine(item);
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
    /// <param name="akce"></param>
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
    /// <param name="what"></param>
    public static int UserMustTypeNumber(string what, int max, int min)
    {
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
    /// <param name="vyzva"></param>
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

    public static
#if ASYNC
        async Task<string>
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
            Console.WriteLine(AllStrings.lsqb + i + AllStrings.rsqb + "  " + variants[i]);

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

    public static string AskForEnter(string whatOrTextWithoutEndingDot, bool appendAfterEnter, string returnWhenIsNotNull)
    {
        if (returnWhenIsNotNull == null)
        {
            if (appendAfterEnter) whatOrTextWithoutEndingDot = i18n("Enter") + " " + whatOrTextWithoutEndingDot + "";

            whatOrTextWithoutEndingDot += ". " + i18n("ForExitEnter") + " -1.";
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
        Console.Write(new string(AllChars.space, Console.WindowWidth + leftCursorAdd));
        Console.SetCursorPosition(leftCursor, currentLineCursor);
    }

    private static
#if ASYNC
        async Task<string>
#else
        string
#endif
        PerformActionAsync(Dictionary<string, object> actions, List<string> listOfActions)
    {
        var selected = SelectFromVariants(listOfActions, "Select action to proceed:");
        if (selected != -1)
        {
            var ind = listOfActions[selected];
            var eh = actions[ind];



            return ind;
        }

        return null;
    }

    public static
#if ASYNC
        async Task
#else
        void
#endif
        InvokeFuncTaskOrAction(object o)
    {
        var t = o.GetType();

        if (t == Types.tAction)
        {
            (o as Action).Invoke();
        }
        else if (t == TypesDelegates.tFuncTask)
        {
            var taskVoid = o as Func<Task>;
            await taskVoid(); ;
        }
    }

    /// <summary>
    ///     Musí se typovat Dictionary
    ///     <string, object>
    ///         object, ne Func<Task> ani Action
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

    #region UserMustTypePrefix

    /// <summary>
    ///     if fail, return empty string.
    ///     Cant load multi line
    ///     Use Load
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
    ///     Potřebuji to tu protože ze schránky i načítám
    /// </summary>
    public static IClipboardHelperCl ClipboardHelper;

    /// <summary>
    ///     if fail, return empty string.
    ///     In A1 not end with :
    ///     Return null when user force stop
    ///     A2 are acceptable chars. Can be null/empty for anything
    /// </summary>
    /// <param name="whatOrTextWithoutEndingDot"></param>
    /// <param name="append"></param>
    private static string UserMustTypePrefix(string whatOrTextWithoutEndingDot, bool append, bool canBeEmpty,
        string prefix = "", params string[] acceptableTyping)
    {
        var z = "";
        whatOrTextWithoutEndingDot = prefix + AskForEnter(whatOrTextWithoutEndingDot, append, "");

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
                    //Console.Write(AllChars.bs2);
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
                    /// Cant call trim or replace \b (any whitespace character), due to situation when insert "/// " for insert xml comments
                    //ulozit = ulozit.Replace("\b", "");
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

        if (z == string.Empty)
        {
            z = ClipboardHelper.GetText();
            Information(i18n("AppLoadedFromClipboard") + " : " + z);
        }

        if (zadBefore != 32) z = z.Trim();

        z = SH.ConvertTypedWhitespaceToString(z.Trim(AllChars.st));

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
        if (inClpb && src != ClSources.a) System.Diagnostics.Debugger.Break();
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
    /// Return None if !A1
    /// If allActions will be null, will not automatically run action
    /// </summary>
    /// <param name="askUser"></param>
    /// <param name="AddGroupOfActions"></param>
    /// <param name="allActions"></param>
    public static
#if ASYNC
        // nevím proč jsem to zakomentoval, příště si to tu zapsat
        async Task<string>
        //string
#else
    string
#endif
        AskUser(bool askUser, Func<Dictionary<string, Func<Task>>> AddGroupOfActions, Dictionary<string, Action> allActions, Dictionary<string, Func<Task>> allActionsAsync, Dictionary<string, object> groupsOfActionsFromProgramCommon)
    {
        string mode = null;
        // must be called in all cases!!
        var d = AddGroupOfActions();

        /*
groupsOfActionsFromProgramCommon bude po novu null
        proto tento kód zakomentuji

        ale to nejde, protože ho potřebuji niže 
        přes AsyncHelper.InvokeFuncTaskOrAction potřebuji naplnit allActions a allActionsAsync
        */

        foreach (var item in d)
        {
            groupsOfActionsFromProgramCommon.Add(item.Key, item.Value);
        }

        if (askUser)
        {
            bool? loadFromClipboard = false;
            //if (ThisApp.Name != "AllProjectsSearch")
            //{
            //    loadFromClipboard = CL.UserMustTypeYesNo(i18n(XlfKeys.DoYouWantLoadDataOnlyFromClipboard) + " " + i18n(XlfKeys.MultiLinesTextCanBeLoadedOnlyFromClipboardBecauseConsoleAppRecognizeEndingWhitespacesLikeEnter));
            //}

            CmdApp.loadFromClipboard = loadFromClipboard.Value;

            if (loadFromClipboard.HasValue)
            {
                var whatUserNeed = "format";
                // na začátku zadám fulltextový řetězec co chci nebo -1 abych měl možnost vybrat ze všech možností
                whatUserNeed = UserMustType("you need or enter -1 for select from all groups");

                if (whatUserNeed == "-1")
                {
                    CL.WriteLine("Nechám uživatele vybrat ze všech možností (zadal -1), perform je: " + perform);
                    CL.WriteLine("Zatím jsem to zakomentoval, mám teď jiné věci na řešení");

                    //CL.PerformActionAsync(groupsOfActionsFromProgramCommon);
                }
                else
                {
                    //
                    perform = false;
                    //AddGroupOfActions();

                    foreach (var item in groupsOfActionsFromProgramCommon)
                    {

#if ASYNC
                        await
#endif
                            InvokeFuncTaskOrAction(item.Value);
                    }

                    Dictionary<string, Action> potentiallyValid = new Dictionary<string, Action>();
                    Dictionary<string, Func<Task>> potentiallyValidAsync = new Dictionary<string, Func<Task>>();
                    foreach (var item in allActions)
                    {
                        if (item.Key.Contains(whatUserNeed) /*.Contains(item.Key, whatUserNeed, SearchStrategy.AnySpaces, false)*/)
                        {
                            potentiallyValid.Add(item.Key, item.Value);
                        }
                    }

                    foreach (var item in allActionsAsync)
                    {
                        if (item.Key.Contains(whatUserNeed) /* .Contains(item.Key, whatUserNeed, SearchStrategy.AnySpaces, false)*/)
                        {
                            potentiallyValidAsync.Add(item.Key, item.Value);
                        }
                    }

                    if (potentiallyValid.Count == 0 && potentiallyValidAsync.Count == 0)
                    {
                        Information(i18n(XlfKeys.NoActionWasFound));
                    }
                    else
                    {
                        //if (potentiallyValid.Any())
                        //{
                        //    mode = CL.PerformAction(potentiallyValid);
                        //}
                        //else
                        //{
                        //mode = CL.PerformActionAsync(potentiallyValidAsync);
                        //}


                        // je zajímave že při tomhle se vypíše to co je v potentiallyValid
                        // není, on to prostě vypíše a čeká
                        // musím to tu zkombinovat!

                        var actionsMerge = AsyncHelper.MergeDictionaries(potentiallyValid, potentiallyValidAsync);

                        mode =
#if ASYNC
                            await
#endif
                                PerformActionAsync(actionsMerge);
                    }
                }
            }
            return mode;
        }
        else
        {
            /*
Zde vůbec nevím co se děje
            To je tím že jsem si nepsal žádné komentáře
            ale na 99%, nechá mě to vybrat skupinu (Dating, Other, atd.)
            ze které později vyberu akci
            */

            var before = perform;
            perform = false;
            foreach (var item in d)
            {

#if ASYNC
                await
#endif

                    InvokeFuncTaskOrAction(item.Value);
            }
            perform = before;

            return mode;
        }
    }

    /// <summary>
    /// for compatibility with CL.WriteLine 
    /// </summary>
    /// <param name = "what"></param>
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
    const char _block = '■';
    const string _back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";
    static int _backL = 0;
    const string _twirl = "-\\|/";





    /// <summary>
    /// 1
    /// </summary>
    public static void WriteProgressBarInit()
    {
        _backL = _back.Length;
    }

    /// <summary>
    /// 2
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="a"></param>
    public static void WriteProgressBar(double percent, WriteProgressBarArgs a = null)
    {
        WriteProgressBar((int)percent, a);
    }

    /// <summary>
    /// 3
    /// Usage:
    /// WriteProgressBar(0);
    /// WriteProgressBar(i, true);
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="update"></param>
    public static void WriteProgressBar(int percent, WriteProgressBarArgs a = null)
    {
        if (a == null)
        {
            a = WriteProgressBarArgs.Default;
        }

        if (a.update)
        {
            sbToClear.Clear();

            //sbToClear.Append( string.Empty.PadRight(s.Length, '\b'));

            sbToClear.Append((string)_back);
            sbToClear.Append(string.Empty.PadRight(s.Length - _backL, '\b'));

            var ts = sbToClear.ToString();

            Write(ts);
        }

        Write("[");
        var p = (int)(percent / 10f + .5f);
        for (var i = 0; i < 10; ++i)
        {
            if (i >= p)
                CL.Write(' ');
            else
                Write((char)_block);
        }

        if (a.writePieces)
        {
            s = "] {0,3:##0}%" + $" {a.actual} / {a.overall}";
        }
        else
        {
            s = "] {0,3:##0}%";
        }

        string fr = string.Format(s, percent);

        Write(fr);
    }

    //private static void Write(char v)
    //{
    //    CL.Write(v);
    //}

    /// <summary>
    /// 4
    /// </summary>
    public static void WriteProgressBarEnd()
    {
        WriteProgressBar(100, new WriteProgressBarArgs(true));
        WriteLine();
    }


    #endregion


}
