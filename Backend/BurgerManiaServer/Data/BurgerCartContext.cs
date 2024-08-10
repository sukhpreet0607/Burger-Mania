using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BurgerManiaServer.Models;

namespace BurgerManiaServer.Data
{
    public class BurgerCartContext : DbContext
    {
        public BurgerCartContext (DbContextOptions<BurgerCartContext> options)
            : base(options)
        {
        }

        public DbSet<BurgerManiaServer.Models.BurgerCart> BurgerCart { get; set; } = default!;
    }
}
