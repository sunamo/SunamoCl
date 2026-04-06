namespace SunamoCl;

partial class CL
{
    /// <summary>
    /// Invokes either a synchronous Action or asynchronous Func&lt;Task&gt; delegate.
    /// </summary>
    /// <param name="funcOrAction">Either an Action or Func&lt;Task&gt; to invoke.</param>
    public static
#if ASYNC
    async Task
#else
        void
#endif
    InvokeFuncTaskOrAction(object funcOrAction)
    {
        var objectType = funcOrAction.GetType();
        if (objectType == TypesDelegates.ActionType)
        {
            (funcOrAction as Action)!.Invoke();
        }
        else if (objectType == TypesDelegates.FuncTaskType)
        {
            var asyncFunc = funcOrAction as Func<Task>;
            await asyncFunc!();
        }
    }
}