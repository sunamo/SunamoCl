// variables names: ok
namespace SunamoCl;

public class CLProgressBarWithChilds : IDisposable //: ProgressStateCl
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
    private ProgressBar progressBar;
    private int overallCount = 0;
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
    public void Done()
    {
        //progressBar.Tick(overall, "Finished");
    }
    public void Start(int totalCount, string message, ProgressBarOptions progressBarOptions)
    {
        overallCount = totalCount;
        var child = progressBar.Spawn(totalCount, message, progressBarOptions);
        children.Add(message, child);
    }
    public void DoneOne(string message)
    {
        children[message].Tick();
    }
    public void Dispose()
    {
        progressBar.Dispose();
    }
}