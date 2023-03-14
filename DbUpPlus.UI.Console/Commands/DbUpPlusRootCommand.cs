using System.Security.Cryptography.X509Certificates;

namespace DbUpPlus.UI.Cli.Commands;

internal class DbUpPlusRootCommand : RootCommand
{
    public DbUpPlusRootCommand()
    {
        Description = """
            Utility for managing database migrations.
            Extends the functionality of DbUp.
            Inspired by Roundhouse/Grate.
            """;
        TreatUnmatchedTokensAsErrors = true;

        AddGlobalOption(GetConnectionStringOption());
        AddGlobalOption(GetScriptsFoldersPathsOption());

        AddCommand(new RunCommand());
    }

    private Option GetScriptsFoldersPathsOption()
    {
        var scriptsFoldersPathsOption = new Option<string>(name: $"--{GlobalOptions.ConfigurationSectionName}:{nameof(GlobalOptions.ScriptsFoldersPaths)}")
        {
            ArgumentHelpName = "path",
            Description = """OPTIONAL - Path to sql script folders: .\onetime, .\anytime, .\everytime""",
        };

        return scriptsFoldersPathsOption;
    }

    private Option GetConnectionStringOption()
    {
        var connectionStringOption = new Option<string>(name: $"--{GlobalOptions.ConfigurationSectionName}:{nameof(GlobalOptions.ConnectionString)}")
        {
            ArgumentHelpName = "connectionString",
            Description = """OPTIONAL - Format: "User ID=USERNAME;Password=PASSWORD;Host=HOST;Port=PORT;Database=DBNAME;" """,
        };

        return connectionStringOption;
    }
}