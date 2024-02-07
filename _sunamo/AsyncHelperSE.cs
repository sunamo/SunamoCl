namespace SunamoCl._sunamo;
internal class AsyncHelperSE
{
    internal static
#if ASYNC
async Task
#else
        void
#endif
InvokeTaskVoidOrVoidVoid(object o)
    {
        var t = o.GetType();

        if (t == Types.tVoidVoid)
        {
            (o as VoidVoid).Invoke();
        }
        else if (t == Types.tTaskVoid)
        {
            var taskVoid = o as TaskVoid;
            await taskVoid(); ;
        }
    }

    internal static Dictionary<string, object> MergeDictionaries(Dictionary<string, VoidVoid> potentiallyValid,
        Dictionary<string, TaskVoid> potentiallyValidAsync)
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
