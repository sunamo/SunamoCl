// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
//namespace SunamoCl._public.SunamoInterfaces.Interfaces;

//public class ProgressStateCl
//{
//    public int currentSongCount;
//    public bool isRegistered { get; set; }

//    public void Init(Action<int> OverallSongs, Action<int> AnotherSong, Action WriteProgressBarEnd)
//    {
//        isRegistered = true;
//        this.AnotherSong += AnotherSong;
//        this.OverallSongs += OverallSongs;
//        this.WriteProgressBarEnd += WriteProgressBarEnd;
//    }

//    public event Action<int> AnotherSong;
//    public event Action<int> OverallSongs;
//    public event Action WriteProgressBarEnd;

//    public void OnAnotherSong()
//    {
//        currentSongCount++;
//        OnAnotherSong(currentSongCount);
//    }

//    public void OnAnotherSong(int songNumber)
//    {
//        AnotherSong(songNumber);
//    }

//    public void OnOverallSongs(int totalSongs)
//    {
//        currentSongCount = 0;
//        OverallSongs(totalSongs);
//    }

//    public void OnWriteProgressBarEnd()
//    {
//        WriteProgressBarEnd();
//    }
//}