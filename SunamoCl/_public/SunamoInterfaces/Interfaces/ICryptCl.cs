// variables names: ok
namespace SunamoCl._public.SunamoInterfaces.Interfaces;

public interface ICryptCl
{
    List<byte> S { set; get; }
    List<byte> Iv { set; get; }
    string Pp { set; get; }
}
