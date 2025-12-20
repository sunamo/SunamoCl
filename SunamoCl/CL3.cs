namespace SunamoCl;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// Instance variables refactored according to C# conventions
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
        if (objectType == TypesDelegates.TAction)
        {
            (o as Action).Invoke();
        }
        else if (objectType == TypesDelegates.TFuncTask)
        {
            var voidTask = o as Func<Task>;
            await voidTask();
        }
    }
}