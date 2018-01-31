using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HR.WebApi.Data.Contexts
{
    public class HRDBContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public HRDBContext() : base("name=HRDBContext")
        {
        }
        public System.Data.Entity.DbSet<HR.WebApi.Data.Models.Employee> Employees { get; set; }
        public System.Data.Entity.DbSet<HR.WebApi.Data.Models.Orgunit> Orgunits { get; set; }
        public System.Data.Entity.DbSet<HR.WebApi.Data.Models.Position> Positions { get; set; }
        public System.Data.Entity.DbSet<HR.WebApi.Data.Models.Relation> Relations { get; set; }
        public System.Data.Entity.DbSet<HR.WebApi.Data.Models.EmployeeBase> EmployeeBases { get; set; }
        public System.Data.Entity.DbSet<HR.WebApi.Data.Models.EmployeeExpand> EmployeeExpands { get; set; }
        public System.Data.Entity.DbSet<HR.WebApi.Data.Models.BankCards> BankCard { get; set; }
        public System.Data.Entity.DbSet<HR.WebApi.Data.Models.Performance> Performances { get; set; }

    }
}
