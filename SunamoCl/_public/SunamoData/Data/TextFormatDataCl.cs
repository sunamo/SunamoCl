// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoCl._public.SunamoData.Data;

public class TextFormatDataCl : List<CharFormatDataCl>
{
    public int RequiredLength = -1;
    public bool TrimBefore;


    public TextFormatDataCl(bool trimBefore, int requiredLength, params CharFormatDataCl[] a)
    {
        this.TrimBefore = trimBefore;
        this.RequiredLength = requiredLength;
        AddRange(a);
    }

    public static class Templates
    {
    }
}