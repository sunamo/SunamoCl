namespace SunamoCl;

/// <summary>
/// Defines source identifier characters for distinguishing clipboard operations from other console operations.
/// </summary>
public class ClSources
{
    /// <summary>
    /// Source identifier for clipboard operations.
    /// </summary>
    public static readonly char Clipboard = 'a';

    /// <summary>
    /// Source identifier for all other operations.
    /// </summary>
    public static readonly char Others = 'z';
}
