// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace RunnerCl;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShellProgressBar;
using SunamoCl;
using SunamoCl._public.SunamoEnums.Enums;
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

    static IServiceCollection services { get; set; }
    static ServiceProvider provider { get; set; }
    static ILogger logger { get; set; }

    static Program()
    {
        programCommon = new ProgramCommon();

        services = new ServiceCollection();

        services.AddScoped<TestContainer>();

        CmdBootStrap.AddILogger(services, true, null, appName);
        CmdBootStrap.AddIConfiguration(services);

        provider = services.BuildServiceProvider();
    }



    static void Main(String[] args)
    {
        MainAsync(args).GetAwaiter().GetResult();
    }

    static async Task MainAsync(String[] args)
    {
        //ProgramCommonTests t = new ProgramCommonTests();
        //t.ProcessArgsTest();

        // můžu přidat přímo do dict ve ProgramCommon protože ProgramCommon.AddToAllActions přidává právě do těchto 2 dict

        await CmdBootStrap.RunWithRunArgs(new RunArgs()
        {
            ServiceCollection = services,
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

        ConsoleLoggerCmd.Instance.WriteLine($"t");

        //ClipboardService.SetText("");

        //var entered = CL.LoadFromClipboardOrConsole("něco");
        //Console.WriteLine($"📥 Received input: {entered}");

        //TestProgressBar();

        //CL.WriteLine("RunInDebugAsync");

        //CmdAppTests t = new CmdAppTests();
        //await t.WaitForSaving();

        //LoggingInSerie();

        Console.WriteLine("🧪 Running test suite...");

        //var tc = provider.GetRequiredService<TestContainer>();
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

        // EN: Call all public methods from CL class
        // CZ: Zavolat všechny veřejné metody z třídy CL
        await TestAllPublicMethods();
    }

    /// <summary>
    /// EN: Tests all public methods from CL class
    /// CZ: Testuje všechny veřejné metody z třídy CL
    /// </summary>
    static async Task TestAllPublicMethods()
    {
        Console.WriteLine();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("  Testing all public CL methods");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();

        // EN: Basic write methods
        // CZ: Základní metody pro výpis
        CL.WriteLine("Test WriteLine(string)");
        CL.WriteLine(42);
        CL.WriteLine();
        CL.Write("Test Write ");
        CL.Write('X');
        CL.WriteLine();
        CL.WriteLineO("Test WriteLineO");
        CL.Write("{0}: {1}", "Key", "Value");
        CL.WriteLine();
        CL.Log("Test Log with {0} params", "formatted");
        CL.WriteLine("Test WriteLine with {0} and {1}", "param1", "param2");

        // EN: Color methods
        // CZ: Metody s barvami
        CL.WriteLineWithColor(ConsoleColor.Green, "Test WriteLineWithColor");
        CL.WriteColor(TypeOfMessageCl.Success, "Test WriteColor");
        CL.Error("Test Error message");
        CL.Warning("Test Warning message");
        CL.Information("Test Information message");
        CL.Success("Test Success message");
        CL.Appeal("Test Appeal message");

        // EN: List and formatting methods
        // CZ: Metody pro seznamy a formátování
        var testList = new List<string> { "Item 1", "Item 2", "Item 3" };
        CL.WriteList(testList, "Test WriteList");
        CL.WriteLineFormat("Test WriteLineFormat: {0} {1}", "arg1", "arg2");
        CL.Pair("TestKey", "TestValue");

        // EN: Table output
        // CZ: Tabulkový výstup
        var tableData = new List<List<string>>
        {
            new List<string> { "Col1", "Col2", "Col3" },
            new List<string> { "A", "B", "C" },
            new List<string> { "1", "2", "3" }
        };
        CL.CmdTable(tableData);

        // EN: StartRunTime and EndRunTime
        // CZ: StartRunTime a EndRunTime
        var runtimeText = CL.StartRunTime("Test StartRunTime");
        Console.WriteLine("Runtime text returned: " + runtimeText);

        // EN: Clear console methods
        // CZ: Metody pro čištění konzole
        // CL.Clear(); // Commented out to keep output visible
        CL.ResetColor();

        // EN: Console properties
        // CZ: Vlastnosti konzole
        try
        {
            Console.WriteLine($"CursorTop: {CL.CursorTop}");
            Console.WriteLine($"WindowWidth: {CL.WindowWidth}");
            Console.WriteLine($"CursorLeft: {CL.CursorLeft}");
            Console.WriteLine($"BufferWidth: {CL.BufferWidth}");
            Console.WriteLine($"WindowHeight: {CL.WindowHeight}");
        }
        catch (IOException)
        {
            Console.WriteLine("Console properties not available in non-interactive mode");
        }

        // EN: WorkingDirectoryFromArgs
        // CZ: WorkingDirectoryFromArgs
        var workingDir = CL.WorkingDirectoryFromArgs(new[] { "mode", Environment.CurrentDirectory }, false);
        Console.WriteLine($"Working directory: {workingDir}");

        // EN: AskForFolder (with debug mode to avoid user input)
        // CZ: AskForFolder (s debug módem aby se nevyžadoval uživatelský vstup)
        var folder = CL.AskForFolder(Environment.CurrentDirectory, true);
        Console.WriteLine($"Folder from AskForFolder: {folder}");

        // EN: AskForFolderMascRec
        // CZ: AskForFolderMascRec
        var (testFolder, masc, rec) = CL.AskForFolderMascRec(Environment.CurrentDirectory, "*.cs", true, true);
        Console.WriteLine($"Folder: {testFolder}, Mask: {masc}, Recursive: {rec}");

        // EN: AskForEnter
        // CZ: AskForEnter
        var enterMessage = CL.AskForEnter("test data", true, null);
        Console.WriteLine($"AskForEnter result: {enterMessage}");

        // EN: SelectFromVariants with Dictionary (non-interactive test)
        // CZ: SelectFromVariants se slovníkem (neinteraktivní test)
        Console.WriteLine("SelectFromVariants example (not executed to avoid blocking)");

        // EN: AppealWithCountdown
        // CZ: AppealWithCountdown
        CL.AppealWithCountdown("Test countdown", 2);

        // EN: NoData
        // CZ: NoData
        CL.NoData();

        // Interactive methods commented out to avoid blocking
        // Interaktivní metody zakomentovány aby nedošlo k blokování

        // CL.PressAnyKeyToContinue();
        // CL.PressEnterToContinue2();
        // CL.PressEnterToContinue3();
        // var dialogResult = CL.DoYouWantToContinue("test");
        // var entered = CL.UserMustType("test");
        // var yesNo = CL.UserMustTypeYesNo("test");
        // var number = CL.UserMustTypeNumber("test", 10);
        // var multiLine = CL.UserMustTypeMultiLine("test");
        // var fromClipboard = CL.LoadFromClipboardOrConsole("test");
        // var selectedFile = CL.SelectFile(Environment.CurrentDirectory);
        // var selectedIndex = CL.SelectFromVariants(testList, "test");
        // var selectedString = CL.SelectFromVariantsString(testList, "test");

        Console.WriteLine();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("  All public method tests completed");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();

        await Task.CompletedTask;
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
        var serviceProvider = services.BuildServiceProvider();

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