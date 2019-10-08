using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CMIM.Models
{
	public class Conjoint
	{
		[Key]
		public long ConjointId { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public virtual Employee Employee { get; set; }
		public virtual ICollection<Enfant> Enfants { get; set; }
		public string Employeematricule { get; set; }
        public string matriculecmim { get; set; }
        public DateTime DateNs { get; set; }
		public Conjoint() { }
	}
}
