using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMIM.Models
{
    public class Rembouslist
    {
        [Key]
        public long listId { get; set; }
        public long RembouserembourssementId { get; set; }
        public string Dossierreferance { get; set; }
        public virtual Rembouse Rembouse { get; set; }
        public virtual Dossier Dossier { get; set; }
        public double montant { get; set; }
    }
}
