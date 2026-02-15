namespace SunamoCl._public.SunamoEnums.Enums;

/// <summary>
/// Defines the types of messages that can be displayed in the console with different colors
/// </summary>
public enum TypeOfMessageCl
{
    /// <summary>
    /// Error message, typically displayed in red
    /// </summary>
    Error,


    /// <summary>
    /// Warning message, typically displayed in yellow
    /// </summary>
    Warning,
    /// <summary>
    /// Informational message, typically displayed in white
    /// </summary>
    Information,


    /// <summary>
    /// Ordinal (standard) message, typically displayed in default color
    /// </summary>
    Ordinal,
    /// <summary>
    /// Appeal message requesting user attention, typically displayed in magenta
    /// </summary>
    Appeal,
    /// <summary>
    /// Success message, typically displayed in green
    /// </summary>
    Success
}