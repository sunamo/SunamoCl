namespace SunamoCl;

public partial class CL
{
    /// <summary>
    /// Press enter after insert data to clipboard.
    /// </summary>
    /// <param name="what">The what.</param>
    public static async Task PressEnterAfterInsertDataToClipboard(string what)
    {
        if (CmdApp.ShouldLoadFromClipboard)
        {
            await AppealEnter( "Insert " + what + " to clipboard");
        }
    }

    /// <summary>
    /// Clear.
    /// </summary>
    public static void Clear()
    {
        Console.Clear();
    }

    /// <summary>
    /// Cmd table.
    /// </summary>
    /// <param name="rows">The rows.</param>
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
    /// Pair.
    /// </summary>
    /// <param name="label">The label.</param>
    /// <param name="value">The value.</param>
    public static void Pair(string label, string value)
    {
        Console.WriteLine($"📊 {label}: {value}");
    }

    /// <summary>
    /// Press any key to continue.
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
    /// Do you want to continue.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>The do you want to continue result.</returns>
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
    /// Appeal enter.
    /// </summary>
    /// <param name="appeal">The appeal.</param>
    public static async Task AppealEnter( string appeal)
    {
        Appeal(appeal + ". " + FromKey("ThenPressEnter") + ".");
        await ClNotify.FlashConsoleTitle();
    }

    // Lets user select an action and runs it. Only needed when the application has its own Mode.cs.
    // Otherwise, autorun at release is handled by RunWithRunArgs.
    // EventHandler is correct here because EventHandler never has a Task return type.
    /// <summary>
    /// Perform action.
    /// </summary>
    /// <param name="actions">The actions.</param>
    /// <param name="sender">The sender.</param>
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
    /// Perform action after run calling.
    /// </summary>
    /// <param name="mode">The mode.</param>
    /// <param name="addGroupOfActionsFunc">The add group of actions func.</param>
    /// <param name="isPrintAllActions">The is print all actions.</param>
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
    /// <summary>
    /// User must type number.
    /// </summary>
    /// <param name="prompt">The prompt.</param>
    /// <param name="max">The max.</param>
    /// <param name="min">The min.</param>
    /// <returns>The user must type number result.</returns>
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
    /// <summary>
    /// User must type number.
    /// </summary>
    /// <param name="max">The max.</param>
    /// <returns>The user must type number result.</returns>
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

    /// <summary>
    /// No data.
    /// </summary>
    public static void NoData()
    {
        Appeal(Messages.NoData);
    }

    /// <summary>
    /// User must type yes no.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The user must type yes no result.</returns>
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
    /// Select from variants.
    /// </summary>
    /// <param name="variants">The variants.</param>
    /// <param name="prompt">The prompt.</param>
    /// <returns>The select from variants result.</returns>
    public static int SelectFromVariants(List<string> variants, string prompt)
    {
        // The displayed list can be narrowed by typing text (filter). The number then selects from the narrowed list,
        // so the mapping displayed index -> original index into variants is kept.
        var visibleIndices = Enumerable.Range(0, variants.Count).ToList();
        while (true)
        {
            const string title = "📋 Select an option:";
            var rowTexts = visibleIndices
                .Select((originalIndex, displayIndex) => $"[{displayIndex + 1:D2}] {variants[originalIndex]}")
                .ToList();
            var contentWidth = rowTexts.Count == 0 ? title.Length : Math.Max(title.Length, rowTexts.Max(r => r.Length));
            var innerWidth = contentWidth + 2;

            Console.WriteLine();
            Console.WriteLine("╔" + new string('═', innerWidth) + "╗");
            Console.WriteLine("║ " + title.PadRight(contentWidth) + " ║");
            Console.WriteLine("╠" + new string('═', innerWidth) + "╣");
            foreach (var rowText in rowTexts)
                Console.WriteLine("║ " + rowText.PadRight(contentWidth) + " ║");
            Console.WriteLine("╚" + new string('═', innerWidth) + "╝");
            Console.WriteLine("Type a number to select, or text to filter the list.");

            var entered = UserMustType(prompt, false);
            if (string.IsNullOrEmpty(entered))
                return -1;

            if (int.TryParse(entered, out var enteredNumber))
            {
                var displayIndex = enteredNumber - 1;
                if (displayIndex >= 0 && displayIndex < visibleIndices.Count)
                    return visibleIndices[displayIndex];
                Warning($"Number must be between 1 and {visibleIndices.Count}.");
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

    /// <summary>
    /// Select from variants string.
    /// </summary>
    /// <param name="variants">The variants.</param>
    /// <param name="prompt">The prompt.</param>
    /// <returns>The select from variants string result.</returns>
    public static string SelectFromVariantsString(List<string> variants, string prompt)
    {
        var selected = SelectFromVariants(variants, prompt);
        return variants[selected];
    }
}
