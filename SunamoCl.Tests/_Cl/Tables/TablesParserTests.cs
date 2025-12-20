// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

using SunamoCl;
using SunamoCl._sunamo;
using SunamoCl.SunamoCmd.Tables;
using SunamoTest;

namespace cmd.Tests.Tables;

class TablesParserTests
{
    public void ToStringTable()
    {
        var extraLongString = "Extra really long string";

        List<string> firstRow = ["A", extraLongString, "", ""];
        List<string> secondRow = ["B", "", extraLongString, ""];

        //List<List<string>> ls = new List<List<string>>();
        //ls.Add(firstRow);
        //ls.Add(secondRow);

        var headers = TestData.listABCD;

        var list = new List<string>();
        list.AddRange(firstRow);
        list.AddRange(secondRow);

        var tableData = CA.OneDimensionArrayToTwoDirection(list.ToArray(), 4);

        //string[,] t = new string[]

        //cl.CmdTableTest();
        //CmdTableTests.CmdTable2Tests();

        var text = TableParser.ToStringTable(tableData);
        CL.WriteLine(text);
    }
}

