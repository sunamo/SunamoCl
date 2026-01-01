namespace SunamoCl._public.SunamoArgs;

public class CreateAppFoldersIfDontExistsArgsCl
{
    public string AppName { get; set; } = "";



    public List<string> KeysCommonSettings { get; set; } = new List<string>();



    public List<string> KeysSettingsList { get; set; } = new List<string>();
    public List<string> KeysSettingsBool { get; set; } = new List<string>();
    public List<string> KeysSettingsOther { get; set; } = new List<string>();
    public Func<List<byte>, List<byte>> RijndaelBytesDecrypt { get; set; } = null!;
    public Func<List<byte>, List<byte>> RijndaelBytesEncrypt { get; set; } = null!;
}