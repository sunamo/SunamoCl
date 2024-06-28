namespace SunamoCl;


/// <summary>
/// Cant add another methods with void and normal - methods have same signature, despite return were different
/// </summary>
internal class ClipboardHelper
{
    internal static IClipboardHelper Instance = null;
    internal static IClipboardHelperApps InstanceApps = null;
    private ClipboardHelper() { }
    internal static bool ContainsText()
    {
        if (Instance == null)
        {
            return InstanceApps.ContainsText();
        }
        else
        {
            return Instance.ContainsText();
        }
    }
    internal static string GetText()
    {
        if (Instance == null)
        {
            return InstanceApps.GetText();
        }
        else
        {
            return Instance.GetText();
        }
    }
    internal static List<string> GetLinesAllWhitespaces()
    {
        var t = GetText();
        return t.Split(AllChars.whiteSpacesChars.ToArray()).ToList();
    }
    internal static List<string> GetLines()
    {
#if !UNITTEST
        if (Instance == null)
        {
            return InstanceApps.GetLines();
        }
        else
        {
            return Instance.GetLines();
        }
#endif
    }
    /// <summary>
    /// Cant be se or only whitespace => even with ClipboardHelper.SetText(v); => content of clipboard will remain the same
    /// Must
    /// </summary>
    /// <param name="s"></param>
    internal static void SetText(string s)
    {
#if !UNITTEST
        if (Instance == null)
        {
            InstanceApps.SetText(s);
        }
        else
        {
            Instance.SetText(s);
        }
#endif
    }
    internal static void SetText2(string s)
    {
        if (Instance == null)
        {
            InstanceApps.SetText2(s);
        }
        else
        {
            Instance.SetText2(s);
        }
    }
    internal static void SetList(List<string> d)
    {
        if (Instance == null)
        {
            InstanceApps.SetList(d);
        }
        else
        {
            Instance.SetList(d);
        }
    }
    internal static void SetLines(List<string> lines)
    {
        if (Instance == null)
        {
            InstanceApps.SetLines(lines);
        }
        else
        {
            Instance.SetLines(lines);
        }
    }
    internal static void CutFiles(params string[] selected)
    {
        if (Instance == null)
        {
            InstanceApps.CutFiles(selected);
        }
        else
        {
            Instance.CutFiles(selected);
        }
    }
    //internal static void SetText(TextBuilder stringBuilder)
    //{
    //    if (Instance == null)
    //    {
    //        InstanceApps.SetText(stringBuilder);
    //    }
    //    else
    //    {
    //        Instance.SetText(stringBuilder);
    //    }
    //}
    internal static void SetText3(string s)
    {
        if (Instance == null)
        {
            InstanceApps.SetText3(s);
        }
        else
        {
            Instance.SetText3(s);
        }
    }
    internal static void SetText(StringBuilder stringBuilder)
    {
        if (Instance == null)
        {
            InstanceApps.SetText(stringBuilder.ToString());
        }
        else
        {
            Instance.SetText(stringBuilder.ToString());
        }
    }
    internal static void SetDictionary<T1, T2>(Dictionary<T1, T2> charEntity, string delimiter)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in charEntity)
        {
            sb.AppendLine(item.Key + delimiter + item.Value);
        }
        SetText(sb.ToString());
    }
    internal static void AppendText(string ext)
    {
        var t = GetText();
        t += Environment.NewLine + Environment.NewLine + ext;
        SetText(t);
    }
    internal static void AppendStackTrace()
    {
        var st = Exc.GetStackTrace(true);
        AppendText(st);
    }
}