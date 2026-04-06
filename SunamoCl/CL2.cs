namespace SunamoCl;

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
    public static bool ShouldPerform { get; set; } = true;

    /// <summary>
    /// Starts a countdown timer
    /// </summary>
    public static void Timer()
    {
        for (var index = 11; index > 0; index--)
        {
            Task.Delay(index * 1000).ContinueWith(_ => WriteTimeLeft());
        }
    }

    /// <summary>
    /// Determines the working directory from command-line arguments. Returns empty string if path does not exist.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    /// <param name="isTakingSecondIfMoreThanTwoParams">Whether to use the second argument as working directory when more than two parameters are provided.</param>
    /// <returns>Working directory path with trailing slash.</returns>
    /// <exception cref="Exception">Thrown when no mode argument is provided.</exception>
    public static string WorkingDirectoryFromArgs(string[] args, bool isTakingSecondIfMoreThanTwoParams)
    {
        string workingDirectory = string.Empty;
        // First argument is always the mode
        if (args.Count() == 1)
        {
            workingDirectory = Environment.CurrentDirectory;
        }
        // Mode + argument (e.g. PushToGitAndNuget {commit_msg})
        // If folder needs to be specified and app is not designed as "mode folder", use --RunInDebug in CommonArgs
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
    /// Asks user to provide data either via clipboard or manual console input.
    /// </summary>
    /// <param name="text">Description of the expected data.</param>
    public static string LoadFromClipboardOrConsole(string text)
    {
        var inputData = @"";
        // Display formatted prompt with icons
        Console.WriteLine();
        Console.WriteLine($"╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  📥 Input Required: {text.PadRight(33)} ║");
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
            Console.Write($"✏️  Please type {text} manually: ");
            inputData = CL.UserMustType(text, "");
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
        return Directory.GetFiles(folder, mask, isRecursive.GetValueOrDefault() ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList();
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

    /// <summary>
    /// Waits for user to press Q key to exit, blocking the main thread
    /// </summary>
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
    /// Displays a startup header with the given text wrapped in star characters.
    /// </summary>
    /// <param name="text">Header text to display.</param>
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
    /// Displays termination message and waits for user to press Enter.
    /// </summary>
    public static void EndRunTime(bool isAttemptingToRepairError = false)
    {
        if (isAttemptingToRepairError)
            Information(Messages.RepairErrors);
        Information(Messages.AppWillBeTerminated);
        Console.ReadLine();
    }

    /// <summary>
    /// Returns the full path of the selected file, or null when the operation is cancelled.
    /// </summary>
    /// <param name="folder">Folder to list files from.</param>
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