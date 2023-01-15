using FileProcessor.Classes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace FileProcessor
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("TodoContext")
        {
        }

        public DbSet<EmpData> Emp_Prediction { get; set; }

    }
}
