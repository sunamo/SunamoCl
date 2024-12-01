namespace SunamoCl.SunamoCmdArgs_Cmd;

public partial class ProgramCommon
{
    /// <summary>
    ///     must be IEnumerable
    /// </summary>
    /// <param name="e"></param>
    private void ProcessArgsErrors(IEnumerable<Error> e)
    {
    }

    public Tuple<T, Mode>? ProcessArgs<T, Mode>(string[] args, Mode ifParseFail, bool writeError = true)
        where T : CommonArgs
        where Mode : struct
    {
        var a = Activator.CreateInstance<T>();

        if (a == null)
        {
            ThrowEx.Custom($"{nameof(a)} is null");
            return null;
        }

        string arg = null;

        #region Parse and executing node if was set

        CL.WriteLine("args.Length: " + args.Length);

        if (args.Length != 0)
        {
            // 2) parsování atributů
            CmdArgs.ProcessArgsErrors = ProcessArgsErrors;
            a = CmdArgs.SaveArgsWorker<T>(args);

            //Arg1 etc. is in if below
            //CL.WriteLine(a.Path);
            //CL.WriteLine(a.Mode);
            arg = a.Mode;
        }

        //#if !DEBUG
        //CL.WriteLine("Arg: " + SH.NullToStringOrDefault(arg));
        if (arg != null)
        {
            var mode = Enum.Parse<Mode>(arg);
            CL.WriteLine("arg is NOT null");
            if (EqualityComparer<Mode>.Default.Equals(mode, ifParseFail))
            {
                if (writeError)
                {
                    //ThisApp.Error("Parse mode failed, probably " + arg + " is not in Mode defined");
                }

                return new Tuple<T, Mode>(a, ifParseFail);
            }
        }
        else
        {
            CL.WriteLine("arg is null");
        }

        //#endif

        #endregion

        return new Tuple<T, Mode>(a, Enum.Parse<Mode>(a.Mode));
    }

    #region Stejné property se mi vkládají do RunArgs

    //public Dictionary<string, Action> allActions = new Dictionary<string, Action>();
    //public Dictionary<string, Func<Task>> allActionsAsync = new Dictionary<string, Func<Task>>();
    //public Func<Dictionary<string, Action>> AddGroupOfActions;
    //// Způsobuje mi to problémy tím že se pokouší vložit klíč který již existuje (např. Dating)
    //// Zdá se že k ničemu to nepotřebuji, proto veškerou práci s tím všude zakomentuji
    //// tak ne, potřebuji ho i nadále abych si do něj uložil názvy všech akcí
    //// a přes fulltext vyberu hledané
    //public Dictionary<string, object> groupsOfActions = new Dictionary<string, object>(); 

    #endregion
}