namespace SunamoCl;

public partial class CL
{
    public static string PressEnterWhenDataWillBeInClipboard { get; set; } = "📋 Press Enter when data will be copied to clipboard";
    private static volatile bool isExiting;
    private static readonly string charOfHeader = "*";

    public static bool ShouldPerform { get; set; } = true;

    public static void Timer()
    {
        for (var index = 11; index > 0; index--)
        {
            Task.Delay(index * 1000).ContinueWith(_ => WriteTimeLeft());
        }
    }

    public static string WorkingDirectoryFromArgs(string[] args, bool isTakingSecondIfMoreThanTwoParams)
    {
        var workingDirectory = string.Empty;
        // First argument is always the mode
        if (args.Length == 1)
        {
            workingDirectory = Environment.CurrentDirectory;
        }
        // Mode + argument (e.g. PushToGitAndNuget {commit_msg})
        // If folder needs to be specified and app is not designed as "mode folder", use --RunInDebug in CommonArgs
        else if (args.Length == 2)
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

    public static string LoadFromClipboardOrConsole(string text)
    {
        string inputData;
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

    public static string AskForFolder(string folderDebug, bool isDebug)
    {
        var folder = isDebug ? folderDebug : LoadFromClipboardOrConsole("folder");
        return folder;
    }

    public static List<string> AskForFolderMaskRecFiles(string folderDebug, string maskDebug, bool isRecursiveDebug, bool isDebug)
    {
        var(folder, mask, isRecursive) = AskForFolderMaskRec(folderDebug, maskDebug, isRecursiveDebug, isDebug);
        return Directory.GetFiles(folder, mask, isRecursive.GetValueOrDefault() ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList();
    }

    public static (string folder, string mask, bool? isRecursive) AskForFolderMaskRec(string folderDebug, string maskDebug, bool? isRecursiveDebug, bool isDebug)
    {
        string folder;
        string mask;
        bool? isRecursive;
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
            isRecursive = UserMustTypeYesNo("recursive");
        }

        return (folder, mask, isRecursive);
    }

    public static void PressEnterToContinue2()
    {
        using (var standardInput = Console.OpenStandardInput())
        using (var streamReader = new StreamReader(standardInput))
        {
            Task readLineTask = streamReader.ReadLineAsync();
            Console.WriteLine("✅ Process started successfully");
            readLineTask.Wait();
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
        }
    }

    public static string StartRunTime(string text)
    {
        var textLength = text.Length;
        var stars = new string(charOfHeader[0], textLength);
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine(stars);
        stringBuilder.AppendLine(text);
        stringBuilder.AppendLine(stars);
        var result = stringBuilder.ToString();
        Information(result);
        return result;
    }

    public static void EndRunTime(bool isAttemptingToRepairError = false)
    {
        if (isAttemptingToRepairError)
            Information(Messages.RepairErrors);
        Information(Messages.AppWillBeTerminated);
        Console.ReadLine();
    }

    public static string? SelectFile(string folder)
    {
        var files = Directory.GetFiles(folder).ToList();
        var selectedFile = SelectFromVariants(files, "file which you want to open");
        if (selectedFile == -1)
            return null;
        return files[selectedFile];
    }
}