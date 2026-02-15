namespace SunamoCl;

/// <summary>
/// Progress bar with child progress bars for tracking multiple parallel operations
/// </summary>
public class CLProgressBarWithChilds : IDisposable //: ProgressStateCl
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
    private ProgressBar progressBar;
    private int overallCount = 0;
    /// <summary>
    /// Initializes a new instance with a default main progress bar
    /// </summary>
    public CLProgressBarWithChilds()
    {
        var options = new ProgressBarOptions
        {
            ForegroundColor = ConsoleColor.Yellow,
            BackgroundColor = ConsoleColor.DarkYellow,
            ProgressCharacter = '─'
        };
        progressBar = new ProgressBar(0, "Main", ConsoleColor.Yellow);
    }
    private Dictionary<string, ChildProgressBar> children = new();
    /// <summary>
    /// Marks all child progress bars as done
    /// </summary>
    public void Done()
    {
        //progressBar.Tick(overall, "Finished");
    }
    /// <summary>
    /// Creates a child progress bar under the main progress bar
    /// </summary>
    /// <param name="totalCount">Total number of items for the child progress bar</param>
    /// <param name="message">Message identifying the child operation</param>
    /// <param name="progressBarOptions">Options for customizing the child progress bar appearance</param>
    public void Start(int totalCount, string message, ProgressBarOptions progressBarOptions)
    {
        overallCount = totalCount;
        var child = progressBar.Spawn(totalCount, message, progressBarOptions);
        children.Add(message, child);
    }
    /// <summary>
    /// Increments the progress of the child progress bar identified by the message
    /// </summary>
    /// <param name="message">Message identifying which child progress bar to tick</param>
    public void DoneOne(string message)
    {
        children[message].Tick();
    }
    /// <summary>
    /// Disposes the main progress bar and all its children
    /// </summary>
    public void Dispose()
    {
        progressBar.Dispose();
    }
}