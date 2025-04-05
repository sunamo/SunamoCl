namespace SunamoCl;

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
    public void Done()
    {
        progressBar.Tick(overall, "Finished");

        progressBar.Dispose();
    }

    public void Start(int obj, string message, ProgressBarOptions progressBarOptions)
    {
        overall = obj;
        progressBar = new ProgressBar(obj, message, progressBarOptions);
    }

    /// <summary>
    ///     A1 is to increment done items after really finished async operation. Can be any.
    /// </summary>
    /// <param name="asyncResult"></param>
    public void DoneOne()
    {
        progressBar.Tick();
    }
    public void Dispose()
    {
        progressBar.Dispose();
    }
}