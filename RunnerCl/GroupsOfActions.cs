// Instance variables refactored according to C# conventions
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
        get => CL.perform;
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