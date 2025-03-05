namespace SunamoCl;

public class CLProgressBarWithChilds : IDisposable //: ProgressStateCl
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
    Dictionary<string, ChildProgressBar> childs = new();
    public void LyricsHelper_WriteProgressBarEnd()
    {
        //progressBar.Tick(overall, "Finished");
    }
    public void LyricsHelper_OverallSongs(int obj, string message, ProgressBarOptions progressBarOptions)
    {
        overall = obj;
        var child = progressBar.Spawn(obj, message, progressBarOptions);
        childs.Add(message, child);
    }
    public void LyricsHelper_AnotherSong(string message)
    {
        childs[message].Tick();
    }
    public void Dispose()
    {
        progressBar.Dispose();
    }
}