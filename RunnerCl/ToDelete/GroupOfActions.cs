// variables names: ok
namespace RunnerCl.ToDelete;

using SunamoCl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Program
{
    // Property to check if action should be performed
    static bool shouldPerformAction
    {
        get => CL.ShouldPerform;
    }

    static
async Task<Dictionary<string, object>>
Other()
    {
        var actions = OtherActions();

        if (shouldPerformAction)
        {
            await
                        CLActions.PerformActionAsync(actions);
        }

        return actions;
    }
}
