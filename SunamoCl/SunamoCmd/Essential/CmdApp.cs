namespace SunamoCl.SunamoCmd.Essential;

public class CmdApp
{
    public static bool ShouldWaitOnEnd { get; set; } = false;

    public static bool OpenAndWaitForChangeContentOfInputFile { get; set; } = true;

    public static bool ShouldLoadFromClipboard { get; internal set; }

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

    public static void WaitOnEnd()
    {
    }

    public static void Init()
    {
    }

    internal static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs eventArgs)
    {
        throw (Exception)eventArgs.ExceptionObject;
    }

    public static void SetLogger()
    {
    }
}
