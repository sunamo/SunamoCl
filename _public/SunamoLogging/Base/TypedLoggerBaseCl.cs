
namespace SunamoCl._sunamo.SunamoLogging.Base;
/// <summary>
/// In difference with LoggerBase take type of message as enum
/// </summary>
public abstract class TypedLoggerBaseCl
{
    private static Type type = typeof(TypedLoggerBaseCl);
    private Action<TypeOfMessageCl, string, string[]> _typedWriteLineDelegate;

    public TypedLoggerBaseCl(Action<TypeOfMessageCl, string, string[]> typedWriteLineDelegate)
    {
        _typedWriteLineDelegate = typedWriteLineDelegate;
    }

#if !DEBUG2
    public TypedLoggerBaseCl()
    {

    }
#endif




    /// <summary>
    /// Only due to Old sfw apps
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="name"></param>
    /// <param name="v2"></param>
    public void WriteLineFormat(string v1, params string[] name)
    {
        Ordinal(v1, name);
    }

    #region 
    public void Success(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Success, text, p);
    }

    public void Error(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Error, text, p);
    }
    public void Warning(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Warning, text, p);
    }

    public void Appeal(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Appeal, text, p);
    }

    public void Ordinal(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Ordinal, text, p);
    }

    public void WriteLine(TypeOfMessageCl t, string m)
    {
        switch (t)
        {
            case TypeOfMessageCl.Error:
                Error(m);
                break;
            case TypeOfMessageCl.Warning:
                Warning(m);
                break;
            case TypeOfMessageCl.Information:
                Information(m);
                break;
            case TypeOfMessageCl.Ordinal:
                Ordinal(m);
                break;
            case TypeOfMessageCl.Appeal:
                Appeal(m);
                break;
            case TypeOfMessageCl.Success:
                Success(m);
                break;
            default:
                ThrowEx.NotImplementedCase(t);
                break;
        }
    }

    public void Information(string text, params string[] p)
    {

        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Information, text, p);
    }
    #endregion
}
