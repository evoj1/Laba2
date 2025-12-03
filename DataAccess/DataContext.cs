using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DataContext() : base("name=AppDb")
        {

        }
    }
}
