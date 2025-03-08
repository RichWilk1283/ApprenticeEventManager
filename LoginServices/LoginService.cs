using ApprenticeEventManager.Components.Login.Models;
using ApprenticeEventManager.DatabaseServices;
using ApprenticeEventManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;

namespace ApprenticeEventManager.LoginServices
{
  public class LoginService
  {
    private readonly string _connectionString;

    public LoginService(IConfiguration config)
    {
      _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public bool AuthenticateUser(LoginFormModel authUser)
    {
      User dbUser = UserDb.GetUserByEmail(authUser.Email);

      var passwordHasher = new PasswordHasher<User>();
      var result = passwordHasher.VerifyHashedPassword(dbUser, dbUser.HashedPassword, authUser.HashedPassword);

      return result == PasswordVerificationResult.Success;
    }

    public void LogLoginAttempt(LoginFormModel loginUser, int successful)
    {
      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string insertQuery = "INSERT INTO aemapp_login_log (login_date, successful, login_email, login_password) VALUES (@logindate, @successful, @loginemail, @loginpassword)";
        SqliteCommand command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@logindate", new DateTime());
        command.Parameters.AddWithValue("@successful", successful);
        command.Parameters.AddWithValue("@loginemail", loginUser.Email);
        command.Parameters.AddWithValue("@loginpassword", loginUser.HashedPassword);
        command.ExecuteNonQuery();
      }


    }
  }
}
