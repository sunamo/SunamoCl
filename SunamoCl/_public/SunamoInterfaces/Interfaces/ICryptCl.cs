// variables names: ok
namespace SunamoCl._public.SunamoInterfaces.Interfaces;

/// <summary>
/// Interface for cryptographic operations providing salt, initialization vector, and passphrase
/// </summary>
public interface ICryptCl
{
    /// <summary>
    /// Gets or sets the salt bytes used for cryptographic operations
    /// </summary>
    List<byte> S { set; get; }
    /// <summary>
    /// Gets or sets the initialization vector bytes
    /// </summary>
    List<byte> Iv { set; get; }
    /// <summary>
    /// Gets or sets the passphrase used for encryption and decryption
    /// </summary>
    string Pp { set; get; }
}
