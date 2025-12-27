namespace SunamoCl.SunamoCmd;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public class CmdBootStrap
{
    /// <summary>
    /// Automatically calls the method in release according to the args
    /// </summary>
    /// <param name="runArgs"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<string?> RunWithRunArgs(RunArgs runArgs)
    {
        var wasNull = new List<string>();

        if (runArgs.CatchUnhandledException)
        {
            AppDomain.CurrentDomain.UnhandledException += CmdApp.UnhandledExceptionTrapper;
        }

        CmdApp.LoadFromClipboard = runArgs.LoadFromClipboard;

        var askUser = false;
        var executedAction = string.Empty;
        if (!runArgs.IsDebug && runArgs.AskUserIfRelease.HasValue && runArgs.AskUserIfRelease.Value) askUser = true;

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

    /// <summary>
    /// Resolve IConfiguration from appsettings.json
    /// </summary>
    /// <param name="services"></param>
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

    public static void AddILogger(IServiceCollection? services, bool isLoggingToConsole, ILoggerProvider? FileLoggerProvider, string categoryNameLogger)
    {
        ServiceProvider serviceProvider = null;
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
            if (FileLoggerProvider != null)
            {
                loggerFactory.AddProvider(FileLoggerProvider);
            }
            if (categoryNameLogger == null)
            {
                throw new ArgumentNullException("categoryNameLogger was null");
            }
            var logger = loggerFactory.CreateLogger(categoryNameLogger);
            services.AddSingleton(typeof(ILogger), logger);
            #endregion
            #region Bad - creates a new instance all the time
            //services.AddTransient(provider =>
            //{
            //    var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
            //    if (a.FileLoggerProvider != null)
            //    {
            //        loggerFactory.AddProvider(a.FileLoggerProvider);
            //    }
            //    if (a.categoryNameLogger == null)
            //    {
            //        throw new ArgumentNullException("categoryNameLogger was null");
            //    }
            //    return loggerFactory.CreateLogger(a.categoryNameLogger);
            //}); 
            #endregion
        }
        else if (isLoggingToConsole || FileLoggerProvider != null)
        {
            throw new Exception($"{nameof(services)} is null but {nameof(isLoggingToConsole)}/{nameof(FileLoggerProvider)} is set up");
        }
    }
}