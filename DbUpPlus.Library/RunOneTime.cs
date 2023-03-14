﻿namespace DbUpPlus.Library;

public static class RunOneTime
{
    public static int Run(string connectionString, string scriptsFoldersPathString, bool dropDatabase)
    {
        int exitCode = 0;

        if (dropDatabase)
            Helpers.DropDatabase(connectionString);

        EnsureDatabase.For.PostgresqlDatabase(connectionString);

        UpgradeEngine upgrader = DeployChanges.To
        .PostgresqlDatabase(connectionString)
        .WithScriptsFromFileSystem
        (
            path: scriptsFoldersPathString, 
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