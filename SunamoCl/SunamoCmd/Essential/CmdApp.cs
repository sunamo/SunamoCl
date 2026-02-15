namespace SunamoCl.SunamoCmd.Essential;

/// <summary>
/// Provides core command-line application functionality including file operations, logging setup and unhandled exception handling
/// </summary>
public class CmdApp
{
    /// <summary>
    ///     Dont ask in console, load from Clipboard
    /// </summary>
    public static bool ShouldWaitOnEnd { get; set; } = false;

    /// <summary>
    /// Gets or sets whether to open and wait for content change of an input file
    /// </summary>
    public static bool OpenAndWaitForChangeContentOfInputFile { get; set; } = true;

    /// <summary>
    /// Gets or sets whether input should be loaded from clipboard instead of console
    /// </summary>
    public static bool LoadFromClipboard { get; internal set; }


    /// <summary>
    ///     Create in class where are you calling method without A2 openVsCode
    /// </summary>
    /// <param name="logger">Logger instance for diagnostic output</param>
    /// <param name="path">Path to the file to wait for</param>
    /// <param name="openVsCode">Action to open the file in VS Code for editing</param>
    /// <returns>Content of the file after saving</returns>
    public static
#if ASYNC
        async Task<string>
#else
    string
#endif
        WaitForSaving(ILogger logger, string path, Action<ILogger, string, bool, int?> openVsCode)
    {
        Console.WriteLine($"Running WaitForSaving\n   File: {path}\n   Auto-open: {OpenAndWaitForChangeContentOfInputFile}");

        if (OpenAndWaitForChangeContentOfInputFile)
        {
            openVsCode(logger, path, false, null);
            CL.WriteLine(
                $"Waiting for insert html to {Path.GetFileName(path)}, press enter to continue");
            CL.ReadLine();
        }

        Console.WriteLine($"Reading file: {path}");

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

    /// <summary>
    /// Waits for user input before ending the application in debug mode
    /// </summary>
    public static void WaitOnEnd()
    {
#if DEBUG
        if (ShouldWaitOnEnd) CL.ReadLine();
#endif
    }

    /// <summary>
    /// Initializes the command-line application environment
    /// </summary>
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