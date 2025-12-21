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
    InvokeFuncTaskOrAction(object funcOrAction)
    {
        var objectType = funcOrAction.GetType();
        if (objectType == TypesDelegates.TAction)
        {
            (funcOrAction as Action).Invoke();
        }
        else if (objectType == TypesDelegates.TFuncTask)
        {
            var voidTask = funcOrAction as Func<Task>;
            await voidTask();
        }
    }
}