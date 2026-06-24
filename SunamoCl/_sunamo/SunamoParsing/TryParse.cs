namespace SunamoCl._sunamo.SunamoParsing;

internal class TryParse
{
    internal class Integer
    {
        internal static Integer Instance { get; set; } = new();
        internal int LastInt { get; set; } = -1;
    }
}
