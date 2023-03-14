using System.Net.Sockets;

namespace DbUpPlus.Library;

public static class RunOneTime
{
    public static int Run(string connectionString, string scriptsFoldersPathString, bool dropDatabase)
    {
        int exitCode;

        try
        {
            if (dropDatabase)
                Helpers.DropDatabase(connectionString);

            EnsureDatabase.For.PostgresqlDatabase(connectionString);
        }
        catch (SocketException ex)
        {
            Helpers.ShowFailure(nameof(RunOneTime), ex); 
            exitCode = 1;
            return exitCode;
        }
        

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
        {
            Helpers.ShowFailure(nameof(RunOneTime), result.Error);
            exitCode = 1;
        }
        else
        {
            Helpers.ShowSuccess(nameof(RunOneTime));
            exitCode = 0;
        }

        return exitCode;
    }
}
