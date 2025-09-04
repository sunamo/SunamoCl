namespace RunnerCl;

using SunamoCl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Program
{
    static Dictionary<string, object> DatingActions()
    {
        Dictionary<string, Action> actions = new Dictionary<string, Action>();
        Dictionary<string, Func<Task>> actionsAsync = new Dictionary<string, Func<Task>>();
        actions.Add("None", delegate { });
        actions.Add("Executables of all browsers", WriteTest);
        //actions.Add("Executables of all browsers", WriteTest);
        actions.Add("Test Test1 Test2 (search list)", WriteTest);
        actions.Add("TestTest2Host", WriteTest);
        actions.Add("TestTest", TestTest);

        // Už nebude potřeba. v AskUser mi to získá znovu actions a actionsAsync dle typů ve value
        //AddToAllActions("Dating", actions, actionsAsync);
        return CLActions.MergeActions(actions, actionsAsync);
    }

    static void TestTest()
    {
        Console.WriteLine("🧪 Running TestTest method...");
    }

    static void WriteTest()
    {
        Console.WriteLine("✅ Test executed successfully!");
    }
}