using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TechNeuronstask.Models
{
    public class Employee
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive number")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(maximumLength:255,ErrorMessage ="Description maxmimum length should be 255")]
        public string Description { get; set; }
    }
    
}
