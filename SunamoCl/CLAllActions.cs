namespace SunamoCl;

internal class CLAllActions
{
    private static Dictionary<string, Action> allActions = new();
    private static Dictionary<string, Func<Task>> allActionsAsync = new();
    internal static async Task AddToActions(Func<Dictionary<string, Func<Task<Dictionary<string, object>>>>> addGroupOfActionsFunc)
    {
        var groupsOfActionsFromProgramCommon = addGroupOfActionsFunc();
        CL.Perform = false;
        foreach (var item in groupsOfActionsFromProgramCommon)
        {
            var itemValue = item.Value();
            var actions = await itemValue;
            foreach (var actionEntry in actions)
            {
                var actionObject = actionEntry.Value;
                var objectType = actionObject.GetType();
                if (objectType == TypesDelegates.TAction)
                {
                    var action = actionObject as Action;
                    if (actionEntry.Key != "None")
                    {
                        ThrowEx.KeyAlreadyExists(allActions, actionEntry.Key, nameof(allActions));
                        allActions.Add(actionEntry.Key, action!);
                    }
                }
                else if (objectType == TypesDelegates.TFuncTask)
                {
                    var taskVoid = actionObject as Func<Task>;
                    if (actionEntry.Key != "None")
                    {
                        ThrowEx.KeyAlreadyExists(allActionsAsync, actionEntry.Key, nameof(allActionsAsync));
                        allActionsAsync.Add(actionEntry.Key, taskVoid!);
                    }
                }
            }
        }
        CL.Perform = true;
    }
    internal static async Task<string> RunActionWithName(string whatUserNeed)
    {
        string? mode = string.Empty;
        var potentiallyValid = new Dictionary<string, Action>();
        var potentiallyValidAsync = new Dictionary<string, Func<Task>>();
        // Když jsem chtěl jen Test, nenašlo mi to nic, protože upřesňující podmínka. Takže tam musí být více upper case 
        // první kontrola na mezeru
        // pokud nebude mezera a bude bude více upper case znaků => ExactlyName
        var containsSpace = whatUserNeed.Contains(" ");
        var hasMoreUpperCaseChars = (whatUserNeed.Count(character => char.IsUpper(character)) > 1);
        var searchStrategy = containsSpace ? SearchStrategy.AnySpaces : (hasMoreUpperCaseChars ? SearchStrategy.ExactlyName : SearchStrategy.AnySpaces);
        foreach (var item in allActions)
            if (SH.ContainsCl(item.Key, whatUserNeed, searchStrategy))
                potentiallyValid.Add(item.Key, item.Value);
        foreach (var item in allActionsAsync)
            if (SH.ContainsCl(item.Key, whatUserNeed, searchStrategy))
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