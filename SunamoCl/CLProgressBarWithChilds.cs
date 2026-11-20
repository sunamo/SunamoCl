namespace SunamoCl;

/// <summary>
/// Represents the CL progress bar with childs type.
/// </summary>
public class CLProgressBarWithChilds : IDisposable //: ProgressStateCl
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
    private ProgressBar progressBar;
    private int overallCount = 0;
    /// <summary>
    /// CL progress bar with childs.
    /// </summary>
    /// <returns>The CL progress bar with childs result.</returns>
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
    /// Done.
    /// </summary>
    /// <param name="0">The 0.</param>
    /// <param name=""Main"">The main.</param>
    /// <param name="ConsoleColor.Yellow">The console color yellow.</param>
    /// <returns>The done result.</returns>
    public void Done()
    {
    }
    /// <summary>
    /// Start.
    /// </summary>
    public void Start(int totalCount, string message, ProgressBarOptions progressBarOptions)
    {
        overallCount = totalCount;
        var child = progressBar.Spawn(totalCount, message, progressBarOptions);
        children.Add(message, child);
    }
    /// <summary>
    /// Done one.
    /// </summary>
    /// <param name="totalCount">The total count.</param>
    /// <param name="message">The message.</param>
    /// <param name="progressBarOptions">The progress bar options.</param>
    /// <returns>The done one result.</returns>
    public void DoneOne(string message)
    {
        children[message].Tick();
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
