// variables names: ok

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
    static ProgramCommon programCommon = null!;
    const string appName = "RunnerCl";

    static IServiceCollection services { get; set; } = null!;
    static ServiceProvider provider { get; set; } = null!;
    static ILogger logger { get; set; } = null!;

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

        // Can add directly to dict in ProgramCommon because ProgramCommon.AddToAllActions adds to these 2 dicts

        await CmdBootStrap.RunWithRunArgs(new RunArgs()
        {
            ServiceCollection = services,
            AddGroupOfActions = AddGroupOfActions,
            RunInDebugAsync = RunInDebugAsync,
            Args = args,
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







        Console.WriteLine("🧪 Running test suite...");






        // Call all public methods from CL class
        await TestAllPublicMethods();
    }

    static async Task TestAllPublicMethods()
    {
        Console.WriteLine();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("  Testing all public CL methods");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();

        // Basic write methods
        CL.WriteLine("Test WriteLine(string)");
        CL.WriteLine(42);
        CL.WriteLine();
        CL.Write("Test Write ");
        CL.Write('X');
        CL.WriteLine();
        CL.WriteLineObject("Test WriteLineObject");
        CL.Write("{0}: {1}", "Key", "Value");
        CL.WriteLine();
        CL.Log("Test Log with {0} params", "formatted");
        CL.WriteLine("Test WriteLine with {0} and {1}", "param1", "param2");

        // Color methods
        CL.WriteLineWithColor(ConsoleColor.Green, "Test WriteLineWithColor");
        CL.WriteColor(TypeOfMessageCl.Success, "Test WriteColor");
        CL.Error("Test Error message");
        CL.Warning("Test Warning message");
        CL.Information("Test Information message");
        CL.Success("Test Success message");
        CL.Appeal("Test Appeal message");

        // List and formatting methods
        var testList = new List<string> { "Item 1", "Item 2", "Item 3" };
        CL.WriteList(testList, "Test WriteList");
        CL.WriteLineFormat("Test WriteLineFormat: {0} {1}", "arg1", "arg2");
        CL.Pair("TestKey", "TestValue");

        // Table output
        var tableData = new List<List<string>>
        {
            new List<string> { "Col1", "Col2", "Col3" },
            new List<string> { "A", "B", "C" },
            new List<string> { "1", "2", "3" }
        };
        CL.CmdTable(tableData);

        // StartRunTime and EndRunTime
        var runtimeText = CL.StartRunTime("Test StartRunTime");
        Console.WriteLine("Runtime text returned: " + runtimeText);

        // Clear console methods
        // CL.Clear(); // Commented out to keep output visible
        CL.ResetColor();

        // Console properties
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

        // WorkingDirectoryFromArgs
        var workingDir = CL.WorkingDirectoryFromArgs(new[] { "mode", Environment.CurrentDirectory }, false);
        Console.WriteLine($"Working directory: {workingDir}");

        var folder = CL.AskForFolder(Environment.CurrentDirectory, true);
        Console.WriteLine($"Folder from AskForFolder: {folder}");

        // AskForFolderMaskRec
        var (testFolder, masc, rec) = CL.AskForFolderMaskRec(Environment.CurrentDirectory, "*.cs", true, true);
        Console.WriteLine($"Folder: {testFolder}, Mask: {masc}, Recursive: {rec}");

        // AskForEnter
        var enterMessage = CL.AskForEnter("test data", true, null);
        Console.WriteLine($"AskForEnter result: {enterMessage}");

        // SelectFromVariants with Dictionary (non-interactive test)
        Console.WriteLine("SelectFromVariants example (not executed to avoid blocking)");

        // AppealWithCountdown
        CL.AppealWithCountdown("Test countdown", 2);

        // NoData
        CL.NoData();

        // Interactive methods commented out to avoid blocking


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



        #endregion
    }

    private static void LoggingInSerie()
    {
        #region Logging test
        var serviceProvider = services.BuildServiceProvider();

        #region Logger ordering issue - all 3 logs sometimes print after "Finished" due to logger internal buffering, not missing awaits
        /*
        Returning string from RunInDebug did not help either.
        Sometimes the remaining 2 lines print after "Finished".
        This is caused by the logger itself, not the application code — all awaits are in place.
        Did not work even without Task.Run.
        */

        //await Task.Run(() =>




        // This on the other hand works flawlessly:
        Console.WriteLine("🔹 Test phase 1 completed");
        Console.WriteLine("🔹 Test phase 2 completed");
        Console.WriteLine("🔹 Test phase 3 completed");
        Console.WriteLine("🔹 Test phase 4 completed");
        #endregion
        #endregion
    }
}
