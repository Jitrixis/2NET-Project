using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Restaurant.Database
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<MealString> Strings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Waiter>().HasMany(p => p.Bills);
            modelBuilder.Entity<Table>().HasMany(t => t.Bills);
        }
    }
}