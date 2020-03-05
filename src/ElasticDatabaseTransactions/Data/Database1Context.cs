using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElasticDatabaseTransactions.Models;

namespace ElasticDatabaseTransactions.Data
{
    public class Database1Context : DbContext
    {
        public Database1Context (DbContextOptions<Database1Context> options)
            : base(options)
        {
        }

        public DbSet<ElasticDatabaseTransactions.Models.Person> Person { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
        }
    }
}
