using System.ComponentModel.DataAnnotations;

namespace ApprenticeEventManager.Components.Login.Models
{
  public class LoginFormModel
  {
    [Required]
    [StringLength(50, ErrorMessage = "Too Long.")]
    public string Email { get; set; }
    [StringLength(20, ErrorMessage = "Too Long.")]
    public string HashedPassword { get; set; }
  }
}
