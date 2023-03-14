
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
        var dropDatabaseOption = new Option<bool>(name: $"--{RunOneTimeOptions.ConfigurationSectionName}:{nameof(RunOneTimeOptions.DropDatabase)}")
        {
            Description = "OPTIONAL - Drops the existing database - Default: false",
        };
        return dropDatabaseOption;
    }

    public new class Handler : ICommandHandler
    {
        private int _exitCode;

        private readonly GlobalOptions _globalOptions;
        private readonly RunOneTimeOptions _runOneTimeOptions;
        private readonly ILogger<Handler> _logger;

        public Handler(
            IOptions<GlobalOptions> globalOptions, 
            IOptions<RunOneTimeOptions> runOneTimeOptions,
            ILogger<Handler> logger)
        {
            _logger = logger;

            try
            {
                _globalOptions = globalOptions.Value;
            }
            catch (OptionsValidationException ex)
            {
                _exitCode = 1;
                foreach (string failure in ex.Failures)
                {
                    _logger.LogError(failure);
                }
            }

            try
            {
                _runOneTimeOptions = runOneTimeOptions.Value;
            }
            catch (OptionsValidationException ex)
            {
                _exitCode = 1;
                foreach (string failure in ex.Failures)
                {
                    _logger.LogError(failure);
                }
            }
        }

        public int Invoke(InvocationContext context)
        {
            if (_exitCode != 0)
            {
                _logger.LogError($"Failed to successfully complete {nameof(RunOneTimeCommand)}");
                return _exitCode;
            }

            DirectoryInfo scriptsFoldersDirectory = new(_globalOptions.ScriptsFoldersPaths);

            _exitCode = RunOneTime.Run(
                _globalOptions.ConnectionString,
                scriptsFoldersDirectory.FullName,
                _runOneTimeOptions.DropDatabase);

            return _exitCode;
        }

        public Task<int> InvokeAsync(InvocationContext context) => Task.FromResult(Invoke(context));
    }
}