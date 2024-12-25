namespace SunamoCl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CLActions
{
    public static Dictionary<string, object> MergeActions(Dictionary<string, Action> actions, Dictionary<string, Func<Task>> actionsAsync)
    {
        Dictionary<string, Action> actions2 = new Dictionary<string, Action>();
        Dictionary<string, Func<Task>> actionsAsync2 = new Dictionary<string, Func<Task>>();

        foreach (var item in actions)
        {
            actions2.Add(item.Key, (dynamic)item.Value);
        }

        foreach (var item in actionsAsync)
        {
            actionsAsync2.Add(item.Key, (dynamic)item.Value);
        }

        return MergeDictionaries(actions2, actionsAsync2);
    }

    private static Dictionary<string, object> MergeDictionaries(Dictionary<string, Action> potentiallyValid,
            Dictionary<string, Func<Task>> potentiallyValidAsync)
    {
        var actionsMerge = new Dictionary<string, object>(potentiallyValid.Count + potentiallyValidAsync.Count);
        if (potentiallyValid != null)
            foreach (var item in potentiallyValid)
                actionsMerge.Add(item.Key, item.Value);
        if (potentiallyValidAsync != null)
            foreach (var item in potentiallyValidAsync)
                actionsMerge.Add(item.Key, item.Value);
        return actionsMerge;
    }

    public static
#if ASYNC
    async Task<string?>
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
    private static
#if ASYNC
    async Task<string?>
#else
string
#endif
    PerformActionAsync(Dictionary<string, object> actions, List<string> listOfActions)
    {
        if (listOfActions.Count > 1)
        {
            return await AskForActionAndRun(actions, listOfActions);
        }
        else
        {
            var actionName = listOfActions.First();
            if (actions.ContainsKey(actionName))
            {
                await CL.InvokeFuncTaskOrAction(actions[actionName]);
                return actionName;
            }
            else
            {
                return await AskForActionAndRun(actions, listOfActions);
            }
        }
    }
    private static async Task<string?> AskForActionAndRun(Dictionary<string, object> actions, List<string> listOfActions)
    {
        var selected = CL.SelectFromVariants(listOfActions, "Select action to proceed:");
        if (selected != -1)
        {
            var ind = listOfActions[selected];
            var eh = actions[ind];
#if ASYNC
            await
#endif
            CL.InvokeFuncTaskOrAction(eh);
            return ind;
        }
        return null;
    }
}