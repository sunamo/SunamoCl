// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoCl.Tests.SunamoCmdArgs_Cmd;
using SunamoCl.SunamoCmdArgs.Data;
using SunamoCl.SunamoCmdArgs_Cmd;
using SunamoCl.Tests._sunamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

enum Mode
{
    None,
    Test
}

class CommonArgsTest : CommonArgs
{

}

/// <summary>
/// Tests for ProgramCommon command-line argument parsing functionality
/// </summary>
public class ProgramCommonTests
{
    /// <summary>
    /// Tests parsing command-line arguments with Mode parameter into strongly typed result
    /// </summary>
    [Fact]
    public void ProcessArgsTest()
    {
        ProgramCommon programCommon = new ProgramCommon();
        var result = programCommon.ProcessArgs<CommonArgsTest, Mode>(["--Mode", "Test"], Mode.None);
    }
}
