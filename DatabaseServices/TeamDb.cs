using ApprenticeEventManager.Components.Teams.Pages;
using ApprenticeEventManager.Models;
using Microsoft.Data.Sqlite;

namespace ApprenticeEventManager.DatabaseServices
{
  public class TeamDb
  {
    private static readonly string connectionString = "Data Source=ApprenticeEventManager.db";

    public static List<Team> GetAllDbTeams()
    {
      List<Team> teams = new();

      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string getAllQuery = "SELECT * FROM aemapp_teams";
        using (var command = new SqliteCommand(getAllQuery, connection))
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

    public static Team GetTeamById(int id)
    {
      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string getByIdQuery = "SELECT * FROM aemapp_teams WHERE team_id = @teamId";
        using (var command = new SqliteCommand(getByIdQuery, connection))
        {
          command.Parameters.AddWithValue("@teamId", id);

          using (var reader = command.ExecuteReader())
          {
            if (reader.Read())
            {
              return new Team 
              {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                HomeOffice = reader.GetString(2)
              };
            }
          }
        }
      }
      return null;
    }

    public static string AddTeamDb(Team newTeam)
    {
      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string insertQuery = "INSERT INTO aemapp_teams (name, home_office) VALUES (@name, @homeOffice)";
        SqliteCommand command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@name", newTeam.Name);
        command.Parameters.AddWithValue("@homeOffice", newTeam.HomeOffice);
        command.ExecuteNonQuery();
      }
      return $"Added Team: {newTeam.Name}, with a home office of: {newTeam.HomeOffice}.";

    }

    public static bool RemoveTeamDb(Team team)
    {
      Team teamCheck = GetTeamById(team.Id);

      if (teamCheck == null)
      {
        return false;
      }

      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string deleteQuery = "DELETE FROM aemapp_teams WHERE team_id = @teamId";
        SqliteCommand command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@teamId", team.Id);
        command.ExecuteNonQuery();
      }
      return true;
    }

    public static bool UpdateTeamDb(Team updatedTeam)
    {
      Team teamCheck = GetTeamById(updatedTeam.Id);

      if (teamCheck == null)
      {
        return false;
      }

      using (var connection = new SqliteConnection(connectionString))
      {
        connection.Open();
        string updateQuery = "UPDATE aemapp_teams SET name = @teamName, home_office = @homeOffice WHERE team_id = @teamId";
        SqliteCommand command = new SqliteCommand(updateQuery, connection);
        command.Parameters.AddWithValue("@teamName", updatedTeam.Name);
        command.Parameters.AddWithValue("@homeOffice", updatedTeam.HomeOffice);
        command.Parameters.AddWithValue("@teamId", updatedTeam.Id);
        command.ExecuteNonQuery();
      }
      return true;
    }
  }
}