// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoCl._sunamo;

//namespace SunamoCl._sunamo.SunamoExceptions._AddedToAllCsproj;
internal class FS
{
    internal static string WithEndSlash(string folderPath)
    {
        return folderPath.TrimEnd('\\') + "\\";
    }
}