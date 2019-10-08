using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CMIM.Models
{
    public class BU
    {
        [Key]
        public string BuId { get; set; }
        public string name { get; set; }    
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
