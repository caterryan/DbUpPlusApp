
namespace DbUpPlus.UI.Cli.Commands;

internal class RunOneTimeCommand : Command
{
    public RunOneTimeCommand() : base("onetime")
    {
        Description = "Run exactly once per database";
        TreatUnmatchedTokensAsErrors = true;

        AddOption(GetDropDatabaseOption());
    }

    private Option GetDropDatabaseOption()
    {
        var dropDatabaseOption = new Option<bool>(name: $"--{RunOneTimeOptions.ConfigName}:{nameof(RunOneTimeOptions.DropDatabase)}")
        {
            Description = "OPTIONAL - Drops the existing database - Default: false",
        };
        return dropDatabaseOption;
    }

    public new class Handler : ICommandHandler
    {
        private readonly GlobalOptions _globalOptions;
        private readonly RunOneTimeOptions _runOneTimeOptions;
        private readonly ILogger<Handler> _logger;

        public Handler(
            IOptions<GlobalOptions> globalOptions, 
            IOptions<RunOneTimeOptions> runOneTimeOptions,
            ILogger<Handler> logger)
        {
            _globalOptions = globalOptions.Value;
            _runOneTimeOptions = runOneTimeOptions.Value;
            _logger = logger;
        }

        public int Invoke(InvocationContext context)
        {
            var exitCode = context.ExitCode;

            //exitCode = RunOneTime.Run();

            _logger.LogError($"{nameof(_globalOptions.ConnectionString)}: {_globalOptions.ConnectionString}");
            _logger.LogError($"{nameof(_globalOptions.ScriptsFoldersPaths)}: {_globalOptions.ScriptsFoldersPaths}");
            _logger.LogError($"{nameof(_runOneTimeOptions.DropDatabase)}: {_runOneTimeOptions.DropDatabase}");

            return exitCode;
        }

        public Task<int> InvokeAsync(InvocationContext context) => Task.FromResult(Invoke(context));
    }
}