using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMS.Models;

namespace SMS.DatabaseContext
{
    public class StudentContext:IdentityDbContext
    {
        public StudentContext(DbContextOptions opt):base(opt)
        {
            
        }
        public DbSet<StudentDetails > StudentDetails { get; set; }
    }
}
