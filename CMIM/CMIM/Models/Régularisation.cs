using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMIM.Models
{
    public class Regularisation
    {
        [Key]
        public long RegularisationID { get; set; }
        public DateTime dateRegularisation { get; set; }
        public string Mois { get; set; }
        public virtual List<Regul_Emp> listDesRégularisation { get; set; }
        public double Total { get; set; }
    }
}
