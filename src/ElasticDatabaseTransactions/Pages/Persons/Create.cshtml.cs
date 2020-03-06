using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ElasticDatabaseTransactions.Models;
using Microsoft.Extensions.Configuration;
using System.Transactions;
using System.Data.SqlClient;

namespace ElasticDatabaseTransactions
{
    public class CreateModel : PageModel
    {
        private readonly ElasticDatabaseTransactions.Data.Database1Context _context1;
        private readonly ElasticDatabaseTransactions.Data.Database2Context _context2;
        private readonly IConfiguration _configuration;

        public CreateModel(ElasticDatabaseTransactions.Data.Database1Context context1,
            ElasticDatabaseTransactions.Data.Database2Context context2,
            IConfiguration configuration)
        {
            _context1 = context1;
            _context2 = context2;
            _configuration = configuration;
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

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                decimal? id = null;

                try
                {
                    using (var conn1 = new SqlConnection(_configuration.GetConnectionString("Database1Context")))
                    {
                        conn1.Open();
                        SqlCommand cmd1 = conn1.CreateCommand();
                        cmd1.CommandText = string.Format("INSERT INTO Person ([FirstName],[LastName]) VALUES (@FirstName, @LastName); SELECT @@IDENTITY as LastIdentityValue;");
                        cmd1.Parameters.AddWithValue("@FirstName", Person.FirstName);
                        cmd1.Parameters.AddWithValue("@LastName", Person.LastName);
                        id = (decimal?)await cmd1.ExecuteScalarAsync();
                    }
                }
                catch (Exception ex) { }

                using (var conn2 = new SqlConnection(_configuration.GetConnectionString("Database2Context")))
                {
                    conn2.Open();

                    foreach (string department in Departments.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    {
                        var cmd2 = conn2.CreateCommand();
                        cmd2.CommandText = string.Format("INSERT INTO DepartmentAssignments ([PersonID],[DepartmentName]) VALUES (@PersonID, @DepartmentName);");
                        cmd2.Parameters.AddWithValue("@PersonID", id.Value);
                        cmd2.Parameters.AddWithValue("@DepartmentName", department);
                        await cmd2.ExecuteNonQueryAsync();
                    }
                }

                scope.Complete();
            }

            return RedirectToPage("./Index");
        }
    }
}
