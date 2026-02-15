namespace SunamoCl;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public partial class CL
{
    /// <summary>
    /// Prompts user to press Enter after inserting data to clipboard
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="what">Description of data to insert</param>
    public static async Task PressEnterAfterInsertDataToClipboard(ILogger logger, string what)
    {
        if (CmdApp.LoadFromClipboard)
        {
            await AppealEnter( "Insert " + what + " to clipboard");
        }
    }

    /// <summary>
    /// Clears the console screen
    /// </summary>
    public static void Clear()
    {
        Console.Clear();
    }

    /// <summary>
    /// Displays data in a formatted table
    /// </summary>
    /// <param name="rows">Rows of data to display</param>
    public static void CmdTable(IEnumerable<List<string>> rows)
    {
        StringBuilder formattingString = new();
        var firstRow = rows.First();
        for (var index = 0; index < firstRow.Count; index++)
            formattingString.Append("{" + index + ",5}|");
        formattingString.Append("|");
        var formatString = formattingString.ToString();
        foreach (var item in rows)
            Console.WriteLine(formatString, item.ToArray());
    }

    /// <summary>
    /// Displays a label-value pair
    /// </summary>
    /// <param name="label">Label text</param>
    /// <param name="value">Value text</param>
    public static void Pair(string label, string value)
    {
        Console.WriteLine($"📊 {label}: {value}");
    }

    /// <summary>
    /// Waits for user to press any key to continue
    /// </summary>
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
    /// <param name = "message"></param>
    public static DialogResult DoYouWantToContinue(string? message)
    {
        if (message == null)
        {
            message = FromKey("DoYouWantToContinue") + "?";
        }

        Warning(message);
        var userChoice = UserMustTypeYesNo(message).GetValueOrDefault();
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

    /// <summary>
    /// Performs a specific action based on mode after initial run calling, iterating through action groups to find a match
    /// </summary>
    /// <param name="mode">The mode identifier to match against available actions</param>
    /// <param name="addGroupOfActionsFunc">Function that returns grouped actions as nested dictionaries</param>
    /// <param name="isPrintAllActions">Whether to print all available actions to console</param>
    public static
#if ASYNC
        async Task
#else
    void
#endif
    PerformActionAfterRunCalling(object mode, Func<Dictionary<string, Func<Task<Dictionary<string, object>>>>> addGroupOfActionsFunc, bool isPrintAllActions)
    {
        if (mode == null)
            return;
        if (mode.ToString()!.Trim() == "")
            return;
        Perform = false;
        var addGroupOfActions = addGroupOfActionsFunc();
        WriteLine("addGroupOfActions.Count: " + addGroupOfActions.Count);
        StringBuilder allActionsStringBuilder = new();
        if (isPrintAllActions)
        {
            allActionsStringBuilder = new();
            allActionsStringBuilder.AppendLine("All actions");
        }

        bool isRunning = false;
        foreach (var item in addGroupOfActions)
        {
            var actions = await item.Value();
            foreach (var actionEntry in actions)
            {
                if (isPrintAllActions)
                {
                    allActionsStringBuilder.AppendLine(actionEntry.Key);
                }

                if (actionEntry.Key == mode.ToString()!.Trim())
                {
                    isRunning = true;
                    var actionValue = actionEntry.Value;
                    await InvokeFuncTaskOrAction(actionValue);
                    if (!isPrintAllActions)
                    {
                        return;
                    }
                }
            }
        }

        if (isPrintAllActions)
        {
            Console.WriteLine(allActionsStringBuilder.ToString());
        }

        if (!isRunning)
        {
            //ThisApp.Error("No method to call was founded");
            Error("No method to call was founded");
        }

        Perform = true;
    }

    private static string FromKey(string key)
    {
        // Map keys to user-friendly messages
        return key switch
        {
            "Enter" => "Enter",
            "ForExitEnter" => "To exit, enter",
            "DoYouWantToContinue" => "Do you want to continue",
            "ThenPressEnter" => "Then press Enter",
            "SelectActionToProceed" => "Select action to proceed",
            _ => key
        };
    }

    /// <summary>
    ///     Return names of actions passed from keys
    /// </summary>
    /// <param name = "actions"></param>
    private static List<string> NamesOfActions(Dictionary<string, EventHandler> actions)
    {
        List<string> actionNames = new();
        foreach (var actionItem in actions)
            actionNames.Add(actionItem.Key);
        return actionNames;
    }

    /// <summary>
    ///     Return int.MinValue when user force stop operation
    /// </summary>
    public static int UserMustTypeNumber(string prompt, int max, int min)
    {
        if (max > 999)
            ThrowEx.Custom("Max can be max 999 (creating serie of number could be too time expensive)");
        string? entered = null;
        var isNumber = false;
        entered = UserMustType(prompt, false);
        if (entered == null)
            return int.MinValue;
        isNumber = int.TryParse(entered, out var parsed);
        while (!isNumber)
        {
            entered = UserMustType(prompt, false);
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
    /// <param name = "prompt"></param>
    public static int SelectFromVariants(List<string> variants, string prompt)
    {
        Console.WriteLine();
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║  📋 Select an option:                                  ║");
        Console.WriteLine("╠═══════════════════════════════════════════════════════╣");
        for (var index = 0; index < variants.Count; index++)
            Console.WriteLine($"║  [{index:D2}] {variants[index].PadRight(48)} ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
        return UserMustTypeNumber(prompt, variants.Count - 1);
    }

    /// <summary>
    /// Displays a list of variants for user selection and returns the selected variant as string
    /// </summary>
    /// <param name="variants">List of string variants to choose from</param>
    /// <param name="prompt">Text to display as selection prompt</param>
    /// <returns>The string value of the selected variant</returns>
    public static string SelectFromVariantsString(List<string> variants, string prompt)
    {
        var selected = SelectFromVariants(variants, prompt);
        return variants[selected];
    }
}