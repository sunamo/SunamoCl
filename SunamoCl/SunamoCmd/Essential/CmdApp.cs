namespace SunamoCl.SunamoCmd.Essential;

/// <summary>
/// Represents the cmd app type.
/// </summary>
public class CmdApp
{
    /// <summary>
    /// Gets or sets the should wait on end.
    /// </summary>
    public static bool ShouldWaitOnEnd { get; set; } = false;

    /// <summary>
    /// Gets or sets the open and wait for change content of input file.
    /// </summary>
    public static bool OpenAndWaitForChangeContentOfInputFile { get; set; } = true;

    /// <summary>
    /// Gets or sets the should load from clipboard.
    /// </summary>
    public static bool ShouldLoadFromClipboard { get; internal set; }

    /// <summary>
    /// Wait for saving.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="path">The path.</param>
    /// <param name="openVsCode">The open vs code.</param>
    /// <returns>The wait for saving result.</returns>
    public static
        async Task<string>
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
            await FileAsync.WriteAllTextAsync(path, string.Empty);
            return string.Empty;
        }

        return
            await
                FileAsync.ReadAllTextAsync(path);
    }

    /// <summary>
    /// Wait on end.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="string.Empty">The string empty.</param>
    /// <returns>The wait on end result.</returns>
    public static void WaitOnEnd()
    {
    }

    /// <summary>
    /// Init.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The init result.</returns>
    public static void Init()
    {
    }

    internal static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs eventArgs)
    {
        throw (Exception)eventArgs.ExceptionObject;
    }

    /// <summary>
    /// Set logger.
    /// </summary>
    public static void SetLogger()
    {
    }
}
