using ApprenticeEventManager.Components.Users.Pages;
using ApprenticeEventManager.Models;
using Microsoft.Data.Sqlite;

namespace ApprenticeEventManager.DatabaseServices
{
  public class UserDb
  {

    private static readonly string connectionString = "Data Source=ApprenticeEventManager.db";

    public static List<User> GetAllDbUsers()
    {
      List<User> users = new();

      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string query = "SELECT * FROM aemapp_users";
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
            user.HashedPassword = reader.GetString(4);
            users.Add(user);
          }
        }
      }
      return users;
    }

    public static User GetUserById(int id)
    {
      return new User();
    }

    public static User GetUserByEmail(string email)
    {
      User retrievedUser = new();

      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string query = $"SELECT * FROM aemapp_users WHERE email = '{email}'";
        using (var command = new SqliteCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            retrievedUser.Id = reader.GetInt32(0);
            retrievedUser.FirstName = reader.GetString(1);
            retrievedUser.LastName = reader.GetString(2);
            retrievedUser.Email = reader.GetString(3);
            retrievedUser.HashedPassword = reader.GetString(4);
          }
        }
        return retrievedUser;
      }
    }

    public static string AddUserDb(User newUser)
    {
      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string insertUserQuery = "INSERT INTO aemapp_users (first_name, last_name, email, password) VALUES (@firstName, @lastName, @email, @password)";
        SqliteCommand command = new SqliteCommand(insertUserQuery, connection);
        command.Parameters.AddWithValue("@firstName", newUser.FirstName);
        command.Parameters.AddWithValue("@lastName", newUser.LastName);
        command.Parameters.AddWithValue("@email", newUser.Email);
        command.Parameters.AddWithValue("@password", newUser.HashedPassword);
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

    public static string CreatePasswordUserDb()
    {
      return "response";
    }

    public static string UpdatePasswordUserDb()
    {
      return "response";
    }
  }
}
