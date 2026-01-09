// variables names: ok
namespace SunamoCl.SunamoCmd.Essential;

public class CmdApp
{
    /// <summary>
    ///     Dont ask in console, load from Clipboard
    /// </summary>
    public static bool ShouldWaitOnEnd { get; set; } = false;

    public static bool OpenAndWaitForChangeContentOfInputFile { get; set; } = true;

    public static bool LoadFromClipboard { get; internal set; }


    /// <summary>
    ///     Create in class where are you calling method without A2 openVsCode
    /// </summary>
    /// <param name="path"></param>
    /// <param name="openVsCode"></param>
    public static
#if ASYNC
        async Task<string>
#else
    string
#endif
        WaitForSaving(ILogger logger, string path, Action<ILogger, string, bool, int?> openVsCode)
    {
        Console.WriteLine($"🔄 Running WaitForSaving\n   📄 File: {path}\n   🎯 Auto-open: {OpenAndWaitForChangeContentOfInputFile}");

        if (OpenAndWaitForChangeContentOfInputFile)
        {
            openVsCode(logger, path, false, null);
            CL.WriteLine(
                $"Waiting for insert html to {Path.GetFileName(path)}, press enter to continue");
            CL.ReadLine();
        }

        Console.WriteLine($"📖 Reading file: {path}");

        if (!File.Exists(path))
        {
            await File.WriteAllTextAsync(path, string.Empty);
            return string.Empty;
        }

        return
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(path);
    }

    public static void WaitOnEnd()
    {
#if DEBUG
        if (ShouldWaitOnEnd) CL.ReadLine();
#endif
    }

    public static void Init()
    {
        // Nev�m zda je dobr� n�pad. Kdy� vznikne nechycen� exception, dostane se do UnhandledExceptionTrapper() ale u� to nem�m v debuggeru VS. Mo�n� to je jen �patn�m nastaven�m IDE.

        //AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
    }

    /// <summary>
    /// Nev�m zda je dobr� n�pad. Kdy� vznikne nechycen� exception, dostane se do UnhandledExceptionTrapper() ale u� to nem�m v debuggeru VS. Mo�n� to je jen �patn�m nastaven�m IDE.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    internal static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
    {
        throw (Exception)e.ExceptionObject;
    }

    /// <summary>
    ///     Alternatives are:
    ///     InitApp.SetDebugLogger
    ///     CmdApp.SetLogger
    ///     WpfApp.SetLogger
    /// </summary>
    public static void SetLogger()
    {
    }
}