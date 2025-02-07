using ApprenticeEventManager.Models;
using Microsoft.Data.Sqlite;

namespace ApprenticeEventManager.DatabaseServices
{
  public class UserDb
  {

    private static string connectionString = "Data Source=ApprenticeEventManager.db";

    public static List<User> GetAllDbUsers()
    {
      List<User> users = new();

      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string query = "SELECT * FROM users";
        using (var command = new SqliteCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            User user = new();
            user.Id = reader.GetInt32(0);
            user.FirstName = reader.GetString(1);
            user.LastName = reader.GetString(2);
            user.Email = reader.GetString(3);
            users.Add(user);
          }
        }
      }
      return users;
    }

    public static User GetById(int id)
    {
      return new User();
    }

    public static string AddUserDb(User newUser)
    {
      Console.WriteLine($"{newUser.FirstName} {newUser.LastName}");
      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string insertUserQuery = "INSERT INTO users (first_name, last_name, email) VALUES (@firstName, @lastName, @email)";
        SqliteCommand command = new SqliteCommand(insertUserQuery, connection);
        command.Parameters.AddWithValue("@firstName", newUser.FirstName);
        command.Parameters.AddWithValue("@lastName", newUser.LastName);
        command.Parameters.AddWithValue("@email", newUser.Email);
        command.ExecuteNonQuery();
      }
      return $"Added User: {newUser.FirstName} {newUser.LastName}, {newUser.Email}";
    }

    public static string RemoveUserDb()
    {
      return "response";
    }

    public static string UpdateUserDb()
    {
      return "response";
    }
  }
}
