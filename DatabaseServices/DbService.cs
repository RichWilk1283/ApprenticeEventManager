using Microsoft.Data.Sqlite;

namespace ApprenticeEventManager.DatabaseServices
{
  public class DbService
  {
    private readonly string _connectionString;

    public DbService(IConfiguration config)
    {
      _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public void InitialiseDb()
    {

      string[] tableQuery = [
        "CREATE TABLE IF NOT EXISTS aemapp_events (" +
        "event_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "name TEXT NOT NULL," +
        "description TEXT," +
        "date TEXT NOT NULL," +
        "apprentices_required INTEGER)",

        "CREATE TABLE IF NOT EXISTS aemapp_addresses (" +
        "address_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "number_name TEXT NOT NULL," +
        "street_name TEXT," +
        "city TEXT," +
        "county TEXT," +
        "postcode TEXT)",

        "CREATE TABLE IF NOT EXISTS aemapp_business_function (" +
        "function_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "name TEXT NOT NULL)",

        "CREATE TABLE IF NOT EXISTS aemapp_roles (" +
        "role_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "name TEXT NOT NULL)",

        "CREATE TABLE IF NOT EXISTS aemapp_slots (" +
        "slot_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "start TEXT NOT NULL," +
        "end TEXT NOT NULL," +
        "required_apprentices INTEGER)",

        "CREATE TABLE IF NOT EXISTS aemapp_teams (" +
        "team_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "name TEXT NOT NULL," +
        "home_office TEXT)",

        "CREATE TABLE IF NOT EXISTS aemapp_users (" +
        "user_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "first_name TEXT NOT NULL," +
        "last_name TEXT," +
        "email TEXT," +
        "password TEXT)",

        "CREATE TABLE IF NOT EXISTS aemapp_user_roles (" +
        "user_id INTEGER NOT NULL," +
        "role_id INTEGER NOT NULL," +
        "PRIMARY KEY (user_id, role_id)," +
        "FOREIGN KEY(user_id) REFERENCES aemapp_users(user_id) ON DELETE CASCADE," +
        "FOREIGN KEY(role_id) REFERENCES aemapp_roles(role_id) ON DELETE CASCADE)",

        "CREATE TABLE IF NOT EXISTS aemapp_user_events (" +
        "user_id INTEGER NOT NULL," +
        "event_id INTEGER NOT NULL," +
        "PRIMARY KEY (user_id, event_id)," +
        "FOREIGN KEY(user_id) REFERENCES aemapp_users(user_id) ON DELETE CASCADE," +
        "FOREIGN KEY(event_id) REFERENCES aemapp_events(event_id) ON DELETE CASCADE)",

        "CREATE TABLE IF NOT EXISTS aemapp_login_log (" +
        "log_id INTEGER PRIMARY KEY AUTOINCREMENT," +
        "login_date TEXT NOT NULL," +
        "successful INTEGER NOT NULL CHECK (successful IN (0, 1))," +
        "login_email TEXT," +
        "login_password TEXT)"
      ];

      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();

        foreach (var table in tableQuery)
        {
          SqliteCommand command = new SqliteCommand(table, connection);
          command.ExecuteNonQuery();
        }        
      }
    }
  }
}
