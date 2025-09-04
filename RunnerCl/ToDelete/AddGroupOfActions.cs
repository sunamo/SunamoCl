// Instance variables refactored according to C# conventions
namespace RunnerCl.ToDelete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Program
{
    public static Dictionary<string, Func<Task<Dictionary<string, object>>>> AddGroupOfActions()
    {
        Dictionary<string, Func<Task<Dictionary<string, object>>>> actionGroups = new()
        {
            { "Other", Other }, // 1
        };

        return actionGroups;
    }
}