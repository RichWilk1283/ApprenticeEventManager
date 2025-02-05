using Microsoft.Data.Sqlite;

namespace ApprenticeEventManager.DatabaseServices
{
  public class DbService
  {
    private static string connectionString = "Data Source=ApprenticeEventManager.db";

    public static void InitialiseDb()
    {

      string[] tableQuery = [
        "CREATE TABLE IF NOT EXISTS Events (" +
        "event_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "name TEXT NOT NULL," +
        "date TEXT NOT NULL," +
        "apprentices_required INTEGER)",

        "CREATE TABLE IF NOT EXISTS ItemsDuplicate (" +
        "item_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "item_name TEXT NOT NULL," +
        "item_description TEXT," +
        "item_value TEXT)",

        "CREATE TABLE IF NOT EXISTS ItemsDuplicate (" +
        "item_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "item_name TEXT NOT NULL," +
        "item_description TEXT," +
        "item_value TEXT)",

        "CREATE TABLE IF NOT EXISTS ItemsDuplicate (" +
        "item_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "item_name TEXT NOT NULL," +
        "item_description TEXT," +
        "item_value TEXT)",

        "CREATE TABLE IF NOT EXISTS ItemsDuplicate (" +
        "item_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "item_name TEXT NOT NULL," +
        "item_description TEXT," +
        "item_value TEXT)",

        "CREATE TABLE IF NOT EXISTS ItemsDuplicate (" +
        "item_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "item_name TEXT NOT NULL," +
        "item_description TEXT," +
        "item_value TEXT)",
      ];

      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();

        foreach (var table in tableQuery)
        {
          SqliteCommand command = new SqliteCommand(table, connection);
          command.ExecuteNonQuery();
        }        
      }
      Console.WriteLine("Hopefully db's created");
    }
  }
}
