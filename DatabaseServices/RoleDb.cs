using ApprenticeEventManager.Models;
using Microsoft.Data.Sqlite;

namespace ApprenticeEventManager.DatabaseServices
{
  public class RoleDb
  {
    private static readonly string connectionString = "Data Source=ApprenticeEventManager.db";

    public static List<Role> GetAllDbRoles()
    {
      List<Role> roles = new();

      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string query = "SELECT * FROM aemapp_roles";
        using (var command = new SqliteCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            Role role = new();
            role.Id = reader.GetInt32(0);
            role.Name = reader.GetString(1);
            roles.Add(role);
          }
        }
      }
      return roles;
    }

    public static Role GetById(int id)
    {
      return new Role();
    }

    public static string AddRoleDb(Role newRole)
    {
      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string insertQuery = "INSERT INTO aemapp_roles (name) VALUES (@name)";
        SqliteCommand command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@name", newRole.Name);
        command.ExecuteNonQuery();
      }
      return $"Added Role: {newRole.Name}.";
    }

    public static string RemoveRoleDb()
    {
      return "response";
    }

    public static string UpdateRoleDb()
    {
      return "response";
    }
  }
}