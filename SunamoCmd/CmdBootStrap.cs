namespace SunamoCl.SunamoCmd;
public class CmdBootStrap
{
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
                    CL.WriteLine($"Was entered some args, askUser was setted from {askUser} to false");
                    askUser = false;
                }

                await CLAllActions.AddToActions(a.AddGroupOfActions);

                if (askUser)
                {
                    var whatUserNeed = CL.UserMustType("you need or enter -1 for select from all groups");
                    arg = await CLAllActions.RunActionWithName(whatUserNeed);
                }
                else
                {

                    if (a.Args.Length > 0)
                    {
                        arg = await CLAllActions.RunActionWithName(a.Args[0]);
                    }
                    else
                    {
                        throw new Exception($"{nameof(askUser)} was false, but a.Args have zero elements. Maybe is wrongly IsDebug = false. If is not asking user, it is necessary to pass the action that will be performed");
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
            #region Je to nutno přidávat takto. jinak při předání do každého souboru vytvoří nový ILogger. v konzoli to nejde vidět ale v souboru ano
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
            #region Špatné - vytváří stále novou instanci
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