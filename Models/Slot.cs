namespace ApprenticeEventManager.Models
{
  public class Slot
  {
    public int Id { get; set; }
    public DateTime Start {  get; set; }
    public DateTime End { get; set; }
    public int RequiredApprentices { get; set; }
  }
}
