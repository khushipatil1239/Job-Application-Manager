using Microsoft.EntityFrameworkCore;
using JobApplicationManager.Models;

namespace JobApplicationManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<JobApplication> JobApplications { get; set; }
    }
}
