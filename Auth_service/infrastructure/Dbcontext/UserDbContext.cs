using Auth_service.domain.entity;
using Microsoft.EntityFrameworkCore;

namespace User_service.infrastructure.Dbcontext
{
    public class UserDbContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public UserDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=usersdb;Username=postgres;Password=здесь_указывается_пароль_от_postgres");
        }
        
    }
}
