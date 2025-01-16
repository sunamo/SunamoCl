namespace SunamoCl;

using ShellProgressBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CLProgressBar : IDisposable //: ProgressStateCl
{
    private int _writeOnlyDividableBy;
    private bool isWriteOnlyDividableBy;

    public int writeOnlyDividableBy
    {
        get => _writeOnlyDividableBy;
        set
        {
            _writeOnlyDividableBy = value;
            isWriteOnlyDividableBy = value != 0;
        }
    }

    ProgressBar progressBar;
    int overall = 0;

    public void LyricsHelper_WriteProgressBarEnd()
    {
        progressBar.Tick(overall, "Finished");
    }

    public void LyricsHelper_OverallSongs(int obj, string message, ProgressBarOptions progressBarOptions)
    {
        overall = obj;
        progressBar = new ProgressBar(obj, message, progressBarOptions);
    }

    /// <summary>
    ///     A1 is to increment done items after really finished async operation. Can be any.
    /// </summary>
    /// <param name="asyncResult"></param>
    public void LyricsHelper_AnotherSong()
    {
        progressBar.Tick();

    }

    public void Dispose()
    {
        progressBar.Dispose();
    }
}