// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// Instance variables refactored according to C# conventions

namespace RunnerCl;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShellProgressBar;
using SunamoCl;
using SunamoCl.SunamoCmd;
using SunamoCl.SunamoCmd.Args;
using SunamoCl.SunamoCmdArgs_Cmd;
using System;
using System.Threading.Tasks;
using TextCopy;

internal partial class Program
{
    static ProgramCommon programCommon;
    const string appName = "RunnerCl";

    static IServiceCollection Services { get; set; }
    static ServiceProvider Provider { get; set; }
    static ILogger logger { get; set; }

    static Program()
    {
        programCommon = new ProgramCommon();

        Services = new ServiceCollection();

        Services.AddScoped<TestContainer>();

        CmdBootStrap.AddILogger(Services, true, null, appName);
        CmdBootStrap.AddIConfiguration(Services);

        Provider = Services.BuildServiceProvider();
    }



    static void Main()
    {
        MainAsync().GetAwaiter().GetResult();
    }

    static async Task MainAsync()
    {
        //ProgramCommonTests t = new ProgramCommonTests();
        //t.ProcessArgsTest();

        // můžu přidat přímo do dict ve ProgramCommon protože ProgramCommon.AddToAllActions přidává právě do těchto 2 dict

        await CmdBootStrap.RunWithRunArgs(new RunArgs()
        {
            ServiceCollection = Services,
            AddGroupOfActions = AddGroupOfActions,
            //AddGroupOfActions = CommandsToAllCsFiles.Cmd.Program.AddGroupOfActions,
            RunInDebugAsync = RunInDebugAsync,
            Args =
#if DEBUG
            //["TestTest"],
            [],
#else
args,
#endif


            IsDebug =
#if DEBUG
            true
#else
false
#endif
        });

        CL.WriteLine("🎯 Task completed successfully!");
        Console.ReadLine();
    }

    static async Task RunInDebugAsync()
    {
        await Task.Delay(1);

        CL.AppealWithCountdown("Spustit testy za", 3);
        Console.WriteLine("Finished");

        //await ClNotify.FlashConsoleTitle(logger, "Akce vyžadována!"); // Blikání titulu 5x



        //ClipboardService.SetText("");

        //var entered = CL.LoadFromClipboardOrConsole("něco");
        //Console.WriteLine($"📥 Received input: {entered}");

        //TestProgressBar();

        //CL.WriteLine("RunInDebugAsync");

        //CmdAppTests t = new CmdAppTests();
        //await t.WaitForSaving();

        //LoggingInSerie();

        Console.WriteLine("🧪 Running test suite...");

        //var tc = Provider.GetRequiredService<TestContainer>();
        //tc.A();

        //var options = new ProgressBarOptions
        //{
        //    ProgressCharacter = '─',
        //    ProgressBarOnBottom = true,
        //    CollapseWhenFinished = false,
        //    DisplayTimeInRealTime = false
        //};

        //CLProgressBarWithChilds pb = new CLProgressBarWithChilds();


        //RunFor10("First", options, pb);
        //RunFor10("Second", options, pb);
    }



    private static void TestProgressBar()
    {
        Console.WriteLine("🔄 Starting progress bar test...");
        Console.WriteLine("📦 Initializing components...");
        CLProgressBar progressBar = new();
        progressBar.Start(10, "Message PB", new());

        for (int i = 0; i < 5; i++)
        {
            progressBar.DoneOne();
            Thread.Sleep(500);
        }

        progressBar.Done();

        Console.WriteLine("✅ First progress bar completed");
        Console.WriteLine("🔄 Starting second progress bar...");

        progressBar = new();
        progressBar.Start(10, "Message PB", new());

        for (int i = 0; i < 5; i++)
        {
            progressBar.DoneOne();
            Thread.Sleep(500);
        }

        progressBar.Done();

        Console.WriteLine("✅ Second progress bar completed");
        Console.WriteLine("🏁 All tests finished successfully!");
    }

    private static void RunFor10(string message, ProgressBarOptions options, CLProgressBarWithChilds progressBarWithChilds)
    {
        progressBarWithChilds.Start(10, message, options);

        for (int i = 0; i < 10; i++)
        {
            progressBarWithChilds.DoneOne(message);
            Thread.Sleep(100);
        }

        progressBarWithChilds.Done();
    }

    private static void ProgressBarTesting()
    {
        #region ProgressBar testing
        //var cl = new CLProgressBar();

        //Console.WriteLine("Before progress bar");
        //cl.Start(10);

        //for (int i = 0; i < 10; i++)
        //{
        //    cl.DoneOne();
        //    await Task.Delay(1000);
        //}

        //cl.Done(); 
        #endregion
    }

    private static void LoggingInSerie()
    {
        #region Logging test
        var serviceProvider = Services.BuildServiceProvider();

        #region Tohle mi nefunguje. Nejsem schopen aby se mi vždy vypsali všechny 3 a teprve pak "Finished"
        /*
Nepomohlo ani aby RunInDebug vracelo string který potom dále použiji
        Občas se zbylé 2 vypíšou až po Finished
        ale to bude kódem samotného loggeru
        V mém kódu to fakt není, všude kde má být await tak tam je
        nefungovalo to ani bez Task.Run
        */

        //await Task.Run(() =>
        //{
        //    var logger = s.GetRequiredService<ILogger>();


        //    logger.LogTrace("From main trace");
        //    logger.LogDebug("From main debug");
        //    logger.LogInformation("Info");

        //    logger.LogWarning("Warning");
        //    logger.LogError("From main error");
        //    logger.LogCritical("Critical");
        //});

        //Toto naopak funguje bezchybně:
        Console.WriteLine("🔹 Test phase 1 completed");
        Console.WriteLine("🔹 Test phase 2 completed");
        Console.WriteLine("🔹 Test phase 3 completed");
        Console.WriteLine("🔹 Test phase 4 completed");
        #endregion
        #endregion
    }
}