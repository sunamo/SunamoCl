namespace SunamoCl._public.SunamoArgs;

/// <summary>
/// Represents the create app folders if dont exists args cl type.
/// </summary>
public class CreateAppFoldersIfDontExistsArgsCl
{
    /// <summary>
    /// Gets or sets the app name.
    /// </summary>
    public string AppName { get; set; } = "";

    /// <summary>
    /// Gets or sets the keys common settings.
    /// </summary>
    public List<string> KeysCommonSettings { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the keys settings list.
    /// </summary>
    public List<string> KeysSettingsList { get; set; } = new List<string>();
    /// <summary>
    /// Gets or sets the keys settings bool.
    /// </summary>
    public List<string> KeysSettingsBool { get; set; } = new List<string>();
    /// <summary>
    /// Gets or sets the keys settings other.
    /// </summary>
    public List<string> KeysSettingsOther { get; set; } = new List<string>();
    /// <summary>
    /// Gets or sets the rijndael bytes decrypt.
    /// </summary>
    public Func<List<byte>, List<byte>> RijndaelBytesDecrypt { get; set; } = null!;
    /// <summary>
    /// Gets or sets the rijndael bytes encrypt.
    /// </summary>
    public Func<List<byte>, List<byte>> RijndaelBytesEncrypt { get; set; } = null!;
}
