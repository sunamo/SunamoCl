namespace SunamoCl._sunamo;

internal class AsyncHelper
{
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
