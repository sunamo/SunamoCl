namespace SunamoCl._public.SunamoEnums.Enums;

/// <summary>
/// Defines the type of from-to value representation used in range operations
/// </summary>
public enum FromToUseCl
{
    /// <summary>
    /// Values are represented as DateTime
    /// </summary>
    DateTime,
    /// <summary>
    /// Values are represented as Unix timestamps
    /// </summary>
    Unix,
    /// <summary>
    /// Values are represented as Unix timestamps containing only time portion
    /// </summary>
    UnixJustTime,
    /// <summary>
    /// No specific representation, used for plain numeric ranges
    /// </summary>
    None
}