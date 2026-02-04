namespace SunamoCl._sunamo;

internal class FS
{
    internal static string WithEndSlash(string folderPath)
    {
        return folderPath.TrimEnd('\\') + "\\";
    }
}