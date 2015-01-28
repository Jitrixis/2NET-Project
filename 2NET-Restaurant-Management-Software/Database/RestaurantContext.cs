using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace _2NET_Restaurant_Management_Software.Database
{
    class RestaurantContext : DbContext
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Waiter> Waiters { get; set; }
    }
}
