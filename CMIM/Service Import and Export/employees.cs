//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Service_Import_and_Export
{
    using System;
    using System.Collections.Generic;
    
    public partial class employees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public employees()
        {
            this.Conjoint = new HashSet<Conjoint>();
            this.Dossiers = new HashSet<Dossiers>();
            this.Enfant = new HashSet<Enfant>();
            this.Regul_Emp = new HashSet<Regul_Emp>();
        }
    
        public string matricule { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string adresse { get; set; }
        public string matriculecmim { get; set; }
        public long PlaceplacdeId { get; set; }
        public string buId { get; set; }
        public string company { get; set; }
        public long UserId { get; set; }
    
        public virtual ActivityPlace ActivityPlace { get; set; }
        public virtual bu bu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Conjoint> Conjoint { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dossiers> Dossiers { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enfant> Enfant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Regul_Emp> Regul_Emp { get; set; }
    }
}
