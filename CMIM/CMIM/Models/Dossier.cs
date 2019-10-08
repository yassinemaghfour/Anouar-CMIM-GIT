using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CMIM.Models
{
	public class Dossier : IComparable
	{
		[Key]
		public string referance { get; set; }
		public string employeematricule { get; set; }
        public long? EnfantId { get; set; }
        public long? ConjointId { get; set; }
        public virtual Employee employee { get; set; }
        public virtual Conjoint conjoint { get; set; }
        public virtual Enfant enfant { get; set; }
        public DateTime date { get; set; }
        public DateTime dateDossier { get; set; }
        public DateTime? dateLunette { get; set; }
        public double montant { get; set; }
        public double Mt_radio { get; set; }
        public double Mt_analyse { get; set; }
        public double MtSoins_D     { get; set; }
        public double MtProthese_D { get; set; }
        public double Devis_D { get; set; }
        public double MtVisite_M { get; set; }
        public double MtPharmacie_M { get; set; }
        public double MtVisite_L { get; set; }
        public double MtCadre_L { get; set; }
        public double MtGi_L { get; set; }
       


        public double avance { get; set; }
		public double rembourse { get; set; }
		public double diff { get; set; }
		private string type;
		private string Etat;
        public long? rembourssementId { get; set; }
        public virtual Rembouse Rembouse { get; set; }
        public virtual Rembouslist Rembouslist { get; set; }

        public long? BordereauId {get;set;}


		public string etat
		{
			get { return this.Etat; }
			set
			{
				if (value != "Ouvert" && value != "Remboursé" && value != "Avancé" && value != "Envoyé à la CMIM" && value != "Régularisé")
					throw new Exception("Invalid state");

				this.Etat = value;
			}
		}

		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				if (value.ToLower() != "dontaire" || value.ToLower() != "medical" || value.ToLower() != "radiologie" || value.ToLower() != "analyses" || value.ToLower() != "lunettes")
					throw new Exception("Invalide Type");

				this.type = value;
			}
		}

        public virtual User User { get; set; }
        public Int64 UserId { get; set; }

        public int CompareTo(object obj)
		{
			Dossier d = (Dossier)obj;
			if (d.referance == d.referance)
				return 0;
			return 1;
		}
	}
}
