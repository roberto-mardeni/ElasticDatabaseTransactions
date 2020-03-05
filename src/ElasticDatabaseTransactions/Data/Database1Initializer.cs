using ElasticDatabaseTransactions.Models;
using System;
using System.Linq;

namespace ElasticDatabaseTransactions.Data
{
    public static class Database1Initializer
    {
        public static void Initialize(Database1Context context)
        {
            context.Database.EnsureCreated();

            // Look for any persons.
            if (context.Person.Any())
            {
                return;   // DB has been seeded
            }

            var persons = new Person[]
            {
                new Person{FirstName="Carson",LastName="Alexander"},
                new Person{FirstName="Meredith",LastName="Alonso"},
                new Person{FirstName="Arturo",LastName="Anand"},
                new Person{FirstName="Gytis",LastName="Barzdukas"},
                new Person{FirstName="Yan",LastName="Li"},
            };
            foreach (Person s in persons)
            {
                context.Person.Add(s);
            }
            context.SaveChanges();
        }
    }
}