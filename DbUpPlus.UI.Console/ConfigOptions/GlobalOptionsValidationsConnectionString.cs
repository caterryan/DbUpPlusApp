namespace DbUpPlus.UI.Cli.ConfigOptions;

sealed partial class GlobalOptionsValidationsConnectionString : IValidateOptions<GlobalOptions>
{
    public GlobalOptions? _options;

    public GlobalOptionsValidationsConnectionString(IConfiguration configuration)
    {
        _options = configuration
                    .GetSection(GlobalOptions.ConfigurationSectionName)
                    .Get<GlobalOptions>();
    }

    public ValidateOptionsResult Validate(string? name, GlobalOptions options)
    {
        StringBuilder failure = new();


        #region Validations

        Regex validationRegex = ValidationRegex();
        Match match = validationRegex.Match(_options.ConnectionString);
        if (string.IsNullOrEmpty(match.Value))
        {
            failure.AppendLine(
                $"""
                Invalid {nameof(_options.ConnectionString)}: {_options.ConnectionString}.
                ConnectionString Format: User ID=USERNAME;Password=PASSWORD;Host=HOST;Port=PORT;Database=DATABASE;
                """);
        }
        
        #endregion


        return failure.Length > 0
        ? ValidateOptionsResult.Fail(failure.ToString())
        : ValidateOptionsResult.Success;
    }

    [GeneratedRegex(@"^User ID=(\S*);Password=(\S*);Host=(\S*);Port=(\S*);Database=(\S*);$")]
    private static partial Regex ValidationRegex();

}
