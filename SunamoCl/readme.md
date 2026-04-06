# SunamoCl

Console UI utility library for .NET command-line applications. Provides colored output, progress bars, user input prompts, action selection menus, countdown timers, table formatting, and application bootstrapping with dependency injection.

## Features

- **Colored console output** — write messages with color-coded severity (Error, Warning, Success, Information, Appeal)
- **User input** — type-safe prompts with validation, multi-line input, yes/no dialogs, number input
- **Action menus** — group and select actions from dictionaries, with async support
- **Progress bars** — single and parent/child progress bars via ShellProgressBar
- **Table formatting** — render data as formatted ASCII tables
- **Countdown timer** — visual countdown with appeal messages
- **Console flashing** — flash the console window title bar (Windows P/Invoke)
- **Clipboard integration** — load input from clipboard via TextCopy
- **Application bootstrapping** — `CmdBootStrap.RunWithRunArgs` for DI, logging, unhandled exceptions, and action routing
- **Console log mirroring** — tee all console output to a file for AI/automation tools to read
- **Verbose logging mode** — opt-in detailed logging of all application steps
- **Command-line argument parsing** — via CommandLineParser with typed options and mode resolution

## Installation

```
dotnet add package SunamoCl
```

## Target Frameworks

`net10.0`, `net9.0`, `net8.0`

Uses C# 12.0 features (collection expressions, primary constructors) requiring .NET 8.0+.

## Quick Start

```csharp
using SunamoCl;
using SunamoCl.SunamoCmd;
using SunamoCl.SunamoCmd.Args;

await CmdBootStrap.RunWithRunArgs(new RunArgs
{
    Args = args,
    IsDebug = false,
    AddGroupOfActions = () => new Dictionary<string, Func<Task<Dictionary<string, object>>>>
    {
        { "MyGroup", MyGroupActions }
    },
    IsVerboseConsoleLogging = true,
    ConsoleLogFilePath = "console.log"
});
```

## Key Classes

| Class | Description |
|---|---|
| `CL` | Static console operations — write, read, color, select, input prompts |
| `CmdBootStrap` | Application bootstrapping — RunWithRunArgs, DI, logging setup |
| `CmdApp` | Core app utilities — wait for file, unhandled exceptions |
| `CLActions` | Merge and execute sync/async action dictionaries |
| `CLAllActions` | Group-based action registration and execution |
| `CLProgressBar` | Single progress bar wrapper around ShellProgressBar |
| `CLProgressBarWithChilds` | Parent/child progress bars for parallel operations |
| `CmdTable` | Formatted ASCII table output |
| `TableParser` | Convert collections to formatted string tables |
| `ClFlasher` | Flash console window title bar (Windows) |
| `ClNotify` | Infinite loop runner and flash notifications |
| `TeeTextWriter` | Mirror console output to a log file |
| `ConsoleLogger` | Static console logger with internationalization |
| `ConsoleLoggerCmd` | Logger derived from LoggerBaseCl for DI integration |

## Links

- [NuGet](https://www.nuget.org/profiles/sunamo)
- [GitHub](https://github.com/sunamo/PlatformIndependentNuGetPackages)
- [Developer site](https://sunamo.cz)

Request for new features / bug report: [Mail](mailto:radek.jancik@sunamo.cz) or on GitHub
