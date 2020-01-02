using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace rdlcDemo.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base ("DefaultConnection")
        {

        }

        public DbSet <Employee> Employees { get; set; }
        public DbSet<MatrixDemo> MatrixDemo { get; set; }

    }
}