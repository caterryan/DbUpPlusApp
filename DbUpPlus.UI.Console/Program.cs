using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            var config = context.Configuration;

            #region Options Registration
            services
                .AddOptions<GlobalOptions>()
                .Bind(config.GetSection(GlobalOptions.ConfigurationSectionName))
                .ValidateDataAnnotations();

            services
                .AddOptions<RunOneTimeOptions>()
                .Bind<RunOneTimeOptions>(config.GetSection(RunOneTimeOptions.ConfigurationSectionName));
            #endregion
            #region Options Validations Registration
            services.TryAddEnumerable(
                ServiceDescriptor.Singleton
                <IValidateOptions<GlobalOptions>, GlobalOptionsValidationsScriptsFoldersPaths>());

            services.TryAddEnumerable(
                ServiceDescriptor.Singleton
                <IValidateOptions<GlobalOptions>, GlobalOptionsValidationsConnectionString>());
            #endregion
            #region Service Registrations
            
            #endregion

        })
            #region Command Handler Registrations

        .UseCommandHandler<RunOneTimeCommand, RunOneTimeCommand.Handler>();
            #endregion

    private static Func<string[], IHostBuilder> GetHostBuilderFactory(string[] args) =>
        _ => Host.CreateDefaultBuilder(args);
}
