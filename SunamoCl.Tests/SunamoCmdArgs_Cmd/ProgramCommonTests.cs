
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

public class ProgramCommonTests
{
    [Fact]
    public void ProcessArgsTest()
    {
        ProgramCommon p = new ProgramCommon();
        var a = p.ProcessArgs<CommonArgsTest, Mode>(["--Mode", "Test"], Mode.None);
    }
}
