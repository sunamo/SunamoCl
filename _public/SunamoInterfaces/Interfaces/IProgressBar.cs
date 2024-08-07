namespace SunamoCl._public.SunamoInterfaces.Interfaces;

public interface IProgressBar
{
    bool isRegistered { get; set; }
    int writeOnlyDividableBy { get; set; }
    void Init(IPercentCalculatorCl pc);
    void Init(IPercentCalculatorCl pc, bool isNotUt);


    void LyricsHelper_AnotherSong(object asyncResult);
    void LyricsHelper_AnotherSong();
    void LyricsHelper_AnotherSong(int i);
    void LyricsHelper_OverallSongs(int obj);
    void LyricsHelper_WriteProgressBarEnd();
}