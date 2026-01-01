// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoCl.Args;

/// <summary>
/// Arguments for configuring list output formatting
/// </summary>
public class WriteListArgs
{
    /// <summary>
    /// String to wrap each list item into
    /// </summary>
    public string WrapInto { get; set; } = string.Empty;

    /// <summary>
    /// Whether to write line numbers before each item
    /// </summary>
    public bool WriteNumber { get; set; }
}