using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CMIM.Models
{
	public class ActivityPlace
	{
		[Key]
		public long placdeId { get; set; }
		public string name { get; set; }
		public virtual ICollection<Employee> Employees { get; set; }
	}
}
