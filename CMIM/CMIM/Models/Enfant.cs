using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CMIM.Models
{
	public class Enfant
	{
		[Key]
		public long EnfantId { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public DateTime DateNs { get; set; }
		public DateTime DateVs { get; set; }
		public long ConjointId { get; set; }
		public virtual Conjoint Conjoint { get; set; }
		public string employeematricule { get; set; }
		public virtual Employee Employee { get; set; }
        public string MatriculeCmim { get; set; }
        public override string ToString()
		{
            return this.MatriculeCmim + " " + this.Firstname + " " + this.Lastname + " " + this.DateNs.ToShortDateString() + " " + this.DateVs.ToShortDateString() + " " + this.employeematricule + " " + this.ConjointId.ToString();
		}
	}
}
