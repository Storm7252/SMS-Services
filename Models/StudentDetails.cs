using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Models
{
    public class StudentDetails
    {
        [Key]
        public int StudentId { get; set; }
        public string FathersName { get; set; }

        public string Address { get; set; }

        public long PhoneNumber { get; set; }

        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        

    }
}
