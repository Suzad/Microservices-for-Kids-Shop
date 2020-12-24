using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductService.Database.Entity;

namespace ProductService.Database
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Product> products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql("server=127.0.0.1;port=3306;user=suzad;password=suzad;database=ProductDB")
                .UseLoggerFactory(LoggerFactory)
        }
    }
}
