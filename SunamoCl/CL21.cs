namespace SunamoCl;

public partial class CL
{
    public static async Task PressEnterAfterInsertDataToClipboard(string what)
    {
        if (CmdApp.ShouldLoadFromClipboard)
        {
            await AppealEnter( "Insert " + what + " to clipboard");
        }
    }

    public static void Clear()
    {
        Console.Clear();
    }

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

    public static void Pair(string label, string value)
    {
        Console.WriteLine($"📊 {label}: {value}");
    }

    public static void PressAnyKeyToContinue()
    {
        Console.WriteLine();
        Console.WriteLine("╔═══════════════════════════════════════════════╗");
        Console.WriteLine("║  ⏸️  Press any key to continue...             ║");
        Console.WriteLine("╚═══════════════════════════════════════════════╝");
        Console.ReadLine();
    }

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

    public static async Task AppealEnter( string appeal)
    {
        Appeal(appeal + ". " + FromKey("ThenPressEnter") + ".");
        await ClNotify.FlashConsoleTitle();
    }

    // Lets user select an action and runs it. Only needed when the application has its own Mode.cs.
    // Otherwise, autorun at release is handled by RunWithRunArgs.
    // EventHandler is correct here because EventHandler never has a Task return type.
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
        async Task
    PerformActionAfterRunCalling(object mode, Func<Dictionary<string, Func<Task<Dictionary<string, object>>>>> addGroupOfActionsFunc, bool isPrintAllActions)
    {
        if (mode == null)
            return;
        if (mode.ToString()!.Trim() == "")
            return;
        ShouldPerform = false;
        var actionGroups = addGroupOfActionsFunc();
        WriteLine("actionGroups.Count: " + actionGroups.Count);
        StringBuilder allActionsStringBuilder = new();
        if (isPrintAllActions)
        {
            allActionsStringBuilder = new();
            allActionsStringBuilder.AppendLine("All actions");
        }

        bool isRunning = false;
        foreach (var item in actionGroups)
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
            Error("No method to call was found");
        }

        ShouldPerform = true;
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

    private static List<string> NamesOfActions(Dictionary<string, EventHandler> actions)
    {
        List<string> actionNames = new();
        foreach (var actionItem in actions)
            actionNames.Add(actionItem.Key);
        return actionNames;
    }

    // Return int.MinValue when user force stop operation
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

    // Return int.MinValue when user force stop operation
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

    public static void NoData()
    {
        Appeal(Messages.NoData);
    }

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

    public static int SelectFromVariants(List<string> variants, string prompt)
    {
        // Zobrazený seznam lze zúžit napsáním textu (filtr). Číslo pak vybírá ze zúženého seznamu,
        // proto se drží mapování zobrazený index -> původní index do variants.
        var visibleIndices = Enumerable.Range(0, variants.Count).ToList();
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║  📋 Select an option:                                  ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════╣");
            for (var displayIndex = 0; displayIndex < visibleIndices.Count; displayIndex++)
                Console.WriteLine($"║  [{displayIndex:D2}] {variants[visibleIndices[displayIndex]].PadRight(48)} ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
            Console.WriteLine("Type a number to select, or text to filter the list.");

            var entered = UserMustType(prompt, false);
            if (string.IsNullOrEmpty(entered))
                return -1;

            if (int.TryParse(entered, out var enteredNumber))
            {
                if (enteredNumber >= 0 && enteredNumber < visibleIndices.Count)
                    return visibleIndices[enteredNumber];
                Warning($"Number must be between 0 and {visibleIndices.Count - 1}.");
                continue;
            }

            var filteredIndices = new List<int>();
            for (var originalIndex = 0; originalIndex < variants.Count; originalIndex++)
                if (variants[originalIndex].Contains(entered, StringComparison.OrdinalIgnoreCase))
                    filteredIndices.Add(originalIndex);

            if (filteredIndices.Count == 0)
            {
                Information($"No option contains '{entered}'. Showing all options.");
                visibleIndices = Enumerable.Range(0, variants.Count).ToList();
            }
            else if (filteredIndices.Count == 1)
            {
                return filteredIndices[0];
            }
            else
            {
                visibleIndices = filteredIndices;
            }
        }
    }

    public static string SelectFromVariantsString(List<string> variants, string prompt)
    {
        var selected = SelectFromVariants(variants, prompt);
        return variants[selected];
    }
}
