namespace SunamoCl._sunamo;

//namespace SunamoCl._sunamo.SunamoExceptions._AddedToAllCsproj;
internal class FS
{
    internal static void CreateFoldersPsysicallyUnlessThere(string? nad)
    {
        ThrowEx.IsNullOrEmpty("nad", nad);
        if (Directory.Exists(nad))
        {
            return;
        }
        List<string> slozkyKVytvoreni = new List<string>
{
nad
};
        while (true)
        {
            nad = Path.GetDirectoryName(nad);

            if (nad == null)
            {
                break;
            }

            if (Directory.Exists(nad))
            {
                break;
            }

            slozkyKVytvoreni.Add(nad);
        }
        slozkyKVytvoreni.Reverse();
        foreach (string item in slozkyKVytvoreni)
        {
            string folder = item;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
    }

}