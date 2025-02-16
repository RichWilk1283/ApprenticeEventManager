using ApprenticeEventManager.Models;
using Microsoft.Data.Sqlite;

namespace ApprenticeEventManager.DatabaseServices
{
  public class TeamDb
  {
    private static string connectionString = "Data Source=ApprenticeEventManager.db";

    public static List<Team> GetAllDbTeams()
    {
      List<Team> teams = new();

      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string query = "SELECT * FROM team";
        using (var command = new SqliteCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            Team team = new();
            team.Id = reader.GetInt32(0);
            team.Name = reader.GetString(1);
            team.HomeOffice = reader.GetString(2);
            teams.Add(team);
          }
        }
      }
      return teams;
    }

    public static string AddTeamDb(Team newTeam)
    {
      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string insertQuery = "INSERT INTO team (name, home_office) VALUES (@name, @homeOffice)";
        SqliteCommand command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@name", newTeam.Name);
        command.Parameters.AddWithValue("@homeOffice", newTeam.HomeOffice);
        command.ExecuteNonQuery();
      }
      return $"Added Team: {newTeam.Name}, with a home office of: {newTeam.HomeOffice}.";

    }
  }
}