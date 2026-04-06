namespace SunamoCl._sunamo;

/// <summary>
/// Helper class for async-related utility methods.
/// </summary>
internal class AsyncHelper
{
    /// <summary>
    /// Merges synchronous and asynchronous action dictionaries into a single dictionary.
    /// </summary>
    /// <param name="syncActions">Synchronous actions dictionary.</param>
    /// <param name="asyncActions">Asynchronous actions dictionary.</param>
    internal static Dictionary<string, object> MergeDictionaries(Dictionary<string, Action> syncActions,
        Dictionary<string, Func<Task>> asyncActions)
    {
        var mergedActions = new Dictionary<string, object>(syncActions.Count + asyncActions.Count);
        if (syncActions != null)
            foreach (var item in syncActions)
                mergedActions.Add(item.Key, item.Value);
        if (asyncActions != null)
            foreach (var item in asyncActions)
                mergedActions.Add(item.Key, item.Value);
        return mergedActions;
    }
}