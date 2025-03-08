using ApprenticeEventManager.Components.Login.Components;

namespace ApprenticeEventManager.Models
{
  public class LoginLog : DbModel
  {
    public DateTime LoginDate { get; set; }
    public bool Successful { get; set; }
    public string LoginEmail { get; set; }
    public string LoginHashedPassword { get; set; }
  }
}
