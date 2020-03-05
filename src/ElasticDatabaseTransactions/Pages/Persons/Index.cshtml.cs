using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ElasticDatabaseTransactions.Data;
using ElasticDatabaseTransactions.Models;

namespace ElasticDatabaseTransactions
{
    public class IndexModel : PageModel
    {
        private readonly ElasticDatabaseTransactions.Data.Database1Context _context;

        public IndexModel(ElasticDatabaseTransactions.Data.Database1Context context)
        {
            _context = context;
        }

        public IList<Person> Person { get;set; }

        public async Task OnGetAsync()
        {
            Person = await _context.Person.ToListAsync();
        }
    }
}
