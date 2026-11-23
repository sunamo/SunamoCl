namespace SunamoCl;

partial class CL
{
    /// <summary>
    /// Invoke func task or action.
    /// </summary>
    /// <param name="funcOrAction">The func or action.</param>
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
