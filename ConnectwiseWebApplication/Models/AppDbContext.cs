using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ConnectwiseWebApplication.Models
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext>  dbContext) : base(dbContext) 
        {
            

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Reminder> Reminder { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<ReminderLog> ReminderLog { get; set; }
        public DbSet<MasterEmail> MasterEmail { get; set; }

        
    }
}
