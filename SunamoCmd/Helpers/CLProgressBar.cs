namespace SunamoCl.SunamoCmd.Helpers;

public class CLProgressBar : ProgressStateCl//, IProgressBar
{
    private int _writeOnlyDividableBy;
    // proč bych v ut neměl vypisovat? zase tak často to pod testy neběží, rozhodně méně často než v reálu
    // navíc i v ut potřebuji vidět jak se to chová a dělám ten kód jen složitější. 
    //public bool isNotUt;
    private bool isWriteOnlyDividableBy;

    private PercentCalculator pc;

    public int writeOnlyDividableBy
    {
        get => _writeOnlyDividableBy;
        set
        {
            _writeOnlyDividableBy = value;
            isWriteOnlyDividableBy = value != 0;
        }
    }

    public void Init()
    {
        Init(LyricsHelper_OverallSongs, LyricsHelper_AnotherSong, LyricsHelper_WriteProgressBarEnd);
    }

    public void LyricsHelper_WriteProgressBarEnd()
    {

        Set();
        if (isWriteOnlyDividableBy)
        {
            CL.ClearCurrentConsoleLine();
            CL.WriteLine(n + " Finished");
        }
        else
        {
            CL.WriteProgressBarEnd();
        }

        Unset();

    }

    public void LyricsHelper_OverallSongs(int obj)
    {
        Set();
        n = 0;
        if (isWriteOnlyDividableBy)
        {
            CL.WriteLine("Starting...");
        }
        else
        {
            pc = new PercentCalculator(obj);
            CL.WriteProgressBar(0);
        }

        Unset();

    }

    /// <summary>
    ///     A1 is to increment done items after really finished async operation. Can be any.
    /// </summary>
    /// <param name="asyncResult"></param>
    public void LyricsHelper_AnotherSong(object asyncResult)
    {
        Set();
        n++;
        LyricsHelper_AnotherSong(n);
        Unset();

    }

    public void LyricsHelper_AnotherSong(int i)
    {
        Set();
        if (isWriteOnlyDividableBy)
        {
            if (i % writeOnlyDividableBy == 0)
            {
                CL.ClearCurrentConsoleLine();
                //TypedSunamoLogger.Instance.Information(i.ToString());
                Console.WriteLine(i.ToString());
            }
        }
        else
        {
            pc.AddOnePercent();
            CL.WriteProgressBar((int)pc.last, new WriteProgressBarArgs(true, i, pc._overallSum));
        }

        Unset();

    }



    public void LyricsHelper_AnotherSong()
    {
        LyricsHelper_AnotherSong(null);
    }

    private static void Unset()
    {
        CL.inClpb = false;
        CL.src = ClSources.z;
    }

    private static void Set()
    {
        CL.inClpb = true;
        CL.src = ClSources.a;
    }

    /// <summary>
    ///     private coz should be called only in this class
    /// </summary>
    /// <returns></returns>
    private bool IsDividable()
    {
        if (isWriteOnlyDividableBy)
            return n % writeOnlyDividableBy == 0;
        else
            return true;
    }
}