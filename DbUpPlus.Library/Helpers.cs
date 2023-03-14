namespace DbUpPlus.Library;

public static class Helpers
{
    public static int ShowFailure(string runType, Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{runType} failed!");
        Console.WriteLine(ex);
        Console.ResetColor();

        return -1;
    }

    public static int ShowSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{message} completed successfully!");
        Console.ResetColor();

        return 0;
    }

    public static void DropDatabase(string connectionString)
    {
        NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(connectionString);

        string databaseName = builder.Database;
        builder.Database = "postgres";

        string sql =
            $"""
            SELECT pg_terminate_backend(pg_stat_activity.pid) FROM pg_stat_activity WHERE pg_stat_activity.datname = '{databaseName}';

                DROP DATABASE IF EXISTS "{databaseName}"
            """;

        using (NpgsqlConnection connection = new NpgsqlConnection(builder.ToString()))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }

            connection.Close();
            Console.WriteLine($"Dropped database {databaseName}");
        }
    }
}
