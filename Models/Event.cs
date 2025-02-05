namespace ApprenticeEventManager.Models
{
  public class Event
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly Date { get; set; }
    public int TotalApprenticesRequired { get; set; }
  }
}
