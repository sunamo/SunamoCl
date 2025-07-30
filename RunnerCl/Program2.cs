namespace RunnerCl;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

partial class Program
{

    private static Dictionary<string, Func<Task<Dictionary<string, object>>>> AddGroupOfActions()
    {
        Dictionary<string, Func<Task<Dictionary<string, object>>>> groupsOfActions = new()
        {
            { "Dating", Dating }
        };

        return groupsOfActions;
    }
}