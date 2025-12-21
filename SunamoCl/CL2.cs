namespace SunamoCl;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public partial class CL
{
    public static string xPressEnterWhenDataWillBeInClipboard = "📋 Press Enter when data will be copied to clipboard";
    private static volatile bool exit;
    private static readonly string charOfHeader = "*";
    public static bool perform = true;
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
    /// <param name = "takeSecondIfHaveMoreThanTwoParams"></param>
    /// <returns></returns>
    /// <exception cref = "Exception"></exception>
    public static string WorkingDirectoryFromArgs(string[] args, bool takeSecondIfHaveMoreThanTwoParams)
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
            if (takeSecondIfHaveMoreThanTwoParams)
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

    public static void SelectFromVariants(Dictionary<string, Action> actions, string xSelectAction)
    {
        var appealMessage = xSelectAction + ":";
        var index = 0;
        foreach (var kvp in actions)
        {
            WriteLine($"  [{index:D2}] 📌 {kvp.Key}");
            index++;
        }

        var enteredValue = UserMustTypeNumber(appealMessage, actions.Count - 1);
        if (enteredValue == -1)
        {
            OperationWasStopped();
            return;
        }

        index = 0;
        string operationName = null;
        foreach (var actionKey in actions.Keys)
        {
            if (index == enteredValue)
            {
                operationName = actionKey;
                break;
            }

            index++;
        }

        var selectedAction = actions[operationName];
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

    public static string AskForFolder(string folderDbg, bool isDebug)
    {
        string folder = null;
        if (isDebug)
            folder = folderDbg;
        else
            folder = LoadFromClipboardOrConsole("folder");
        return folder;
    }

    public static List<string> AskForFolderMascRecFiles(string folderDbg, string mascDbg, bool recDbg, bool isDebug)
    {
        var(folder, masc, rec) = AskForFolderMascRec(folderDbg, mascDbg, recDbg, isDebug);
        return Directory.GetFiles(folder, masc, rec.Value ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList();
    }

    public static (string folder, string masc, bool? rec) AskForFolderMascRec(string folderDbg, string mascDbg, bool? recDbg, bool isDebug)
    {
        string folder = null;
        string masc = null;
        bool? rec = false;
        if (isDebug)
        {
            folder = folderDbg;
            masc = mascDbg;
            rec = recDbg;
        }
        else
        {
            folder = LoadFromClipboardOrConsole("folder");
            masc = UserMustType("masc");
            recDbg = UserMustTypeYesNo("recursive");
        }

        return (folder, masc, rec);
    }

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
            exit = true;
        });
        while (!exit)
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
    public static void EndRunTime(bool attempToRepairError = false)
    {
        if (attempToRepairError)
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
        var filesList = Directory.GetFiles(folder).ToList();
        var outputPath = "";
        var selectedFile = SelectFromVariants(filesList, "file which you want to open");
        if (selectedFile == -1)
            return null;
        outputPath = filesList[selectedFile];
        return outputPath;
    }
}