// variables names: ok
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
    private static Dictionary<string, object> MergeDictionaries(Dictionary<string, Action> syncActions,
            Dictionary<string, Func<Task>> asyncActions)
    {
        var mergedActions = new Dictionary<string, object>(syncActions.Count + asyncActions.Count);
        if (syncActions != null)
            foreach (var item in syncActions)
                mergedActions.Add(item.Key, item.Value);
        if (asyncActions != null)
            foreach (var item in asyncActions)
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
        var actionNames = actions.Keys.ToList();
        return
#if ASYNC
        await
#endif
        PerformActionAsync(actions, actionNames);
    }
    private static
#if ASYNC
    async Task<string?>
#else
string
#endif
    PerformActionAsync(Dictionary<string, object> actions, List<string> actionNames)
    {
        if (actionNames.Count > 1)
        {
            return await AskForActionAndRun(actions, actionNames);
        }
        else
        {
            var actionName = actionNames.First();
            if (actions.ContainsKey(actionName))
            {
                await CL.InvokeFuncTaskOrAction(actions[actionName]);
                return actionName;
            }
            else
            {
                return await AskForActionAndRun(actions, actionNames);
            }
        }
    }
    private static async Task<string?> AskForActionAndRun(Dictionary<string, object> actions, List<string> actionNames)
    {
        var selectedIndex = CL.SelectFromVariants(actionNames, "Select action to proceed:");
        if (selectedIndex != -1)
        {
            var selectedAction = actionNames[selectedIndex];
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