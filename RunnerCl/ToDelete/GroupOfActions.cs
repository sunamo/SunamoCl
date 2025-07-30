namespace RunnerCl.ToDelete;

using SunamoCl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Program
{
    static
#if ASYNC
async Task<Dictionary<string, object>>
#else
void
#endif
Other()
    {
        var actions = OtherActions();

        if (CL.perform)
        {
#if ASYNC
            await
#endif
                        CLActions.PerformActionAsync(actions);
        }

        return actions;
    }
}