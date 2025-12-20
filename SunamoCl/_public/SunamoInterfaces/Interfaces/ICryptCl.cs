// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoCl._public.SunamoInterfaces.Interfaces;

public interface ICryptCl
{
    List<byte> S { set; get; }
    List<byte> Iv { set; get; }
    string Pp { set; get; }
}