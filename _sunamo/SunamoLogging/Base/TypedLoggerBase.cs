namespace SunamoCl._sunamo.SunamoLogging.Base;



/// <summary>
/// In difference with LoggerBase take type of message as enum
/// </summary>
internal abstract class TypedLoggerBase
{
    private static Type type = typeof(TypedLoggerBase);
    private Action<TypeOfMessageCl, string, string[]> _typedWriteLineDelegate;

    internal TypedLoggerBase(Action<TypeOfMessageCl, string, string[]> typedWriteLineDelegate)
    {
        _typedWriteLineDelegate = typedWriteLineDelegate;
    }

#if !DEBUG2
    internal TypedLoggerBase()
    {

    }
#endif




    /// <summary>
    /// Only due to Old sfw apps
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="name"></param>
    /// <param name="v2"></param>
    internal void WriteLineFormat(string v1, params string[] name)
    {
        Ordinal(v1, name);
    }

    #region 
    internal void Success(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Success, text, p);
    }

    internal void Error(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Error, text, p);
    }
    internal void Warning(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Warning, text, p);
    }

    internal void Appeal(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Appeal, text, p);
    }

    internal void Ordinal(string text, params string[] p)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Ordinal, text, p);
    }

    internal void WriteLine(TypeOfMessageCl t, string m)
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

    internal void Information(string text, params string[] p)
    {

        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Information, text, p);
    }
    #endregion
}
