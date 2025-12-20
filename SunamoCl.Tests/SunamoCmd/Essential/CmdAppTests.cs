// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
using Microsoft.Extensions.Logging;
using SunamoCl.SunamoCmd.Essential;
using SunamoTest;
using SunamoWinStd;

namespace SunamoCl.Tests.SunamoCmd.Essential;
public class CmdAppTests
{
    ILogger logger = TestLogger.Instance;

    public async Task WaitForSaving()
    {
        await CmdApp.WaitForSaving(logger, @"D:\_Test\PlatformIndependentNuGetPackages\SunamoCl\WaitForSaving.txt", PHWin.Code);
    }
}