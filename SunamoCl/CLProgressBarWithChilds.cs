// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
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
    public void Start(int obj, string message, ProgressBarOptions progressBarOptions)
    {
        _overallCount = obj;
        var child = _progressBar.Spawn(obj, message, progressBarOptions);
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