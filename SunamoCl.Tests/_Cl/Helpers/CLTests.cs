// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
using SunamoCl;

/// <summary>
/// Tests for console line operations and table display functionality
/// </summary>
public class CLTests
{
    /// <summary>
    /// Tests clearing the current console line and writing new content
    /// </summary>
    public void ClearCurrentConsoleLineTest()
    {
        CL.WriteLine("abcde");
        CL.ClearCurrentConsoleLine();
        CL.WriteLine("12");
    }

    //[TestMethod]
    /// <summary>
    /// Tests console table display with multiple rows of data
    /// </summary>
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
