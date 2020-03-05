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
    public class DetailsModel : PageModel
    {
        private readonly ElasticDatabaseTransactions.Data.Database1Context _context1;
        private readonly ElasticDatabaseTransactions.Data.Database2Context _context2;

        public DetailsModel(ElasticDatabaseTransactions.Data.Database1Context context1, ElasticDatabaseTransactions.Data.Database2Context context2)
        {
            _context1 = context1;
            _context2 = context2;
        }

        public Person Person { get; set; }
        public List<DepartmentAssignment> DepartmentAssignments { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person = await _context1.Person.FirstOrDefaultAsync(m => m.ID == id);

            if (Person == null)
            {
                return NotFound();
            }
            else
            {
                DepartmentAssignments = await _context2.DepartmentAssignments.Where(m => m.PersonID == id).ToListAsync();
            }

            return Page();
        }
    }
}
