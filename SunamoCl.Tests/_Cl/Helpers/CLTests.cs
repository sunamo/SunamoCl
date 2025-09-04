
using SunamoCl;

public class CLTests
{
    public void ClearCurrentConsoleLineTest()
    {
        CL.WriteLine("abcde");
        CL.ClearCurrentConsoleLine();
        CL.WriteLine("12");
    }

    //[TestMethod]
    public void CmdTableTest()
    {
        var els = "Extra long string";
        els = "C";

        List<List<string>> l = new List<List<string>>();
        l.Add(["A", els]);
        l.Add(["B", els]);

        CL.CmdTable(l);
    }


}
