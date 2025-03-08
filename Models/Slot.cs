namespace ApprenticeEventManager.Models
{
  public class Slot : DbModel
  {
    public DateTime Start {  get; set; }
    public DateTime End { get; set; }
    public int RequiredApprentices { get; set; }
  }
}
