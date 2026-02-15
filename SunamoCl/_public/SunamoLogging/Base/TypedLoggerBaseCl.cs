namespace SunamoCl._public.SunamoLogging.Base;

/// <summary>
///     In difference with LoggerBase take type of message as enum
/// </summary>
public abstract class TypedLoggerBaseCl
{
    private static Type type = typeof(TypedLoggerBaseCl);
    private readonly Action<TypeOfMessageCl, string, string[]> typedWriteLineDelegate = null!;

    /// <summary>
    /// Initializes a new instance with the specified typed write delegate
    /// </summary>
    /// <param name="typedWriteLineDelegate">Delegate that writes messages with a message type, text and arguments</param>
    public TypedLoggerBaseCl(Action<TypeOfMessageCl, string, string[]> typedWriteLineDelegate)
    {
        this.typedWriteLineDelegate = typedWriteLineDelegate;
    }

#if !DEBUG2
    /// <summary>
    /// Initializes a new instance with no write delegate (for non-debug builds)
    /// </summary>
    public TypedLoggerBaseCl()
    {
    }
#endif

    /// <summary>
    /// Writes a formatted line as an ordinal message
    /// </summary>
    /// <param name="formatString">Format string for the message</param>
    /// <param name="args">Arguments for the format string</param>
    public void WriteLineFormat(string formatString, params string[] args)
    {
        Ordinal(formatString, args);
    }

    #region

    /// <summary>
    /// Writes a success message to the output
    /// </summary>
    /// <param name="text">Message text, can contain format placeholders</param>
    /// <param name="args">Format arguments for the text</param>
    public void Success(string text, params string[] args)
    {
        typedWriteLineDelegate.Invoke(TypeOfMessageCl.Success, text, args);
    }

    /// <summary>
    /// Writes an error message to the output
    /// </summary>
    /// <param name="text">Message text, can contain format placeholders</param>
    /// <param name="args">Format arguments for the text</param>
    public void Error(string text, params string[] args)
    {
        typedWriteLineDelegate.Invoke(TypeOfMessageCl.Error, text, args);
    }

    /// <summary>
    /// Writes a warning message to the output
    /// </summary>
    /// <param name="text">Message text, can contain format placeholders</param>
    /// <param name="args">Format arguments for the text</param>
    public void Warning(string text, params string[] args)
    {
        typedWriteLineDelegate.Invoke(TypeOfMessageCl.Warning, text, args);
    }

    /// <summary>
    /// Writes an appeal message to the output
    /// </summary>
    /// <param name="text">Message text, can contain format placeholders</param>
    /// <param name="args">Format arguments for the text</param>
    public void Appeal(string text, params string[] args)
    {
        typedWriteLineDelegate.Invoke(TypeOfMessageCl.Appeal, text, args);
    }

    /// <summary>
    /// Writes an ordinal message to the output
    /// </summary>
    /// <param name="text">Message text, can contain format placeholders</param>
    /// <param name="args">Format arguments for the text</param>
    public void Ordinal(string text, params string[] args)
    {
        typedWriteLineDelegate.Invoke(TypeOfMessageCl.Ordinal, text, args);
    }

    /// <summary>
    /// Writes a message of the specified type by routing to the appropriate typed method
    /// </summary>
    /// <param name="messageType">Type of message determining how it will be displayed</param>
    /// <param name="message">Message text to write</param>
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
    /// Writes an informational message to the output
    /// </summary>
    /// <param name="text">Message text, can contain format placeholders</param>
    /// <param name="args">Format arguments for the text</param>
    public void Information(string text, params string[] args)
    {
        typedWriteLineDelegate.Invoke(TypeOfMessageCl.Information, text, args);
    }

    #endregion
}