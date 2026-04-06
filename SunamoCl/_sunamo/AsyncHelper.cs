namespace SunamoCl._sunamo;

/// <summary>
/// Helper class for async-related utility methods.
/// </summary>
internal class AsyncHelper
{
    /// <summary>
    /// Merges synchronous and asynchronous action dictionaries into a single dictionary.
    /// </summary>
    /// <param name="potentiallyValid">Synchronous actions dictionary.</param>
    /// <param name="potentiallyValidAsync">Asynchronous actions dictionary.</param>
    internal static Dictionary<string, object> MergeDictionaries(Dictionary<string, Action> potentiallyValid,
        Dictionary<string, Func<Task>> potentiallyValidAsync)
    {
        var mergedActions = new Dictionary<string, object>(potentiallyValid.Count + potentiallyValidAsync.Count);
        if (potentiallyValid != null)
            foreach (var item in potentiallyValid)
                mergedActions.Add(item.Key, item.Value);
        if (potentiallyValidAsync != null)
            foreach (var item in potentiallyValidAsync)
                mergedActions.Add(item.Key, item.Value);
        return mergedActions;
    }
}