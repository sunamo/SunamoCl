namespace SunamoCl._sunamo;

/// <summary>
/// File system helper class for path manipulation.
/// </summary>
internal class FS
{
    /// <summary>
    /// Ensures the path ends with a backslash.
    /// </summary>
    /// <param name="path">Path to normalize.</param>
    internal static string WithEndSlash(string path)
    {
        return path.TrimEnd('\\') + "\\";
    }
}