using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMIM.Models
{
    public class Regul_Emp
    {

        [Key]
        public long regul_empID { get; set; }
        public virtual Regularisation Régularisation { get; set; }
        public string EmployeeMatricule { get; set; }
       
        public virtual Employee Employee { get; set; }
        public double Total { get; set; }
    }
}
