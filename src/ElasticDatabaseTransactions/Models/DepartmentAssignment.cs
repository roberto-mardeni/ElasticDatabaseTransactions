using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticDatabaseTransactions.Models
{
    public class DepartmentAssignment
    {
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonID { get; set; }
        public string DepartmentName { get; set; }
    }
}
