using ApprenticeEventManager.Components.Users.Pages;
using ApprenticeEventManager.Models;
using Microsoft.Data.Sqlite;

namespace ApprenticeEventManager.DatabaseServices
{
  public class EventDb
  {
    private readonly string _connectionString;

    public EventDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public List<Event> GetAllDbEvents()
    {
      List<Event> allEvents = new();

      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string query = "SELECT * FROM aemapp_events";
        using (var command = new SqliteCommand(query, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            Event newEvent = new();
            newEvent.Id = reader.GetInt32(0);
            newEvent.Name = reader.GetString(1);
            newEvent.Description = reader.GetString(2);
            if (DateOnly.TryParse(reader.GetString(3), out DateOnly result))
            {
              newEvent.Date = result;
            }
            else
            {
              newEvent.Date = new DateOnly();
            }
            newEvent.TotalApprenticesRequired = reader.GetInt32(4);
            allEvents.Add(newEvent);
          }
        }
      }
      return allEvents;
    }

    public Event GetEventById(int id)
    {
      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string getByIdQuery = "SELECT * FROM aemapp_events WHERE event_id = @eventId";
        using (var command = new SqliteCommand(getByIdQuery, connection))
        {
          command.Parameters.AddWithValue("@eventId", id);

          using (var reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              Event newEvent = new();
              newEvent.Id = reader.GetInt32(0);
              newEvent.Name = reader.GetString(1);
              newEvent.Description = reader.GetString(2);
              if (DateOnly.TryParse(reader.GetString(3), out DateOnly result))
              {
                newEvent.Date = result;
              }
              else
              {
                newEvent.Date = new DateOnly();
              }
              newEvent.TotalApprenticesRequired = reader.GetInt32(4);
              return newEvent;
            }
          }
        }
      }
      return null;
    }

    public void AddEventDb(Event newEvent)
    {
      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string insertQuery = "INSERT INTO aemapp_events (name, description, date, apprentices_required) VALUES (@name, @description, @date, @totalApprenticesRequired)";
        using (var command = new SqliteCommand(insertQuery, connection))
        {
          command.Parameters.AddWithValue("@name", newEvent.Name);
          command.Parameters.AddWithValue("@description", newEvent.Description);
          command.Parameters.AddWithValue("@date", newEvent.Date.ToString());
          command.Parameters.AddWithValue("@totalApprenticesRequired", newEvent.TotalApprenticesRequired);
          command.ExecuteNonQuery();
        }
      }
    }

    public bool UpdateEventDb(Event updatedEvent)
    {
      Event eventCheck = GetEventById(updatedEvent.Id);
      if (eventCheck == null)
      {
        return false;
      }

      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string updateQuery = "UPDATE aemapp_events SET event_name = @name, event_description = @description, event_date = @date, total_apprentices_required = @totalApprenticesRequired WHERE event_id = @eventId";
        using (var command = new SqliteCommand(updateQuery, connection))
        {
          command.Parameters.AddWithValue("@name", updatedEvent.Name);
          command.Parameters.AddWithValue("@description", updatedEvent.Description);
          command.Parameters.AddWithValue("@date", updatedEvent.Date.ToString());
          command.Parameters.AddWithValue("@totalApprenticesRequired", updatedEvent.TotalApprenticesRequired);
          command.Parameters.AddWithValue("@eventId", updatedEvent.Id);
          command.ExecuteNonQuery();
        }
      }
      return true;
    }

    public bool RemoveEventDb(Event updatedEvent)
    {
      Event eventCheck = GetEventById(updatedEvent.Id);

      if (eventCheck == null)
      {
        return false;
      }

      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string deleteQuery = "DELETE FROM aemapp_events WHERE event_id = @eventId";
        using (var command = new SqliteCommand(deleteQuery, connection))
        {
          command.Parameters.AddWithValue("@eventId", updatedEvent.Id);
          command.ExecuteNonQuery();
        }
      }
      return true;
    }

  }
}
