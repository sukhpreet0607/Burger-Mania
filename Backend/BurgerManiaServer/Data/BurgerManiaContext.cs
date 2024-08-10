using Microsoft.EntityFrameworkCore;
using BurgerManiaServer.Models;

namespace BurgerManiaServer.Data
{
    public class BurgerManiaContext : DbContext
    {
        public BurgerManiaContext (DbContextOptions<BurgerManiaContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
