using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;

namespace Database.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Database.Citys> Citys { get; set; }

        public DbSet<Database.Users> Users { get; set; }

        public DbSet<Database.items> Items { get; set; }

        public DbSet<Database.Prices> Prices { get; set; }

        public DbSet<Database.Categorys> Categorys { get; set; }

        public DbSet<Database.Images> Images { get; set; }
    }
}
