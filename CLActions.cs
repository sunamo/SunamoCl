namespace SunamoCl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CLActions
{
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