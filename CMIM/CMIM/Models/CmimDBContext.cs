using CMIM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMIM.Models
{
	public class CmimDBContext : DbContext
    { 
		public CmimDBContext(DbContextOptions<CmimDBContext> options) : base(options) { }
		public DbSet<Employee> employees { get; set; }
		public DbSet<Dossier> Dossiers { get; set; }
		public DbSet<ActivityPlace> ActivityPlace { get; set; }
		public DbSet<Conjoint> Conjoint { get; set; }
		public DbSet<Enfant> Enfant { get; set; }
		public DbSet<Bordereau> Bordereau { get; set; }
        public DbSet<Regularisation> Regularisation { get; set; }
        public DbSet<BU> bu { get; set; }
        public DbSet<Site> sites { get; set; }
        public DbSet<Regul_Emp> Regul_Emp {get;set;}
        public DbSet<Rembouse> Remboursser { get; set; }
        public DbSet<Rembouslist> list { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
