namespace SunamoCl;

public class CLProgressBarWithChilds : IDisposable //: ProgressStateCl
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
    private ProgressBar _progressBar;
    private int _overallCount = 0;
    public CLProgressBarWithChilds()
    {
        var options = new ProgressBarOptions
        {
            ForegroundColor = ConsoleColor.Yellow,
            BackgroundColor = ConsoleColor.DarkYellow,
            ProgressCharacter = '─'
        };
        _progressBar = new ProgressBar(0, "Main", ConsoleColor.Yellow);
    }
    private Dictionary<string, ChildProgressBar> _children = new();
    public void Done()
    {
        //progressBar.Tick(overall, "Finished");
    }
    public void Start(int totalCount, string message, ProgressBarOptions progressBarOptions)
    {
        _overallCount = totalCount;
        var child = _progressBar.Spawn(totalCount, message, progressBarOptions);
        _children.Add(message, child);
    }
    public void DoneOne(string message)
    {
        _children[message].Tick();
    }
    public void Dispose()
    {
        _progressBar.Dispose();
    }
}