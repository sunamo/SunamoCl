namespace SunamoCl;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            WriteProgressBarEnd();
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
            WriteProgressBar(0);
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
            WriteProgressBar((int)pc.last, new WriteProgressBarArgs(true, i, pc._overallSum));
        }

        Unset();

    }



    public void LyricsHelper_AnotherSong()
    {
        LyricsHelper_AnotherSong(null);
    }

    private void Unset()
    {
        CL.inClpb = false;
        CL.src = ClSources.z;
    }

    private void Set()
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

    public StringBuilder sbToClear = new();
    public StringBuilder sbToWrite = new();

    #region Progress bar

    private const string _back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";
    private const char _block = '■';
    private const string _twirl = "-\\|/";
    private int _backL;
    private object sbToClearLock = new object();

    public void WriteProgressBar(double percent, WriteProgressBarArgs a = null)
    {
        WriteProgressBar((int)percent, a);
    }

    public void WriteProgressBar(int percent, WriteProgressBarArgs a = null)
    {
        string s = "";
        if (a == null) a = WriteProgressBarArgs.Default;
        if (a.update)
        {
            lock (sbToClearLock)
            {
                sbToClear.Clear();
                sbToClear.Append(_back);
                sbToClear.Append(string.Empty.PadRight(s.Length - _backL, '\b'));
                var ts = sbToClear.ToString();
                CL.Write(ts);
            }
        }
        CL.Write("[");
        var p = (int)(percent / 10f + .5f);
        for (var i = 0; i < 10; ++i)
            if (i >= p)
                CL.Write(' ');
            else
                CL.Write(_block);
        if (a.writePieces)
            s = "] {0,3:##0}%" + $" {a.actual} / {a.overall}";
        else
            s = "] {0,3:##0}%";
        var fr = string.Format(s, percent);
        CL.Write(fr);
    }

    public void WriteProgressBarEnd()
    {
        WriteProgressBar(100, new WriteProgressBarArgs(true));
        CL.WriteLine();
    }

    public void WriteProgressBarInit()
    {
        _backL = _back.Length;
    }

    #endregion Progress bar
}