namespace SunamoCl.SunamoCmd.Essential;

/// <summary>
/// Provides core command-line application functionality including file operations, logging setup and unhandled exception handling
/// </summary>
public class CmdApp
{
    /// <summary>
    /// Gets or sets whether to wait for user input before application ends.
    /// </summary>
    public static bool ShouldWaitOnEnd { get; set; } = false;

    /// <summary>
    /// Gets or sets whether to open and wait for content change of an input file
    /// </summary>
    public static bool OpenAndWaitForChangeContentOfInputFile { get; set; } = true;

    /// <summary>
    /// Gets or sets whether input should be loaded from clipboard instead of console
    /// </summary>
    public static bool ShouldLoadFromClipboard { get; internal set; }


    /// <summary>
    /// Waits for the user to save a file, optionally opening it in VS Code first.
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
    }

    /// <summary>
    /// Handles unhandled exceptions by rethrowing them.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="eventArgs">Event arguments containing the exception.</param>
    internal static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs eventArgs)
    {
        throw (Exception)eventArgs.ExceptionObject;
    }

    /// <summary>
    /// Initializes the logger. Alternatives: InitApp.SetDebugLogger, WpfApp.SetLogger.
    /// </summary>
    public static void SetLogger()
    {
    }
}