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
    
    public partial class Seance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Seance()
        {
            this.SeanceContenus = new HashSet<SeanceContenu>();
            this.SeancePromoes = new HashSet<SeancePromo>();
        }
    
        public int SeanceID { get; set; }
        public string Titre { get; set; }
        public System.DateTime HeureDebut { get; set; }
        public System.DateTime HeureFin { get; set; }
        public int SalleID { get; set; }
    
        public virtual Salle Salle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SeanceContenu> SeanceContenus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SeancePromo> SeancePromoes { get; set; }
    }
}
