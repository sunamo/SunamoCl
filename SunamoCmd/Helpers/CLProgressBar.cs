namespace SunamoCl.SunamoCmd.Helpers;

public class CLProgressBar : ProgressStateCl, IProgressBar
{
    int _writeOnlyDividableBy = 0;
    bool isWriteOnlyDividableBy = false;
    public bool isNotUt = false;
    public int writeOnlyDividableBy
    {
        get
        {
            return _writeOnlyDividableBy;
        }
        set
        {
            _writeOnlyDividableBy = value;
            isWriteOnlyDividableBy = value != 0;
        }
    }

    public void Init(IPercentCalculatorCl pc, bool isNotUt = false)
    {
        this.pc = pc;
        this.isNotUt = isNotUt;
        if (isNotUt)
        {
            Init(LyricsHelper_OverallSongs, LyricsHelper_AnotherSong, LyricsHelper_WriteProgressBarEnd);
        }

    }

    public void LyricsHelper_WriteProgressBarEnd()
    {
        if (isNotUt)
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

    IPercentCalculatorCl pc = null;

    public void LyricsHelper_OverallSongs(int obj)
    {
        if (isNotUt)
        {
            Set();
            n = 0;
            if (isWriteOnlyDividableBy)
            {
                CL.WriteLine("Starting...");
            }
            else
            {
                pc = pc.Create(obj);
                CL.WriteProgressBar(0);
            }
            Unset();
        }
    }

    /// <summary>
    /// A1 is to increment done items after really finished async operation. Can be any.
    /// </summary>
    /// <param name="asyncResult"></param>
    public void LyricsHelper_AnotherSong(object asyncResult)
    {
        if (isNotUt)
        {
            Set();
            n++;
            LyricsHelper_AnotherSong(n);
            Unset();
        }
    }

    public void LyricsHelper_AnotherSong(int i)
    {
        if (isNotUt)
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
    }

    /// <summary>
    /// private coz should be called only in this class
    /// </summary>
    /// <returns></returns>
    private bool IsDividable()
    {
        if (isNotUt)
        {
            if (isWriteOnlyDividableBy)
            {
                return n % writeOnlyDividableBy == 0;

            }
        }
        {
        }
        return true;
    }

    public void Init(IPercentCalculatorCl pc)
    {
        Init(pc, isNotUt);
    }

    public void LyricsHelper_AnotherSong()
    {
        LyricsHelper_AnotherSong(null);
    }
}
