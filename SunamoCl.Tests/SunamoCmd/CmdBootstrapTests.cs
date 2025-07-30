using SunamoCl.SunamoCmdArgs_Cmd;

namespace SunamoCl.Tests.SunamoCmd;
internal class CmdBootstrapTests
{
    static ProgramCommon p = null;
    static readonly string appName = "ConsoleApp1";

    /*
Při instalaci jakéhokoliv balíčku mám vždy tyto chyby:
    NU1106: Unable to satisfy conflicting requests for 'SunamoValues': SunamoValues (>= 24.2.6.3) (via project/SunamoDebugIO 24.2.6.2), SunamoValues (>= 24.2.6.3) (via project/SunamoClipboard 24.2.6.2), SunamoValues (>= 24.2.6.3) (via project/SunamoExceptions 24.2.6.3), SunamoValues (>= 24.2.6.2) (via package/SunamoExceptions 24.2.6.3), SunamoValues (>= 24.2.6.2) (via package/SunamoExceptions 24.2.6.3) Framework: (.NETCoreApp,Version=v8.0)
NU1106: Unable to satisfy conflicting requests for 'SunamoInterfaces': SunamoInterfaces (>= 24.2.6.3) (via project/SunamoWinStd 24.2.6.2), SunamoInterfaces (>= 24.2.6.3) (via project/SunamoClipboard 24.2.6.2), SunamoInterfaces (>= 24.2.7.1) (via project/SunamoCl 24.2.7.3), SunamoInterfaces (>= 24.2.6.2) (via package/SunamoValues 24.2.6.3), SunamoInterfaces (>= 24.2.6.2) (via package/SunamoValues 24.2.6.3) Framework: (.NETCoreApp,Version=v8.0)
NU1106: Unable to satisfy conflicting requests for 'SunamoDelegates': SunamoDelegates (>= 24.2.6.2) (via project/SunamoExceptions 24.2.6.3), SunamoDelegates (>= 24.2.6.1) (via package/SunamoExceptions 24.2.6.3), SunamoDelegates (>= 24.2.6.1) (via package/SunamoInterfaces 24.2.6.3), SunamoDelegates (>= 24.2.6.1) (via package/SunamoExceptions 24.2.6.3), SunamoDelegates (>= 24.2.6.1) (via package/SunamoInterfaces 24.2.6.3), SunamoDelegates (>= 24.2.6.1) (via package/SunamoExceptions 24.2.6.3), SunamoDelegates (>= 24.2.6.2) (via package/SunamoInterfaces 24.2.7.1), SunamoDelegates (>= 24.2.5.1) (via package/SunamoInterfaces 24.2.6.2) Framework: (.NETCoreApp,Version=v8.0)
NU1106: Unable to satisfy conflicting requests for 'SunamoXlfKeys': SunamoXlfKeys (>= 24.2.6.2) (via project/SunamoExceptions 24.2.6.3), SunamoXlfKeys (>= 24.2.6.2) (via project/SunamoCl 24.2.7.3), SunamoXlfKeys (>= 24.2.6.1) (via package/SunamoExceptions 24.2.6.3), SunamoXlfKeys (>= 24.2.6.1) (via package/SunamoExceptions 24.2.6.3) Framework: (.NETCoreApp,Version=v8.0)
NU1106: Unable to satisfy conflicting requests for 'SunamoArgs': SunamoArgs (>= 24.2.7.1) (via project/SunamoCl 24.2.7.3), SunamoArgs (>= 24.2.6.3) (via package/SunamoInterfaces 24.2.6.3), SunamoArgs (>= 24.2.6.3) (via package/SunamoInterfaces 24.2.6.3), SunamoArgs (>= 24.2.5.2) (via package/SunamoInterfaces 24.2.6.2), SunamoArgs (>= 24.2.5.2) (via package/SunamoInterfaces 24.2.6.2) Framework: (.NETCoreApp,Version=v8.0)
NU1106: Unable to satisfy conflicting requests for 'HtmlAgilityPack': HtmlAgilityPack (>= 1.11.58) (via package/SunamoInterfaces 24.2.6.3), HtmlAgilityPack (>= 1.11.58) (via package/SunamoInterfaces 24.2.6.3), HtmlAgilityPack (>= 1.11.58) (via package/SunamoInterfaces 24.2.7.1), HtmlAgilityPack (>= 1.11.58) (via package/SunamoInterfaces 24.2.6.2), HtmlAgilityPack (>= 1.11.58) (via package/SunamoInterfaces 24.2.6.2) Framework: (.NETCoreApp,Version=v8.0)
NU1106: Unable to satisfy conflicting requests for 'Newtonsoft.Json': Newtonsoft.Json (>= 13.0.3) (via package/SunamoInterfaces 24.2.6.3), Newtonsoft.Json (>= 13.0.3) (via package/SunamoInterfaces 24.2.6.3), Newtonsoft.Json (>= 13.0.3) (via package/SunamoData 24.2.6.4), Newtonsoft.Json (>= 13.0.3) (via package/SunamoInterfaces 24.2.7.1), Newtonsoft.Json (>= 13.0.3) (via package/SunamoInterfaces 24.2.6.2), Newtonsoft.Json (>= 13.0.3) (via package/SunamoInterfaces 24.2.6.2) Framework: (.NETCoreApp,Version=v8.0)
NU1106: Unable to satisfy conflicting requests for 'SunamoEnums': SunamoEnums (>= 24.2.6.1) (via package/SunamoInterfaces 24.2.6.3), SunamoEnums (>= 24.2.6.1) (via package/SunamoInterfaces 24.2.6.3), SunamoEnums (>= 24.2.6.2) (via package/SunamoArgs 24.2.7.1), SunamoEnums (>= 24.2.6.2) (via package/SunamoData 24.2.6.4), SunamoEnums (>= 24.2.6.2) (via package/SunamoInterfaces 24.2.7.1), SunamoEnums (>= 24.2.5.1) (via package/SunamoInterfaces 24.2.6.2), SunamoEnums (>= 24.2.5.1) (via package/SunamoInterfaces 24.2.6.2) Framework: (.NETCoreApp,Version=v8.0)
Package restore failed. Rolling back package changes for 'SunamoCl.Tests'.
Time Elapsed: 00:00:00.2062107
========== Finished ==========

zkusil jsem nainstlaovat všechny po jednom ale bez výsledku
    proto zde nemůžu pouštět xunit. zatím
    nugety jsem mohl vyřešit mnohem jednodušeji než všude kopírovat metody. všude přilinkovat cs soubory protože bez toho to asi nepůjde. 

     */

    public static void Main()
    {
        CmdBootstrapTests t = new CmdBootstrapTests();
        t.Run2Test().GetAwaiter().GetResult();
    }

    //[Fact]
    public async Task Run2Test()
    {


        p = new ProgramCommon();

        //SunamoInit.InitHelper.FileIO();
        //SunamoInit.InitHelper.Bts();
        //InitHelper.Ca();

        var args = new string[0];


    }

    public static Dictionary<string, Action> allActions = new Dictionary<string, Action>();
}
