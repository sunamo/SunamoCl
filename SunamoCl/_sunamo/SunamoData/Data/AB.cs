namespace SunamoCl._sunamo.SunamoData.Data;

internal class AB
{
    internal string Key { get; set; }
    internal object Value { get; set; }

    internal AB(string key, object value)
    {
        Key = key;
        Value = value;
    }

    internal static AB Get(string key, object value) => new AB(key, value);

    public override string ToString() => $"{Key}:{Value}";
}
