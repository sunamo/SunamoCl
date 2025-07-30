namespace RunnerCl.ToDelete;

using Microsoft.Extensions.DependencyInjection;
using SunamoCl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Program
{
    static Dictionary<string, object> OtherActions()
    {
        Dictionary<string, Action> actions = new Dictionary<string, Action>();
        Dictionary<string, Func<Task>> actionsAsync = new Dictionary<string, Func<Task>>();
        actions.Add("None", delegate { });
        //actionsAsync.Add("ReplaceForHardCoded", async () => await replaceAllCharsAndAllStringsWithHardCoded.DoForAll(args));

        return m(actions, actionsAsync);
    }

    public static Dictionary<string, object> m(Dictionary<string, Action> actions, Dictionary<string, Func<Task>> actionsAsync)
    {
        return CLActions.MergeActions(actions, actionsAsync);
    }
}