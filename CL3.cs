
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

        var t = o.GetType();
        if (t == Types.tAction)
        {
            (o as Action).Invoke();
        }
        else if (t == TypesDelegates.tFuncTask)
        {
            var taskVoid = o as Func<Task>;
            await taskVoid(); ;
        }
    }
}