namespace SunamoCl._sunamo.SunamoLang.SunamoI18N;

/// <summary>
/// Provides translation functionality for localization keys.
/// </summary>
internal class Translate
{
    /// <summary>
    /// Returns the translation for the given key. Currently returns the key as-is.
    /// </summary>
    /// <param name="key">Localization key.</param>
    internal static string FromKey(string key)
    {
        return key;
    }
}