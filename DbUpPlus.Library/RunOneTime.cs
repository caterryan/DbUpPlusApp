namespace DbUpPlus.Library;

public static class RunOneTime
{
    public static int Run(string connectionString, string scriptsFoldersPathString, bool dropDatabase)
    {
        int exitCode = 0;

        DirectoryInfo scriptsFoldersPath = new DirectoryInfo(scriptsFoldersPathString);



        if (!scriptsFoldersPath.Exists)
            throw new ArgumentException($"Invalid Input: path to script folders is not a valid directory '{scriptsFoldersPath.FullName}'");

        if (dropDatabase)
            Helpers.DropDatabase(connectionString);

        EnsureDatabase.For.PostgresqlDatabase(connectionString);

        UpgradeEngine upgrader = DeployChanges.To
        .PostgresqlDatabase(connectionString)
        .WithScriptsFromFileSystem
        (
            path: scriptsFoldersPath.FullName, 
            options: new FileSystemScriptOptions
            {
                IncludeSubDirectories = true,
                Filter = f => f.Contains($"onetime"),
                UseOnlyFilenameForScriptName = false,
            }
        )
        .LogToConsole()
        .WithTransactionPerScript()
        .WithVariablesDisabled()
        .Build();

        DatabaseUpgradeResult result = upgrader.PerformUpgrade();

        if (!result.Successful)
            Helpers.ShowFailure(nameof(RunOneTime), result.Error);
        else
            Helpers.ShowSuccess(nameof(RunOneTime));

        return exitCode;
    }
}