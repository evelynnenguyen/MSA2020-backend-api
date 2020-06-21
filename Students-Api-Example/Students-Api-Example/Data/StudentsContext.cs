using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Students_Api_Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students_Api_Example.Data
{
    public class StudentsContext : DbContext
    {
        public StudentsContext()
        {

        }
        public StudentsContext(DbContextOptions<StudentsContext> options)
        : base(options)
        { 
        
        }

        public DbSet<Student> Students { get; set; }
    }
}
