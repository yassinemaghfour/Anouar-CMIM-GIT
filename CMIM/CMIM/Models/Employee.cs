using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CMIM.Models
{
	public class Employee
	{
		[Key]
		public string matricule { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string adresse { get; set; }
        public string matriculecmim { get; set; }

        private string Company;

        public string company
        {
            get { return Company; }
            set
            {
                if (value.ToUpper() != "VEM" && value.ToUpper() != "VEAS" && value.ToUpper() != "SVL" && value.ToUpper() != "SVLAS")
                    throw new Exception("Invalide Company");
                else
                    this.Company = value.ToUpper();
            }
        }
        public string buId { get; set; }
        public Int64 PlaceplacdeId { get; set; }

        public Int64 UserId { get; set; }


        public virtual ICollection<Dossier> dossies { get; set; }
		public virtual ICollection<Enfant> enfants { get; set; }
		public virtual ICollection<Conjoint> conjoints { get; set; }

		public virtual ICollection<Regul_Emp> regul_emp {get;set;}
		        
        public virtual ActivityPlace Place { get; set; }
        public virtual BU bu { get; set; }
       
        public virtual User User { get;set; }

     
    }
}
