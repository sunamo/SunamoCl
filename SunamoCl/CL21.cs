namespace SunamoCl;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public partial class CL
{
    public static async Task PressEnterAfterInsertDataToClipboard(ILogger logger, string what)
    {
        if (CmdApp.LoadFromClipboard)
        {
            await AppealEnter( "Insert " + what + " to clipboard");
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
        for (var index = 0; index < firstRow.Count; index++)
            formattingString.Append("{" + index + ",5}|");
        formattingString.Append("|");
        var formatString = formattingString.ToString();
        foreach (var item in last)
            Console.WriteLine(formatString, item.ToArray());
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
    /// <param name = "text"></param>
    public static DialogResult DoYouWantToContinue(string? text)
    {
        if (text == null)
        {
            text = FromKey("DoYouWantToContinue") + "?";
        }

        Warning(text);
        var userChoice = UserMustTypeYesNo(text).GetValueOrDefault();
        if (userChoice)
            return DialogResult.Yes;
        return DialogResult.No;
    }

    /// <summary>
    ///     Print
    /// </summary>
    /// <param name = "appeal"></param>
    public static async Task AppealEnter( string appeal)
    {
        Appeal(appeal + ". " + FromKey("ThenPressEnter") + ".");
        await ClNotify.FlashConsoleTitle();
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
        if (sender == null)
            sender = selected;
        eventHandler.Invoke(sender, EventArgs.Empty);
    }

    public static 
#if ASYNC
        async Task
#else
    void 
#endif
    PerformActionAfterRunCalling(object mode, Func<Dictionary<string, Func<Task<Dictionary<string, object>>>>> AddGroupOfActions, bool printAllActions)
    {
        if (mode == null)
            return;
        if (mode.ToString().Trim() == "")
            return;
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
    /// <param name = "actions"></param>
    private static List<string> NamesOfActions(Dictionary<string, EventHandler> actions)
    {
        List<string> actionsList = new();
        foreach (var actionItem in actions)
            actionsList.Add(actionItem.Key);
        return actionsList;
    }

    /// <summary>
    ///     Return int.MinValue when user force stop operation
    /// </summary>
    public static int UserMustTypeNumber(string what, int max, int min)
    {
        if (max > 999)
            ThrowEx.Custom("Max can be max 999 (creating serie of number could be too time expensive)");
        string entered = null;
        var isNumber = false;
        entered = UserMustType(what, false);
        if (entered == null)
            return int.MinValue;
        isNumber = int.TryParse(entered, out var parsed);
        while (!isNumber)
        {
            entered = UserMustType(what, false);
            isNumber = int.TryParse(entered, out parsed);
            if (parsed <= max && parsed >= min)
                break;
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
        if (entered == null)
            return int.MinValue;
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
    /// <param name = "text"></param>
    public static bool? UserMustTypeYesNo(string text)
    {
        var entered = UserMustType(text + " (Yes/No) ", false);
        // was pressed esc etc.
        if (entered == null)
            return false;
        // -1 removed - only ESC cancels operation
        var character = entered[0];
        if (char.ToLower(entered[0]) == 'y' || character == '1')
            return true;
        return false;
    }

    /// <summary>
    ///     A2 without ending :
    ///     Return index of selected action
    ///     Or int.MinValue when user force stop operation
    /// </summary>
    /// <param name = "variants"></param>
    /// <param name = "what"></param>
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
}