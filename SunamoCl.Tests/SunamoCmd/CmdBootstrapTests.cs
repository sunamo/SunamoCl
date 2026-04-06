// variables names: ok
using SunamoCl.SunamoCmdArgs_Cmd;

namespace SunamoCl.Tests.SunamoCmd;
internal class CmdBootstrapTests
{
    static ProgramCommon? programCommon = null;
    static string AppName { get; } = "ConsoleApp1";

    /*
    When installing any package, there are always NU1106 conflicting version errors.
    Tried installing all packages one by one without success.
    Therefore xunit cannot be run here yet.
    NuGet packages could have been solved much more easily by linking .cs files everywhere
    instead of copying methods everywhere.
    */

    public static void Main()
    {
        CmdBootstrapTests tester = new CmdBootstrapTests();
        tester.Run2Test().GetAwaiter().GetResult();
    }

    //[Fact]
    public async Task Run2Test()
    {


        programCommon = new ProgramCommon();

        //SunamoInit.InitHelper.FileIO();
        //SunamoInit.InitHelper.Bts();
        //InitHelper.Ca();

        var args = new string[0];


    }

    public static Dictionary<string, Action> AllActions = new Dictionary<string, Action>();
}
