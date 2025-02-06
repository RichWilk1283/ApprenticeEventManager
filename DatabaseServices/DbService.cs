using Microsoft.Data.Sqlite;

namespace ApprenticeEventManager.DatabaseServices
{
  public class DbService
  {
    private static string connectionString = "Data Source=ApprenticeEventManager.db";

    public static void InitialiseDb()
    {

      string[] tableQuery = [
        "CREATE TABLE IF NOT EXISTS events (" +
        "event_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "name TEXT NOT NULL," +
        "description TEXT," +
        "date TEXT NOT NULL," +
        "apprentices_required INTEGER)",

        "CREATE TABLE IF NOT EXISTS addresses (" +
        "address_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "number_name TEXT NOT NULL," +
        "street_name TEXT," +
        "city TEXT," +
        "county TEXT," +
        "postcode TEXT)",

        "CREATE TABLE IF NOT EXISTS business_function (" +
        "function_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "name TEXT NOT NULL)",

        "CREATE TABLE IF NOT EXISTS roles (" +
        "role_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "name TEXT NOT NULL)",

        "CREATE TABLE IF NOT EXISTS slots (" +
        "slot_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "start TEXT NOT NULL," +
        "end TEXT NOT NULL," +
        "required_apprentices INTEGER)",

        "CREATE TABLE IF NOT EXISTS team (" +
        "team_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "name TEXT NOT NULL," +
        "home_office TEXT)",

        "CREATE TABLE IF NOT EXISTS users (" +
        "user_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "first_name TEXT NOT NULL," +
        "last_name TEXT," +
        "email TEXT)"
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
