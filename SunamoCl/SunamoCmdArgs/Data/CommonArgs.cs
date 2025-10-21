namespace SunamoCl.SunamoCmdArgs.Data;

public class CommonArgs
{
    ///// <summary>
    ///// Bool se zadává bez jedničky nebo nuly, ta na něj nemá žádný vliv
    ///// Proto nastavení výchozí hodnoty na true nemá žádný smysl, protože jak při zadání true i false bude vždy true
    ///// </summary>
    //[Option("NoTestForAlreadyRunning", ResourceType = typeof(bool))]
    ///// false je zde jediná povolená hodnota
    //public bool NoTestForAlreadyRunning { get; set; } = false;

    [Option("Mode", ResourceType = typeof(string))]
    public string Mode { get; set; } = "";

    /// <summary>
    /// if I want to run the application in a folder other than the current one. In the default state, the only 2 attributes that do not need a switch are only the mode and args to the mode in each application
    /// 
    /// Krom toho ale musí zůstat zachováno i složka na druhém místě hned za módem! Využívá se to např. ve VS při rychlých akcích $(ProjectDir)
    /// </summary>
    [Option("RunInFolder", ResourceType = typeof(string))]
    public string RunInFolder { get; set; }
}