// variables names: ok
namespace SunamoCl;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// Instance variables refactored according to C# conventions
partial class CL
{
    /// <summary>
    /// Invokes either a synchronous Action or asynchronous Func&lt;Task&gt; delegate
    /// </summary>
    /// <param name="funcOrAction">Either an Action or Func&lt;Task&gt; to invoke</param>
    public static
#if ASYNC
    async Task
#else
        void
#endif
    InvokeFuncTaskOrAction(object funcOrAction)
    {
        var objectType = funcOrAction.GetType();
        if (objectType == TypesDelegates.TAction)
        {
            (funcOrAction as Action)!.Invoke();
        }
        else if (objectType == TypesDelegates.TFuncTask)
        {
            var voidTask = funcOrAction as Func<Task>;
            await voidTask!();
        }
    }
}