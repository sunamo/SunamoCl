namespace SunamoCl._sunamo.SunamoParsing;

/// <summary>
/// Provides nested classes for try-parse operations on various types.
/// </summary>
internal class TryParse
{
    /// <summary>
    /// Integer parsing helper with state tracking.
    /// </summary>
    internal class Integer
    {
        /// <summary>
        /// Singleton instance of the Integer parser.
        /// </summary>
        internal static Integer Instance = new();
        /// <summary>
        /// Gets or sets the last successfully parsed integer value.
        /// </summary>
        internal int LastInt { get; set; } = -1;
    }
}