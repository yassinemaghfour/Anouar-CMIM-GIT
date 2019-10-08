using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CMIM.Models
{
	public class Bordereau
	{
		[Key]
		public long BordereauId { get; set; }
		public virtual ICollection<Dossier> Dossiers { get; set; }
		private string company;
        private DateTime dateCreation;



		public string Company
		{
			get { return company; }

			set
			{
				if (value.ToUpper() != "VEM" && value.ToUpper() != "VEAS" && value.ToUpper() != "SVL")
					throw new Exception("Invalide Company");
				else
					this.company = value.ToUpper();
			}
		}

        public DateTime DateCreation {
            get => dateCreation;
            set
            {
                this.dateCreation = DateTime.Now;
            }
        }

        public int CompareTo(object obj)
        {
            Bordereau d = (Bordereau)obj;
            if (this.BordereauId == d.BordereauId)
                return 0;
            return 1;
        }


    }
}
