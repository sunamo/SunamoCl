namespace SunamoCl._public.SunamoLogging.Base;

/// <summary>
///     In difference with LoggerBase take type of message as enum
/// </summary>
public abstract class TypedLoggerBaseCl
{
    private static Type _type = typeof(TypedLoggerBaseCl);
    private readonly Action<TypeOfMessageCl, string, string[]> _typedWriteLineDelegate;

    public TypedLoggerBaseCl(Action<TypeOfMessageCl, string, string[]> typedWriteLineDelegate)
    {
        _typedWriteLineDelegate = typedWriteLineDelegate;
    }

#if !DEBUG2
    public TypedLoggerBaseCl()
    {
    }
#endif

    public void WriteLineFormat(string formatString, params string[] args)
    {
        Ordinal(formatString, args);
    }

    #region

    public void Success(string text, params string[] args)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Success, text, args);
    }

    public void Error(string text, params string[] args)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Error, text, args);
    }

    public void Warning(string text, params string[] args)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Warning, text, args);
    }

    public void Appeal(string text, params string[] args)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Appeal, text, args);
    }

    public void Ordinal(string text, params string[] args)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Ordinal, text, args);
    }

    public void WriteLine(TypeOfMessageCl messageType, string message)
    {
        switch (messageType)
        {
            case TypeOfMessageCl.Error:
                Error(message);
                break;
            case TypeOfMessageCl.Warning:
                Warning(message);
                break;
            case TypeOfMessageCl.Information:
                Information(message);
                break;
            case TypeOfMessageCl.Ordinal:
                Ordinal(message);
                break;
            case TypeOfMessageCl.Appeal:
                Appeal(message);
                break;
            case TypeOfMessageCl.Success:
                Success(message);
                break;
            default:
                ThrowEx.NotImplementedCase(messageType);
                break;
        }
    }

    public void Information(string text, params string[] args)
    {
        _typedWriteLineDelegate.Invoke(TypeOfMessageCl.Information, text, args);
    }

    #endregion
}