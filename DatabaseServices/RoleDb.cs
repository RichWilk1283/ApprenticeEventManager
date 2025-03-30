using ApprenticeEventManager.Models;
using Microsoft.Data.Sqlite;

namespace ApprenticeEventManager.DatabaseServices
{
  public class RoleDb
  {
    private readonly string _connectionString;

    public RoleDb(IConfiguration config)
    {
      _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public List<Role> GetAllDbRoles()
    {
      List<Role> roles = new();

      using (var connection = new SqliteConnection(_connectionString))
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

    public Role GetRoleById(int id)
    {
      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string getByIdQuery = "SELECT * FROM aemapp_roles WHERE role_id = @roleId";
        using (var command = new SqliteCommand(getByIdQuery, connection))
        {
          command.Parameters.AddWithValue("@roleId", id);

          using (var reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              return new Role
              {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
              };
            }
          }
        }
      }
      return null;
    }

    public Role GetRoleByName(string roleName)
    {
      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string getByIdQuery = "SELECT * FROM aemapp_roles WHERE name = @roleName";
        using (var command = new SqliteCommand(getByIdQuery, connection))
        {
          command.Parameters.AddWithValue("@roleName", roleName);

          using (var reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              return new Role
              {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
              };
            }
          }
        }
        return null;
      }
    }

    public string AddRoleDb(Role newRole)
    {
      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string insertQuery = "INSERT INTO aemapp_roles (name) VALUES (@name)";
        SqliteCommand command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@name", newRole.Name);
        command.ExecuteNonQuery();
      }
      return $"Added Role: {newRole.Name}.";
    }

    public bool RemoveRoleDb(Role role)
    {
      Role roleCheck = GetRoleById(role.Id);

      if (roleCheck == null)
      {
        return false;
      }

      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string deleteQuery = "DELETE FROM aemapp_roles WHERE role_id = @roleId";
        SqliteCommand command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@roleId", role.Id);
        command.ExecuteNonQuery();
      }
      return true;
    }

    public string UpdateRoleDb()
    {
      return "response";
    }
  }
}