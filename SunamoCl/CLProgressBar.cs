namespace SunamoCl;

/// <summary>
/// Wraps ShellProgressBar to provide a simple progress bar for console applications
/// </summary>
public class CLProgressBar : IDisposable //: ProgressStateCl
{
    private int writeOnlyDividableByValue;
    private bool isWriteOnlyDividableBy;
    /// <summary>
    /// Gets or sets the divisor value for writing progress updates only when the count is divisible by this value
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
    /// Completes the progress bar by ticking to the total count and disposing it
    /// </summary>
    public void Done()
    {
        progressBar.Tick(overallCount, "Finished");

        progressBar.Dispose();
    }

    /// <summary>
    /// Starts the progress bar with the specified total count, message and options
    /// </summary>
    /// <param name="totalCount">Total number of items to track</param>
    /// <param name="message">Message to display alongside the progress bar</param>
    /// <param name="progressBarOptions">Options for customizing the progress bar appearance</param>
    public void Start(int totalCount, string message, ProgressBarOptions progressBarOptions)
    {
        overallCount = totalCount;
        progressBar = new ProgressBar(totalCount, message, progressBarOptions);
    }

    /// <summary>
    ///     A1 is to increment done items after really finished async operation. Can be any.
    /// </summary>
    public void DoneOne()
    {
        progressBar.Tick();
    }
    /// <summary>
    /// Disposes the underlying progress bar resources
    /// </summary>
    public void Dispose()
    {
        progressBar.Dispose();
    }
}