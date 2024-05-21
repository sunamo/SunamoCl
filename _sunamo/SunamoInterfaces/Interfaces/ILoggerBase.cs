namespace SunamoCl;

using System.Collections;

public interface ILoggerBase
{
    void ClipboardOrDebug(string v, params string[] args);
    bool IsInRightFormat(string text, params string[] args);
    void TwoState(bool ret, params string[] toAppend);
    void WriteCount(string collectionName, IList list);
    void WriteLine(string what, object text);
    void WriteLine(string text, params string[] args);
    void WriteLineFormat(string v1, params string[] name);
    void WriteLineNull(string text, params string[] args);
    void WriteList(List<string> list);
    void WriteList(string collectionName, List<string> list);
    void WriteListOneRow(List<string> item, string swd);
    void WriteNumberedList(string what, List<string> list, bool numbered);
}