namespace SunamoCl._sunamo;

internal class FS
{
    internal static string WithEndSlash(string path)
    {
        return path.TrimEnd('\\') + "\\";
    }
}
