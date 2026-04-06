namespace SunamoCl._sunamo;

// variables names: ok
/// <summary>
/// Defines the strategy for string matching operations.
/// </summary>
internal enum SearchStrategy
{
    /// <summary>
    /// Simple contains check on the original string.
    /// </summary>
    FixedSpace,
    /// <summary>
    /// Splits both searched and search term by spaces; all parts of the search term must be present in the input.
    /// </summary>
    AnySpaces,
    /// <summary>
    /// Requires an exact match between input and search term.
    /// </summary>
    ExactlyName
}