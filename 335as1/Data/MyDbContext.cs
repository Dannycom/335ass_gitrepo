using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _335as1.Models;


namespace _335as1.Data {
    class MyDbContext : DbContext {
        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Product> Products { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder) {
            optionBuilder.UseSqlite("Data Source=MyDatabase.sqlite");
        }
    }
}
