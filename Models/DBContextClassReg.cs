using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplicationAuthenticate.Models
{
    public class DBContextClassReg:DbContext
    {
        public DBContextClassReg():base("Reg Db") 
        {
        
        }
        public DbSet<Reg>Regs { get; set; }
    }
}