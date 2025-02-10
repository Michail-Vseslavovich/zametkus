using Microsoft.EntityFrameworkCore;
using Zametki_service.domain.entity;

namespace Zametki_service.infrastructure.DBcontext
{
    public class NoteBDcontext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public NoteBDcontext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=usersdb;Username=postgres;Password=здесь_указывается_пароль_от_postgres");
        }
    }
}
