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
}
