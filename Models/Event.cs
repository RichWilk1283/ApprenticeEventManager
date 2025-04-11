using System.ComponentModel.DataAnnotations;

namespace ApprenticeEventManager.Models
{
  public class Event : DbModel
  {
    [Required]
    [StringLength(50, ErrorMessage = "Too Long.")]
    public string Name { get; set; }
    public string Description { get; set; }
    [FutureDate]
    [NoWeekends]
    [DataType(DataType.Date)]
    public DateOnly Date { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number.")]
    public int? TotalApprenticesRequired { get; set; }
  }
}

public class  FutureDateAttribute : ValidationAttribute
{
  protected override ValidationResult IsValid(object value, ValidationContext validationContext)
  {
    if (value == null)
    {
      return new ValidationResult("A future date is required");
    }

    if (value is DateOnly dateValue)
    {
      if (dateValue < DateOnly.FromDateTime(DateTime.Now))
      {
        return new ValidationResult("The date must be in the future.");
      }
    }
    return ValidationResult.Success;
  }
}

public class NoWeekendsAttribute : ValidationAttribute
{
  protected override ValidationResult IsValid(object value, ValidationContext validationContext)
  {
    if (value is DateOnly dateValue)
    {
      if (dateValue.DayOfWeek == DayOfWeek.Saturday || dateValue.DayOfWeek == DayOfWeek.Sunday)
      {
        return new ValidationResult("The date cannot be a weekend.");
      }
    }
    return ValidationResult.Success;
  }
}
