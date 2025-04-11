using ApprenticeEventManager.Models;
using Microsoft.Data.Sqlite;

namespace ApprenticeEventManager.DatabaseServices
{
  public class UserDb
  {
    private readonly string _connectionString;
    private RoleDb _roleDb;

    public UserDb(IConfiguration config, RoleDb roleDb)
    {
      _connectionString = config.GetConnectionString("DefaultConnection");
      _roleDb = roleDb;
    }

    public List<User> GetAllDbUsers()
    {
      List<User> users = new();

      using (var connection = new SqliteConnection(_connectionString))
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

    public User GetUserById(int id)
    {
      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string getByIdQuery = "SELECT * FROM aemapp_users WHERE user_id = @userId";
        using (var command = new SqliteCommand(getByIdQuery, connection))
        {
          command.Parameters.AddWithValue("@userId", id);

          using (var reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              return new User
              {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3),
                HashedPassword = reader.GetString(4)
              };
            }
          }
        }
      }
      return null;
    }

    public User GetUserByEmail(string email)
    {
      User retrievedUser = new();

      using (var connection = new SqliteConnection(_connectionString))
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

    public string AddUserDb(User newUser)
    {
      using (var connection = new SqliteConnection(_connectionString))
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

    public bool RemoveUserDb(User user)
    {
      User userCheck = GetUserById(user.Id);

      if (userCheck == null)
      {
        return false;
      }

      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string deleteQuery = "DELETE FROM aemapp_users WHERE user_id = @userId";
        SqliteCommand command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@userId", user.Id);
        command.ExecuteNonQuery();
      }
      return true;
    }

    public bool UpdateUserDb(User updatedUser)
    {
      User userCheck = GetUserById(updatedUser.Id);

      if (userCheck == null)
      {
        return false;
      }

      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string updateQuery = "UPDATE aemapp_users SET first_name = @firstName, last_name = @lastName, email = @email WHERE user_id = @userId";
        SqliteCommand command = new SqliteCommand(updateQuery, connection);
        command.Parameters.AddWithValue("@firstName", updatedUser.FirstName);
        command.Parameters.AddWithValue("@lastName", updatedUser.LastName);
        command.Parameters.AddWithValue("@email", updatedUser.Email);
        command.Parameters.AddWithValue("@userId", updatedUser.Id);
        command.ExecuteNonQuery();
      }
      return true;
    }

    public string UpdatePasswordUserDb()
    {
      return "response";
    }

    public bool AssignUserRole(int userId, string roleName = "User")
    {
      Role role = _roleDb.GetRoleByName(roleName);
      if (role == null)
      {
        return false;
      }

      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string userRoleQuery = "INSERT INTO aemapp_user_roles (user_id, role_id) VALUES (@userId, @roleId)";
        SqliteCommand command = new SqliteCommand(userRoleQuery, connection);
        command.Parameters.AddWithValue("@userId", userId);
        command.Parameters.AddWithValue("@roleId", role.Id);
        command.ExecuteNonQuery();
      }

      return true;
    }

    public List<string> GetUserRole(User user)
    {
      List<string> roles = new();

      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string getUsersRolesQuery = "SELECT r.name FROM aemapp_roles r INNER JOIN aemapp_user_roles ur ON r.role_id = ur.role_id WHERE ur.user_id = @userId";
        using (var command = new SqliteCommand(getUsersRolesQuery, connection))
        {
          command.Parameters.AddWithValue("@userId", user.Id);

          using (var reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              roles.Add(reader.GetString(0));
            }
          }
        }
      }

      return roles;
    }
  }
}
