// variables names: ok
namespace SunamoCl;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
/// <summary>
/// Console logger and user interaction utilities
/// </summary>
public partial class CL
{
    /// <summary>
    /// Message displayed when waiting for data to be copied to clipboard
    /// </summary>
    public static string PressEnterWhenDataWillBeInClipboard { get; set; } = "📋 Press Enter when data will be copied to clipboard";
    private static volatile bool isExiting;
    private static readonly string charOfHeader = "*";

    /// <summary>
    /// Whether to perform actions or skip them
    /// </summary>
    public static bool Perform { get; set; } = true;

    /// <summary>
    /// Starts a countdown timer
    /// </summary>
    public static void Timer()
    {
        for (var index = 11; index > 0; index--)
        {
            var task = Task.Delay(index * 1000).ContinueWith(_ => WriteTimeLeft());
        }
    }

    /// <summary>
    /// Pokud zadaný soubor / složka neexistují, vrátí ""
    /// </summary>
    /// <param name = "args"></param>
    /// <param name = "shouldTakeSecondIfHaveMoreThanTwoParams"></param>
    /// <returns></returns>
    /// <exception cref = "Exception"></exception>
    public static string WorkingDirectoryFromArgs(string[] args, bool isTakingSecondIfMoreThanTwoParams)
    {
        string workingDirectory = string.Empty;
        // PRVNÍ JE VŽDY MÓD
        if (args.Count() == 1)
        {
            workingDirectory = Environment.CurrentDirectory;
        }
        // mód + argument (např. PushToGitAndNuget {commit_msg}
        // tohle není dobrá ukázka protože commit_msg se zadává až poté.
        // nedošlo mi to a kvůli tohoto toto celé vzniklo
        // pokud chci zadat složku ve které to poběží, pokud aplikace není dělaná "mód složka" musím --RunInDebug ve CommonArgs
        // nechám oba přístupy
        else if (args.Count() == 2)
        {
            if (Directory.Exists(args[1]) || File.Exists(args[1]))
            {
                workingDirectory = args[1];
            }
            else
            {
                workingDirectory = Environment.CurrentDirectory;
            }
        // už není potřeba
        //if (!Directory.Exists(workingDirectory))
        //{
        //    CL.WriteList(args, "args");
        //    throw new Exception("Folder does not exists!");
        //}
        }
        else if (args.Length == 0)
        {
            throw new Exception("Was not entered mode, args is empty");
        }
        else
        {
            if (isTakingSecondIfMoreThanTwoParams)
            {
                if (Directory.Exists(args[1]) || File.Exists(args[1]))
                {
                    workingDirectory = args[1];
                }
                else
                {
                    workingDirectory = Environment.CurrentDirectory;
                }
            }
        // Toto by asi neměl být problém
        //throw new Exception("args.Count have elements " + args.Count());
        }

        return FS.WithEndSlash(workingDirectory);
    }

    /// <summary>
    /// Displays a list of actions for user to select from
    /// </summary>
    /// <param name="actions">Dictionary of action names and their implementations</param>
    /// <param name="appealMessage">Message to display to the user</param>
    public static void SelectFromVariants(Dictionary<string, Action> actions, string appealMessage)
    {
        appealMessage = appealMessage.TrimEnd(':') + ":";
        var index = 0;
        foreach (var actionPair in actions)
        {
            WriteLine($"  [{index:D2}] 📌 {actionPair.Key}");
            index++;
        }

        var enteredValue = UserMustTypeNumber(appealMessage, actions.Count - 1);
        if (enteredValue == -1)
        {
            OperationWasStopped();
            return;
        }

        index = 0;
        string? operationName = null;
        foreach (var actionKey in actions.Keys)
        {
            if (index == enteredValue)
            {
                operationName = actionKey;
                break;
            }

            index++;
        }

        var selectedAction = actions[operationName!];
        selectedAction.Invoke();
    }

    private static void OperationWasStopped()
    {
        WriteLine("❌ Operation was cancelled.");
    }

    /// <summary>
    ///     Will ask before getting data
    ///     First I must ask which is always from console - must prepare user to load data to clipboard.
    /// </summary>
    /// <param name = "what"></param>
    public static string LoadFromClipboardOrConsole(string what)
    {
        var inputData = @"";
        // Display formatted prompt with icons
        Console.WriteLine();
        Console.WriteLine($"╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  📥 Input Required: {what.PadRight(33)} ║");
        Console.WriteLine($"╠═══════════════════════════════════════════════════════╣");
        Console.WriteLine($"║  Options:                                             ║");
        Console.WriteLine($"║  • 📋 Copy data to clipboard, then press Enter       ║");
        Console.WriteLine($"║  • ⌨️  Type directly in console                       ║");
        Console.WriteLine($"║  • ❌ Press ESC to cancel                             ║");
        Console.WriteLine($"╚═══════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.Write($"⏳ Waiting for clipboard data... ");
        ReadLine();
        inputData = ClipboardService.GetText();
        if (string.IsNullOrWhiteSpace(inputData))
        {
            Console.WriteLine();
            Console.WriteLine($"⚠️  Clipboard is empty or contains only whitespace");
            Console.Write($"✏️  Please type {what} manually: ");
            inputData = CL.UserMustType(what, "");
        }
        else
        {
            Console.WriteLine($"✅ Data loaded from clipboard");
        }

        return inputData;
    }

    /// <summary>
    /// Prompts user for a folder path, using debug value if in debug mode
    /// </summary>
    /// <param name="folderDebug">Folder path to use in debug mode</param>
    /// <param name="isDebug">Whether application is running in debug mode</param>
    /// <returns>Selected folder path</returns>
    public static string AskForFolder(string folderDebug, bool isDebug)
    {
        string? folder = null;
        if (isDebug)
            folder = folderDebug;
        else
            folder = LoadFromClipboardOrConsole("folder");
        return folder!;
    }

    /// <summary>
    /// Prompts user for folder, mask and recursion settings, then returns matching files
    /// </summary>
    /// <param name="folderDebug">Folder path to use in debug mode</param>
    /// <param name="maskDebug">File mask to use in debug mode</param>
    /// <param name="isRecursiveDebug">Whether to search recursively in debug mode</param>
    /// <param name="isDebug">Whether application is running in debug mode</param>
    /// <returns>List of file paths matching the criteria</returns>
    public static List<string> AskForFolderMascRecFiles(string folderDebug, string maskDebug, bool isRecursiveDebug, bool isDebug)
    {
        var(folder, mask, isRecursive) = AskForFolderMascRec(folderDebug, maskDebug, isRecursiveDebug, isDebug);
        return Directory.GetFiles(folder, mask, isRecursive.Value ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList();
    }

    /// <summary>
    /// Prompts user for folder, file mask and recursion settings
    /// </summary>
    /// <param name="folderDebug">Folder path to use in debug mode</param>
    /// <param name="maskDebug">File mask to use in debug mode</param>
    /// <param name="isRecursiveDebug">Whether to search recursively in debug mode</param>
    /// <param name="isDebug">Whether application is running in debug mode</param>
    /// <returns>Tuple containing folder path, file mask and recursion flag</returns>
    public static (string folder, string mask, bool? isRecursive) AskForFolderMascRec(string folderDebug, string maskDebug, bool? isRecursiveDebug, bool isDebug)
    {
        string? folder = null;
        string? mask = null;
        bool? isRecursive = false;
        if (isDebug)
        {
            folder = folderDebug;
            mask = maskDebug;
            isRecursive = isRecursiveDebug;
        }
        else
        {
            folder = LoadFromClipboardOrConsole("folder");
            mask = UserMustType("mask");
            isRecursiveDebug = UserMustTypeYesNo("recursive");
        }

        return (folder!, mask!, isRecursive);
    }

    /// <summary>
    /// Waits for user to press Enter to continue
    /// </summary>
    public static void PressEnterToContinue2()
    {
        using (var standardInput = Console.OpenStandardInput())
        using (var streamReader = new StreamReader(standardInput))
        {
            Task readLineTask = streamReader.ReadLineAsync();
            Debug.WriteLine("hi");
            Console.WriteLine("✅ Process started successfully");
            readLineTask.Wait(); // When not in Main method, you can use await. 
        // Waiting must happen in the curly brackets of the using directive.
        }

        Console.WriteLine("👋 Goodbye!");
    }

    public static void PressEnterToContinue3()
    {
        Task.Factory.StartNew(() =>
        {
            while (Console.ReadKey().Key != ConsoleKey.Q)
                ;
            isExiting = true;
        });
        while (!isExiting)
        {
        // Do stuff
        }
    }

    /// <summary>
    ///     Return printed text
    /// </summary>
    /// <param name = "text"></param>
    public static string StartRunTime(string text)
    {
        var textLength = text.Length;
        var stars = "";
        stars = new string (charOfHeader[0], textLength);
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine(stars);
        stringBuilder.AppendLine(text);
        stringBuilder.AppendLine(stars);
        var result = stringBuilder.ToString();
        Information(result);
        return result;
    }

    /// <summary>
    ///     Print and wait
    /// </summary>
    public static void EndRunTime(bool isAttemptingToRepairError = false)
    {
        if (isAttemptingToRepairError)
            Information(Messages.RepairErrors);
        Information(Messages.AppWillBeTerminated);
        Console.ReadLine();
    }

    /// <summary>
    ///     Return full path of selected file
    ///     or null when operation will be stopped
    /// </summary>
    /// <param name = "folder"></param>
    public static string? SelectFile(string folder)
    {
        var files = Directory.GetFiles(folder).ToList();
        var outputPath = "";
        var selectedFile = SelectFromVariants(files, "file which you want to open");
        if (selectedFile == -1)
            return null;
        outputPath = files[selectedFile];
        return outputPath;
    }
}