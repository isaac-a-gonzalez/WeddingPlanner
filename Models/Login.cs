using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
  [NotMapped]
  public class Login
  {
    [Required(ErrorMessage = "is required.")]
    [MinLength(8, ErrorMessage = "must be at least 8 characters.")]
    [DataType(DataType.EmailAddress)]  // auto fills the input type attribute
    public string LoginEmail { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string LoginPassword { get; set; }  // this means "don't add it to the DB(database)!" It would be a waste of space.
  }
}
