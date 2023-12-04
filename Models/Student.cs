using System.ComponentModel.DataAnnotations;

namespace Using_Api_In_MVC.Models
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string? name { get; set; }
        [Required]
        public string? email { get; set; }

    }
}
