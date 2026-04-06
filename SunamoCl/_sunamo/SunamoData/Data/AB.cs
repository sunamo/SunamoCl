namespace SunamoCl._sunamo.SunamoData.Data;

/// <summary>
/// Simple key-value pair where key is a string and value is an object.
/// </summary>
internal class AB
{
    /// <summary>
    /// Gets or sets the key (string label).
    /// </summary>
    internal string Key { get; set; }
    /// <summary>
    /// Gets or sets the value (object data).
    /// </summary>
    internal object Value { get; set; }

    /// <summary>
    /// Initializes a new instance with the specified key and value.
    /// </summary>
    /// <param name="key">String key.</param>
    /// <param name="value">Object value.</param>
    internal AB(string key, object value)
    {
        Key = key;
        Value = value;
    }

    /// <summary>
    /// Creates a new AB instance with the specified key and value.
    /// </summary>
    /// <param name="key">String key.</param>
    /// <param name="value">Object value.</param>
    internal static AB Get(string key, object value)
    {
        return new AB(key, value);
    }

    /// <summary>
    /// Returns a string representation in "Key:Value" format.
    /// </summary>
    public override string ToString()
    {
        return Key + ":" + Value;
    }
}