// Instance variables refactored according to C# conventions
namespace RunnerCl.ToDelete;

using SunamoCl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Program
{
    // EN: Property to check if action should be performed
    // CZ: Property pro kontrolu zda má být akce provedena
    static bool shouldPerformAction
    {
        get => CL.perform;
    }

    static
#if ASYNC
async Task<Dictionary<string, object>>
#else
void
#endif
Other()
    {
        var actions = OtherActions();

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