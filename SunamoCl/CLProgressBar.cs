namespace SunamoCl;

public class CLProgressBar : IDisposable //: ProgressStateCl
{
    private int writeOnlyDividableByValue;
    private bool isWriteOnlyDividableBy;
    public int WriteOnlyDividableBy
    {
        get => writeOnlyDividableByValue;
        set
        {
            writeOnlyDividableByValue = value;
            isWriteOnlyDividableBy = value != 0;
        }
    }
    private ProgressBar progressBar = null!;
    private int overallCount = 0;
    public void Done()
    {
        progressBar.Tick(overallCount, "Finished");

        progressBar.Dispose();
    }

    public void Start(int totalCount, string message, ProgressBarOptions progressBarOptions)
    {
        overallCount = totalCount;
        progressBar = new ProgressBar(totalCount, message, progressBarOptions);
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