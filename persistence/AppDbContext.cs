using Library_web_API.persistence.model;
using Microsoft.EntityFrameworkCore;

namespace Library_web_API.persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base (options) { }

        public DbSet<Book> Books => Set<Book>();

        public DbSet<Reservation> Reservations => Set<Reservation>();
    }
}
