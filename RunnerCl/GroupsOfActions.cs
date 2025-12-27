// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
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
        get => CL.Perform;
    }

    static async Task<Dictionary<string, object>> Dating()
    {
        var actions = DatingActions();

        if (shouldPerformAction)
        {
#if ASYNC
            await
#endif
            CLActions.PerformActionAsync(actions);
        }

        return actions;
    }
}