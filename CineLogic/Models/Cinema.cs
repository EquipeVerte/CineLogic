//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CineLogic.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cinema
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cinema()
        {
            this.Salles = new HashSet<Salle>();
        }
    
        public int CinemaID { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public bool EnExploitation { get; set; }
        public int ResponsableID { get; set; }
        public string Programmateur { get; set; }
    
        public virtual Responsable Responsable { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Salle> Salles { get; set; }
    }
}
