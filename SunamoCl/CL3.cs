// Instance variables refactored according to C# conventions
namespace SunamoCl;
partial class CL
{
    public static
#if ASYNC
    async Task
#else
        void
#endif
    InvokeFuncTaskOrAction(object o)
    {
        var objectType = o.GetType();
        if (objectType == TypesDelegates.tAction)
        {
            (o as Action).Invoke();
        }
        else if (objectType == TypesDelegates.tFuncTask)
        {
            var voidTask = o as Func<Task>;
            await voidTask();
        }
    }
}