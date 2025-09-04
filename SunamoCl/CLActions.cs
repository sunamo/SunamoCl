// Instance variables refactored according to C# conventions
namespace SunamoCl;

public class CLActions
{
    public static Dictionary<string, object> MergeActions(Dictionary<string, Action> actions, Dictionary<string, Func<Task>> actionsAsync)
    {
        Dictionary<string, Action> synchronousActions = new Dictionary<string, Action>();
        Dictionary<string, Func<Task>> asynchronousActions = new Dictionary<string, Func<Task>>();
        foreach (var item in actions)
        {
            synchronousActions.Add(item.Key, (dynamic)item.Value);
        }
        foreach (var item in actionsAsync)
        {
            asynchronousActions.Add(item.Key, (dynamic)item.Value);
        }
        return MergeDictionaries(synchronousActions, asynchronousActions);
    }
    private static Dictionary<string, object> MergeDictionaries(Dictionary<string, Action> potentiallyValid,
            Dictionary<string, Func<Task>> potentiallyValidAsync)
    {
        var mergedActions = new Dictionary<string, object>(potentiallyValid.Count + potentiallyValidAsync.Count);
        if (potentiallyValid != null)
            foreach (var item in potentiallyValid)
                mergedActions.Add(item.Key, item.Value);
        if (potentiallyValidAsync != null)
            foreach (var item in potentiallyValidAsync)
                mergedActions.Add(item.Key, item.Value);
        return mergedActions;
    }
    public static
#if ASYNC
    async Task<string?>
#else
string
#endif
    PerformActionAsync(Dictionary<string, object> actions)
    {
        var actionsList = actions.Keys.ToList();
        return
#if ASYNC
        await
#endif
        PerformActionAsync(actions, actionsList);
    }
    private static
#if ASYNC
    async Task<string?>
#else
string
#endif
    PerformActionAsync(Dictionary<string, object> actions, List<string> actionsList)
    {
        if (actionsList.Count > 1)
        {
            return await AskForActionAndRun(actions, actionsList);
        }
        else
        {
            var actionName = actionsList.First();
            if (actions.ContainsKey(actionName))
            {
                await CL.InvokeFuncTaskOrAction(actions[actionName]);
                return actionName;
            }
            else
            {
                return await AskForActionAndRun(actions, actionsList);
            }
        }
    }
    private static async Task<string?> AskForActionAndRun(Dictionary<string, object> actions, List<string> actionsList)
    {
        var selectedIndex = CL.SelectFromVariants(actionsList, "Select action to proceed:");
        if (selectedIndex != -1)
        {
            var selectedAction = actionsList[selectedIndex];
            var eventHandler = actions[selectedAction];
#if ASYNC
            await
#endif
            CL.InvokeFuncTaskOrAction(eventHandler);
            return selectedAction;
        }
        return null;
    }
}