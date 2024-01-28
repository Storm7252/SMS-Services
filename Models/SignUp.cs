using System.ComponentModel.DataAnnotations;

namespace SMS.Models
{
    public class SignUp
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public Roles UserRole { get; set; }
        public enum Roles
        {
            Admin,
            Student

        }
    }
}
