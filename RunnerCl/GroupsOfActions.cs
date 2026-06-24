// variables names: ok
namespace RunnerCl;

using SunamoCl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Program
{
    static bool shouldPerformAction
    {
        get => CL.ShouldPerform;
    }

    static async Task<Dictionary<string, object>> Dating()
    {
        var actions = DatingActions();

        if (shouldPerformAction)
        {
            await
            CLActions.PerformActionAsync(actions);
        }

        return actions;
    }
}
