namespace SunamoCl._sunamo;

//namespace SunamoCl._sunamo.SunamoExceptions._AddedToAllCsproj;
internal class FS
{
    internal static string WithEndSlash(string folderPath)
    {
        return folderPath.TrimEnd('\\') + "\\";
    }
}