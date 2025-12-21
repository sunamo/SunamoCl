namespace SunamoCl;

public class CLProgressBar : IDisposable //: ProgressStateCl
{
    private int _writeOnlyDividableByValue;
    private bool _isWriteOnlyDividableBy;
    public int WriteOnlyDividableBy
    {
        get => _writeOnlyDividableByValue;
        set
        {
            _writeOnlyDividableByValue = value;
            _isWriteOnlyDividableBy = value != 0;
        }
    }
    ProgressBar _progressBar;
    int _overallCount = 0;
    public void Done()
    {
        _progressBar.Tick(_overallCount, "Finished");

        _progressBar.Dispose();
    }

    public void Start(int totalCount, string message, ProgressBarOptions progressBarOptions)
    {
        _overallCount = totalCount;
        _progressBar = new ProgressBar(totalCount, message, progressBarOptions);
    }

    /// <summary>
    ///     A1 is to increment done items after really finished async operation. Can be any.
    /// </summary>
    /// <param name="asyncResult"></param>
    public void DoneOne()
    {
        _progressBar.Tick();
    }
    public void Dispose()
    {
        _progressBar.Dispose();
    }
}