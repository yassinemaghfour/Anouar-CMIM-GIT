using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CMIM.Models
{
    public class Company
    {
       
            [Key]
            public string CompanyId { get; set; }
            public string name { get; set; }
            public virtual ICollection<Company> Companies { get; set; }
            public virtual ICollection<Employee> Employees { get; set; }

    }
}
