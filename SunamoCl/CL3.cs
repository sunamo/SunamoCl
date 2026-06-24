namespace SunamoCl;

partial class CL
{
    public static
    async Task
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
