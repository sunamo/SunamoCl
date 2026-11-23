namespace SunamoCl;

/// <summary>
/// Represents the CL progress bar type.
/// </summary>
public class CLProgressBar : IDisposable //: ProgressStateCl
{
    private int writeOnlyDividableByValue;
    private bool isWriteOnlyDividableBy;
    /// <summary>
    /// The write only dividable by.
    /// </summary>
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
    /// <summary>
    /// Done.
    /// </summary>
    /// <returns>The done result.</returns>
    public void Done()
    {
        progressBar.Tick(overallCount, "Finished");

        progressBar.Dispose();
    }

    /// <summary>
    /// Initializes a new instance of the start class.
    /// </summary>
    public void Start(int totalCount, string message, ProgressBarOptions progressBarOptions)
    {
        overallCount = totalCount;
        progressBar = new ProgressBar(totalCount, message, progressBarOptions);
    }

    /// <summary>
    /// Done one.
    /// </summary>
    /// <param name="totalCount">The total count.</param>
    /// <param name="message">The message.</param>
    /// <param name="progressBarOptions">The progress bar options.</param>
    /// <returns>The done one result.</returns>
    public void DoneOne()
    {
        progressBar.Tick();
    }
    /// <summary>
    /// Dispose.
    /// </summary>
    /// <returns>The dispose result.</returns>
    public void Dispose()
    {
        progressBar.Dispose();
    }
}
