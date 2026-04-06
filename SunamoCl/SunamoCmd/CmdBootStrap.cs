namespace SunamoCl.SunamoCmd;

/// <summary>
/// Provides bootstrapping functionality for command-line applications including running actions, DI configuration, and logging setup
/// </summary>
public class CmdBootStrap
{
    /// <summary>
    /// Automatically calls the method in release according to the args
    /// </summary>
    /// <param name="runArgs">Configuration arguments for the run.</param>
    /// <returns>Name of the executed action, or null if cancelled.</returns>
    public static async Task<string?> RunWithRunArgs(RunArgs runArgs)
    {
        TeeTextWriter? teeOut = null;
        TeeTextWriter? teeError = null;

        if (!string.IsNullOrEmpty(runArgs.ConsoleLogFilePath))
        {
            var logDirectory = Path.GetDirectoryName(runArgs.ConsoleLogFilePath);
            if (!string.IsNullOrEmpty(logDirectory) && !Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            teeOut = new TeeTextWriter(Console.Out, runArgs.ConsoleLogFilePath);
            Console.SetOut(teeOut);

            var errorLogPath = Path.Combine(
                Path.GetDirectoryName(runArgs.ConsoleLogFilePath) ?? ".",
                Path.GetFileNameWithoutExtension(runArgs.ConsoleLogFilePath) + ".errors" + Path.GetExtension(runArgs.ConsoleLogFilePath));
            teeError = new TeeTextWriter(Console.Error, errorLogPath);
            Console.SetError(teeError);

            Console.WriteLine($"Console output is being mirrored to file: {runArgs.ConsoleLogFilePath}");
        }

        if (runArgs.IsVerboseConsoleLogging)
        {
            Console.WriteLine("=== VERBOSE CONSOLE LOGGING IS ENABLED ===");
            Console.WriteLine("All important steps, decisions and data should be logged to console.");
            Console.WriteLine("This is essential for AI tools to understand what the application is doing.");
            Console.WriteLine("To disable, set RunArgs.IsVerboseConsoleLogging = false.");
            Console.WriteLine("===========================================");
        }

        try
        {
            var wasNull = new List<string>();

            if (runArgs.ShouldCatchUnhandledException)
            {
                AppDomain.CurrentDomain.UnhandledException += CmdApp.UnhandledExceptionTrapper;
            }

            CmdApp.ShouldLoadFromClipboard = runArgs.ShouldLoadFromClipboard;

            var askUser = false;
            var executedAction = string.Empty;
            if (!runArgs.IsDebug && runArgs.ShouldAskUserIfRelease.HasValue && runArgs.ShouldAskUserIfRelease.Value) askUser = true;

            if (runArgs.IsDebug)
            {
                if (runArgs.RunInDebugAsync == null)
                {
                    wasNull.Add(nameof(runArgs.RunInDebugAsync));
                }
                else
                {
                    await runArgs.RunInDebugAsync();
                    executedAction = nameof(runArgs.RunInDebugAsync);
                }
            }
            else
            {
                if (runArgs.AddGroupOfActions != null)
                {
                    if (runArgs.Args == null)
                    {
                        ThrowEx.Custom($"{nameof(runArgs.Args)} is null, enter args to recognize whether ask user for action");
                        return null;
                    }

                    if (runArgs.Args.Length != 0)
                    {
                        CL.WriteLine($"Some args were entered, askUser was set from {askUser} to false");
                        askUser = false;
                    }
                    await CLAllActions.AddToActions(runArgs.AddGroupOfActions);
                    if (askUser)
                    {
                        var selectedAction = CL.UserMustType("what you need or enter -1 to select from all groups");
                        executedAction = await CLAllActions.RunActionWithName(selectedAction);
                    }
                    else
                    {
                        if (runArgs.Args.Length > 0)
                        {
                            var action = runArgs.Args[0];
                            Console.WriteLine($"🏁 Starting: {action}");
                            executedAction = await CLAllActions.RunActionWithName(action);
                            Console.WriteLine($"✅ Completed: {action}");
                        }
                        else
                        {
                            throw new Exception($"{nameof(askUser)} was false, but runArgs.Args have zero elements. Maybe IsDebug is wrongly set to false. If the user is not asked, it is necessary to pass the action to be performed");
                        }
                    }
                }
            }
            if (wasNull.Count != 0) throw new Exception("Was null: " + string.Join(",", wasNull));

            return executedAction;
        }
        finally
        {
            if (teeOut != null)
            {
                teeOut.FinalizeLog();
                Console.SetOut(teeOut.OriginalWriter);
                teeOut.Dispose();
            }
            if (teeError != null)
            {
                teeError.FinalizeLog();
                Console.SetError(teeError.OriginalWriter);
                teeError.Dispose();
            }
        }
    }

    /// <summary>
    /// Resolve IConfiguration from appsettings.json
    /// </summary>
    /// <param name="services">Service collection to add configuration to.</param>
    public static void AddIConfiguration(IServiceCollection? services)
    {
        if (services != null)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration config = builder.Build();
            services.AddSingleton(config);
        }
        else
        {
            ThrowEx.Custom($"{nameof(services)} in {nameof(AddIConfiguration)} is null");
        }
    }

    /// <summary>
    /// Configures ILogger with console and/or file logging providers
    /// </summary>
    /// <param name="services">Service collection for dependency injection</param>
    /// <param name="isLoggingToConsole">Whether to add console logging provider</param>
    /// <param name="fileLoggerProvider">Optional file logger provider.</param>
    /// <param name="categoryNameLogger">Category name for the logger</param>
    public static void AddILogger(IServiceCollection? services, bool isLoggingToConsole, ILoggerProvider? fileLoggerProvider, string categoryNameLogger)
    {
        ServiceProvider? serviceProvider = null;
        if (services != null)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                if (isLoggingToConsole)
                {
                    loggingBuilder.AddConsole();
                }
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
            });
            #region It is necessary to add it this way, otherwise a new ILogger will be created for each file passed. It is not visible in the console but it is in the file
            serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            if (fileLoggerProvider != null)
            {
                loggerFactory.AddProvider(fileLoggerProvider);
            }
            if (categoryNameLogger == null)
            {
                throw new ArgumentNullException("categoryNameLogger was null");
            }
            var logger = loggerFactory.CreateLogger(categoryNameLogger);
            services.AddSingleton(typeof(ILogger), logger);
            #endregion
        }
        else if (isLoggingToConsole || fileLoggerProvider != null)
        {
            throw new Exception($"{nameof(services)} is null but {nameof(isLoggingToConsole)}/{nameof(fileLoggerProvider)} is set up");
        }
    }
}