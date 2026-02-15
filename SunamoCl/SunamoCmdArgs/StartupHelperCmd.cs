namespace SunamoCl.SunamoCmdArgs;

/// <summary>
/// Provides static access to startup command-line arguments for the application
/// </summary>
public class StartupHelperCmd //: StartupHelperBase
{
    /// <summary>
    /// Gets or sets the command-line arguments passed to the application at startup
    /// </summary>
    public static string[]? Args { get; set; } = null;
}