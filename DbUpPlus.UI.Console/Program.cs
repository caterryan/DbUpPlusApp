namespace DbUpPlus.UI.Cli;

internal class Program
{
    static void Main(string[] args) => new CommandLineBuilder(new DbUpPlusRootCommand())
            .UseHost(GetHostBuilderFactory(args), GetConfigureHost())
            .UseDefaults()
            .Build()
            .InvokeAsync(args);

    private static Action<IHostBuilder> GetConfigureHost() =>
        builder => builder
        .ConfigureServices((context, services) =>
        {
            // Register Config Options
            var config = context.Configuration;
            services.Configure<GlobalOptions>(config.GetSection(GlobalOptions.ConfigName));
            services.Configure<RunOneTimeOptions>(config.GetSection(RunOneTimeOptions.ConfigName));

            // Register Services

        })
        .UseCommandHandler<RunOneTimeCommand, RunOneTimeCommand.Handler>();

    private static Func<string[], IHostBuilder> GetHostBuilderFactory(string[] args) =>
        _ => Host.CreateDefaultBuilder(args);
}
