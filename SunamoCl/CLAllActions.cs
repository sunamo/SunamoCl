namespace SunamoCl;

/// <summary>
/// Manages registration and execution of all application actions (both sync and async).
/// </summary>
internal class CLAllActions
{
    private static Dictionary<string, Action> allActions = new();
    private static Dictionary<string, Func<Task>> allActionsAsync = new();

    /// <summary>
    /// Registers action groups from the provided factory function.
    /// </summary>
    /// <param name="addGroupOfActionsFunc">Factory returning groups of actions keyed by name.</param>
    internal static async Task AddToActions(Func<Dictionary<string, Func<Task<Dictionary<string, object>>>>> addGroupOfActionsFunc)
    {
        var groupsOfActions = addGroupOfActionsFunc();
        CL.ShouldPerform = false;
        foreach (var item in groupsOfActions)
        {
            var actionsDictionary = await item.Value();
            foreach (var actionEntry in actionsDictionary)
            {
                var actionObject = actionEntry.Value;
                var objectType = actionObject.GetType();
                if (objectType == TypesDelegates.ActionType)
                {
                    var action = actionObject as Action;
                    if (actionEntry.Key != "None")
                    {
                        ThrowEx.KeyAlreadyExists(allActions, actionEntry.Key, nameof(allActions));
                        allActions.Add(actionEntry.Key, action!);
                    }
                }
                else if (objectType == TypesDelegates.FuncTaskType)
                {
                    var asyncAction = actionObject as Func<Task>;
                    if (actionEntry.Key != "None")
                    {
                        ThrowEx.KeyAlreadyExists(allActionsAsync, actionEntry.Key, nameof(allActionsAsync));
                        allActionsAsync.Add(actionEntry.Key, asyncAction!);
                    }
                }
            }
        }
        CL.ShouldPerform = true;
    }

    /// <summary>
    /// Finds and runs an action matching the given name using search strategy based on input format.
    /// </summary>
    /// <param name="searchText">Search text for the action name.</param>
    internal static async Task<string> RunActionWithName(string searchText)
    {
        string? mode = string.Empty;
        var potentiallyValid = new Dictionary<string, Action>();
        var potentiallyValidAsync = new Dictionary<string, Func<Task>>();
        var containsSpace = searchText.Contains(" ");
        var hasMoreUpperCaseChars = (searchText.Count(character => char.IsUpper(character)) > 1);
        var searchStrategy = containsSpace ? SearchStrategy.AnySpaces : (hasMoreUpperCaseChars ? SearchStrategy.ExactlyName : SearchStrategy.AnySpaces);
        foreach (var item in allActions)
            if (SH.ContainsCl(item.Key, searchText, searchStrategy))
                potentiallyValid.Add(item.Key, item.Value);
        foreach (var item in allActionsAsync)
            if (SH.ContainsCl(item.Key, searchText, searchStrategy))
                potentiallyValidAsync.Add(item.Key, item.Value);
        if (potentiallyValid.Count == 0 && potentiallyValidAsync.Count == 0)
        {
            CL.Information(XlfKeys.NoActionWasFound);
            CL.WriteList(allActions.Keys.ToList(), "Available Actions");
            CL.WriteList(allActionsAsync.Keys.ToList(), "Available Async Actions");
        }
        else
        {
            CL.WriteList(potentiallyValid.Keys.ToList(), "potentiallyValid");
            CL.WriteList(potentiallyValidAsync.Keys.ToList(), "potentiallyValidAsync");
            var mergedActions = AsyncHelper.MergeDictionaries(potentiallyValid, potentiallyValidAsync);
            mode = await CLActions.PerformActionAsync(mergedActions);
        }
        return mode!;
    }
}