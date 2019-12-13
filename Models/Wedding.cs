using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
  public class Wedding
  {
    [Key]
    public int WeddingId { get; set; }

    [Required(ErrorMessage = "is required.")]
    [MinLength(2, ErrorMessage = "must be at least 2 characters.")]
    [Display(Name = "WedderOne")]
    public string WedderOne { get; set; }
    [Required(ErrorMessage = "is required.")]
    [MinLength(2, ErrorMessage = "must be at least 2 characters.")]
    [Display(Name = "WedderTwo")]
    public string WedderTwo { get; set; }
    [DataType(DataType.Date)]
    public DateTime DateOfWedding { get; set; }
    public string Address { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int UserId { get; set; }
    public User Creator { get; set; }
    public List<RSVP> GuestList { get; set; }






  }
}
