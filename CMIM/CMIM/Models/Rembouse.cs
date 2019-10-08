using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMIM.Models
{
    public class Rembouse
    {
        [Key]
        public long rembourssementId { get; set; }
        public DateTime DateRembourssement { get; set; }
        public virtual List<Rembouslist> list { get; set; }
    }
}
