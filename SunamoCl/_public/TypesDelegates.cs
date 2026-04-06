namespace SunamoCl._public;

/// <summary>
/// Provides cached Type references for commonly used delegate types.
/// </summary>
internal class TypesDelegates
{
    /// <summary>
    /// Cached Type reference for the Action delegate.
    /// </summary>
    internal static readonly Type ActionType = typeof(Action);
    /// <summary>
    /// Cached Type reference for the Func&lt;Task&gt; delegate.
    /// </summary>
    internal static readonly Type FuncTaskType = typeof(Func<Task>);
}