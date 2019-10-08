using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMIM.Models
{
    public class User
    {
        [Key]
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Etat { get; set; }
        public string Token { get; set; }

        public Int64 siteId { get; set; }
        public virtual Site site { get; set; }          
        public string Role { get; set; }

    }
}
