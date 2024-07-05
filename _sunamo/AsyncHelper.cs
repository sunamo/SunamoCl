namespace SunamoCl._sunamo;


internal class AsyncHelper
{
    internal static Dictionary<string, object> MergeDictionaries(Dictionary<string, Action> potentiallyValid,
        Dictionary<string, Func<Task>> potentiallyValidAsync)
    {
        var actionsMerge = new Dictionary<string, object>(potentiallyValid.Count + potentiallyValidAsync.Count);
        if (potentiallyValid != null)
            foreach (var item in potentiallyValid)
                actionsMerge.Add(item.Key, item.Value);
        if (potentiallyValidAsync != null)
            foreach (var item in potentiallyValidAsync)
                actionsMerge.Add(item.Key, item.Value);
        return actionsMerge;
    }
}
