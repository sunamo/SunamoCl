namespace SunamoCl._public.SunamoArgs;

public class CreateAppFoldersIfDontExistsArgsCl
{
    public string AppName = "";



    public List<string> KeysCommonSettings = new List<string>();



    public List<string> KeysSettingsList = new List<string>();
    public List<string> KeysSettingsBool = new List<string>();
    public List<string> KeysSettingsOther = new List<string>();
    public Func<List<byte>, List<byte>> RijndaelBytesDecrypt;
    public Func<List<byte>, List<byte>> RijndaelBytesEncrypt;
}