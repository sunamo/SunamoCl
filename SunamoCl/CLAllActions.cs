namespace SunamoCl;

internal class CLAllActions
{
    private static Dictionary<string, Action> allActions = new();
    private static Dictionary<string, Func<Task>> allActionsAsync = new();
    internal static async Task AddToActions(Func<Dictionary<string, Func<Task<Dictionary<string, object>>>>> AddGroupOfActions)
    {
        var groupsOfActionsFromProgramCommon = AddGroupOfActions();
        CL.perform = false;
        foreach (var item in groupsOfActionsFromProgramCommon)
        {
            var itemValue = item.Value();
            var text = await itemValue;
            foreach (var item2 in text)
            {
                var o = item2.Value;
                var temp = o.GetType();
                if (temp == TypesDelegates.tAction)
                {
                    var oAction = o as Action;
                    if (item2.Key != "None")
                    {
                        ThrowEx.KeyAlreadyExists(allActions, item2.Key, nameof(allActions));
                        allActions.Add(item2.Key, oAction);
                    }
                }
                else if (temp == TypesDelegates.tFuncTask)
                {
                    var taskVoid = o as Func<Task>;
                    if (item2.Key != "None")
                    {
                        ThrowEx.KeyAlreadyExists(allActionsAsync, item2.Key, nameof(allActionsAsync));
                        allActionsAsync.Add(item2.Key, taskVoid);
                    }
                }
            }
        }
        CL.perform = true;
    }
    internal static async Task<string> RunActionWithName(string whatUserNeed)
    {
        string mode = string.Empty;
        var potentiallyValid = new Dictionary<string, Action>();
        var potentiallyValidAsync = new Dictionary<string, Func<Task>>();
        // Když jsem chtěl jen Test, nenašlo mi to nic, protože upřesňující podmínka. Takže tam musí být více upper case 
        // první kontrola na mezeru
        // pokud nebude mezera a bude bude více upper case znaků => ExactlyName
        var containsSpace = whatUserNeed.Contains(" ");
        var moreUpperCaseChars = (whatUserNeed.Count(d => char.IsUpper(d)) > 1);
        var searchStrategy = containsSpace ? SearchStrategy.AnySpaces : (moreUpperCaseChars ? SearchStrategy.ExactlyName : SearchStrategy.AnySpaces);
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
            var actionsMerge = AsyncHelper.MergeDictionaries(potentiallyValid, potentiallyValidAsync);
            mode = await CLActions.PerformActionAsync(actionsMerge);
        }
        return mode;
    }
}