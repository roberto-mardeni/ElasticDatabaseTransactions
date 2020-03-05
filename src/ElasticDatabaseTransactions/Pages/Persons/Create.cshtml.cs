using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ElasticDatabaseTransactions.Data;
using ElasticDatabaseTransactions.Models;

namespace ElasticDatabaseTransactions
{
    public class CreateModel : PageModel
    {
        private readonly ElasticDatabaseTransactions.Data.Database1Context _context1;
        private readonly ElasticDatabaseTransactions.Data.Database2Context _context2;

        public CreateModel(ElasticDatabaseTransactions.Data.Database1Context context1,
            ElasticDatabaseTransactions.Data.Database2Context context2)
        {
            _context1 = context1;
            _context2 = context2;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Person Person { get; set; }

        [BindProperty]
        public string Departments { get; set; }


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context1.Person.Add(Person);
            await _context1.SaveChangesAsync();

            if (!string.IsNullOrEmpty(Departments))
            {
                foreach(string department in Departments.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    _context2.DepartmentAssignments.Add(new DepartmentAssignment { PersonID = Person.ID, DepartmentName = department });
                }

                await _context2.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
