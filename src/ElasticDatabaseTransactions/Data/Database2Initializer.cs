using ElasticDatabaseTransactions.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElasticDatabaseTransactions.Data
{
    public static class Database2Initializer
    {
        public static void Initialize(Database1Context database1Db, Database2Context context)
        {
            context.Database.EnsureCreated();

            // Look for any department assignments.
            if (context.DepartmentAssignments.Any())
            {
                return;   // DB has been seeded
            }
            // Look for persons available to seed
            if (!database1Db.Person.Any())
            {
                return;
            }

            var dps = new List<DepartmentAssignment>();

            foreach (Person p in database1Db.Person)
            {
                dps.Add(new DepartmentAssignment { PersonID = p.ID, DepartmentName = "IT" });
            }

            foreach (DepartmentAssignment s in dps)
            {
                context.DepartmentAssignments.Add(s);
            }

            context.SaveChanges();
        }
    }
}