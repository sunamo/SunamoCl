namespace SunamoCl._public.SunamoArgs;

/// <summary>
/// Arguments for creating application folders if they do not exist, including settings keys and encryption functions
/// </summary>
public class CreateAppFoldersIfDontExistsArgsCl
{
    /// <summary>
    /// Gets or sets the name of the application
    /// </summary>
    public string AppName { get; set; } = "";



    /// <summary>
    /// Gets or sets the list of common settings keys
    /// </summary>
    public List<string> KeysCommonSettings { get; set; } = new List<string>();



    /// <summary>
    /// Gets or sets the list-type settings keys
    /// </summary>
    public List<string> KeysSettingsList { get; set; } = new List<string>();
    /// <summary>
    /// Gets or sets the boolean-type settings keys
    /// </summary>
    public List<string> KeysSettingsBool { get; set; } = new List<string>();
    /// <summary>
    /// Gets or sets the other-type settings keys
    /// </summary>
    public List<string> KeysSettingsOther { get; set; } = new List<string>();
    /// <summary>
    /// Gets or sets the Rijndael decryption function for byte lists
    /// </summary>
    public Func<List<byte>, List<byte>> RijndaelBytesDecrypt { get; set; } = null!;
    /// <summary>
    /// Gets or sets the Rijndael encryption function for byte lists
    /// </summary>
    public Func<List<byte>, List<byte>> RijndaelBytesEncrypt { get; set; } = null!;
}