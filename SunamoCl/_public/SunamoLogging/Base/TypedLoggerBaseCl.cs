namespace SunamoCl._public.SunamoLogging.Base;

/// <summary>
/// Represents the typed logger base cl type.
/// </summary>
public abstract class TypedLoggerBaseCl
{
    private readonly Action<TypeOfMessageCl, string, string[]> typedWriteLineDelegate = null!;

    /// <summary>
    /// Initializes a new instance of the typed logger base cl class.
    /// </summary>
    /// <param name="typedWriteLineDelegate">The typed write line delegate.</param>
    public TypedLoggerBaseCl(Action<TypeOfMessageCl, string, string[]> typedWriteLineDelegate)
    {
        this.typedWriteLineDelegate = typedWriteLineDelegate;
    }


    /// <summary>
    /// Write line format.
    /// </summary>
    /// <param name="formatString">The format string.</param>
    /// <param name="args">The args.</param>
    public void WriteLineFormat(string formatString, params string[] args)
    {
        Ordinal(formatString, args);
    }

    /// <summary>
    /// Success.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    public void Success(string text, params string[] args)
    {
        typedWriteLineDelegate.Invoke(TypeOfMessageCl.Success, text, args);
    }

    /// <summary>
    /// Error.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    public void Error(string text, params string[] args)
    {
        typedWriteLineDelegate.Invoke(TypeOfMessageCl.Error, text, args);
    }

    /// <summary>
    /// Warning.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    public void Warning(string text, params string[] args)
    {
        typedWriteLineDelegate.Invoke(TypeOfMessageCl.Warning, text, args);
    }

    /// <summary>
    /// Appeal.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    public void Appeal(string text, params string[] args)
    {
        typedWriteLineDelegate.Invoke(TypeOfMessageCl.Appeal, text, args);
    }

    /// <summary>
    /// Ordinal.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    public void Ordinal(string text, params string[] args)
    {
        typedWriteLineDelegate.Invoke(TypeOfMessageCl.Ordinal, text, args);
    }

    /// <summary>
    /// Write line.
    /// </summary>
    /// <param name="messageType">The message type.</param>
    /// <param name="message">The message.</param>
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

    /// <summary>
    /// Information.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="args">The args.</param>
    public void Information(string text, params string[] args)
    {
        typedWriteLineDelegate.Invoke(TypeOfMessageCl.Information, text, args);
    }
}
