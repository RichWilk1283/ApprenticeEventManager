using ApprenticeEventManager.Models;
using Microsoft.Data.Sqlite;

namespace ApprenticeEventManager.DatabaseServices
{
  public class UserDb
  {

    private static string connectionString = "Data Source=ApprenticeEventManager.db";

    public static List<User> GetDbUsers()
    {

      return new List<User>();

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

    public static string AddUserDbTEST()
    {
      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string insertUserQuery = "INSERT INTO users (first_name, last_name, email) VALUES (@firstName, @lastName, @email)";
        SqliteCommand command = new SqliteCommand(insertUserQuery, connection);
        command.Parameters.AddWithValue("@firstName", "Steve");
        command.Parameters.AddWithValue("@lastName", "Harrington");
        command.Parameters.AddWithValue("@email", "steve@djo.co.uk");
        command.ExecuteNonQuery();
      }
      return $"Added User";
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
