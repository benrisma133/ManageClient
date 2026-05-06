using Microsoft.Data.Sqlite;

namespace ManageClient.Repository;

public class DatabaseHelper
{
    private const string ConnectionString = "Data Source=clients.db";

    public static void InitializeDatabase()
    {
        using var connection = new SqliteConnection(ConnectionString);
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
        return new SqliteConnection(ConnectionString);
    }
}