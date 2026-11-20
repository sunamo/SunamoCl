namespace SunamoCl._public.SunamoInterfaces.Interfaces;

/// <summary>
/// Represents the i crypt cl type.
/// </summary>
public interface ICryptCl
{
    /// <summary>
    /// Gets or sets the s.
    /// </summary>
    List<byte> S { set; get; }
    /// <summary>
    /// Gets or sets the iv.
    /// </summary>
    List<byte> Iv { set; get; }
    /// <summary>
    /// Gets or sets the pp.
    /// </summary>
    string Pp { set; get; }
}
