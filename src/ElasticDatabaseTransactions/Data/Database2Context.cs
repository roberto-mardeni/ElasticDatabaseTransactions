using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElasticDatabaseTransactions.Models;

namespace ElasticDatabaseTransactions.Data
{
    public class Database2Context : DbContext
    {
        public Database2Context (DbContextOptions<Database2Context> options)
            : base(options)
        {
        }

        public DbSet<ElasticDatabaseTransactions.Models.DepartmentAssignment> DepartmentAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentAssignment>().ToTable("DepartmentAssignments");
        }
    }
}
