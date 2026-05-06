using Microsoft.Data.Sqlite;

public class DatabaseHelper
{
    private static string GetConnectionString()
    {
        var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "clients.db";
        return $"Data Source={dbName}";
    }

    public static void InitializeDatabase()
    {
        using var connection = new SqliteConnection(GetConnectionString());
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Client (
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName    TEXT NOT NULL,
                PhoneNumber TEXT NOT NULL,
                Email       TEXT NOT NULL
            );";
        command.ExecuteNonQuery();
    }

    public static SqliteConnection GetConnection()
    {
        return new SqliteConnection(GetConnectionString());
    }
}