namespace SunamoCl.SunamoCmd;
public class CmdBootStrap
{
    /// <summary>
    /// Automatically calls the method in release according to the args
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<string?> RunWithRunArgs(RunArgs a)
    {
        var wasNull = new List<string>();

        if (a.CatchUnhandledException)
        {
            AppDomain.CurrentDomain.UnhandledException += CmdApp.UnhandledExceptionTrapper;
        }

        CmdApp.LoadFromClipboard = a.LoadFromClipboard;

        var askUser = false;
        var arg = string.Empty;
        if (!a.IsDebug && a.AskUserIfRelease.HasValue && a.AskUserIfRelease.Value) askUser = true;

        if (a.IsDebug)
        {
            if (a.RunInDebugAsync == null)
            {
                wasNull.Add(nameof(a.RunInDebugAsync));
            }
            else
            {
                await a.RunInDebugAsync();
                arg = nameof(a.RunInDebugAsync);
            }
        }
        else
        {
            if (a.AddGroupOfActions != null)
            {
                if (a.Args == null)
                {
                    ThrowEx.Custom($"{nameof(a.Args)} is null, enter args to recognize whether ask user for action");
                    return null;
                }

                if (a.Args.Length != 0)
                {
                    CL.WriteLine($"Some args were entered, askUser was set from {askUser} to false");
                    askUser = false;
                }
                await CLAllActions.AddToActions(a.AddGroupOfActions);
                if (askUser)
                {
                    var whatUserNeed = CL.UserMustType("what you need or enter -1 to select from all groups");
                    arg = await CLAllActions.RunActionWithName(whatUserNeed);
                }
                else
                {
                    if (a.Args.Length > 0)
                    {
                        var action = a.Args[0];
                        Console.WriteLine($"🏁 Starting: {action}");
                        arg = await CLAllActions.RunActionWithName(action);
                        Console.WriteLine($"✅ Completed: {action}");
                    }
                    else
                    {
                        throw new Exception($"{nameof(askUser)} was false, but a.Args have zero elements. Maybe IsDebug is wrongly set to false. If the user is not asked, it is necessary to pass the action to be performed");
                    }
                }
            }
        }
        if (wasNull.Count != 0) throw new Exception("Was null: " + string.Join(",", wasNull));

        return arg;
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

    public static void AddILogger(IServiceCollection? services, bool IsLoggingToConsole, ILoggerProvider? FileLoggerProvider, string categoryNameLogger)
    {
        ServiceProvider sp = null;
        if (services != null)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                if (IsLoggingToConsole)
                {
                    loggingBuilder.AddConsole();
                }
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
            });
            #region It is necessary to add it this way, otherwise a new ILogger will be created for each file passed. It is not visible in the console but it is in the file
            sp = services.BuildServiceProvider();
            var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
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
        else if (IsLoggingToConsole || FileLoggerProvider != null)
        {
            throw new Exception($"{nameof(services)} is null but {nameof(IsLoggingToConsole)}/{nameof(FileLoggerProvider)} is set up");
        }
    }
}