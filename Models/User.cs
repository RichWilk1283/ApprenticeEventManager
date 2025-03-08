using System.ComponentModel.DataAnnotations;

namespace ApprenticeEventManager.Models
{
  public class User : DbModel
  {
    [Required]
    [StringLength(50, ErrorMessage = "Too Long.")]
    public string FirstName { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Too Long.")]
    public string LastName { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Too Long.")]
    public string Email { get; set; }
    [StringLength(20, ErrorMessage = "Too Long.")]
    public string HashedPassword { get; set; }
  }
}
