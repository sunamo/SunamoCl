namespace SunamoCl;
//namespace SunamoCl.SunamoCmd.Essential;


internal class TypedConsoleLogger : TypedLoggerBase
{
    internal static TypedConsoleLogger Instance = new TypedConsoleLogger();

    private TypedConsoleLogger() : base(CL.ChangeColorOfConsoleAndWrite)
    {

    }


}
