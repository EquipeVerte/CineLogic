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
    
    public partial class SeanceContenu
    {
        public string ContenuTitre { get; set; }
        public int SeanceID { get; set; }
        public Nullable<int> indexOrdre { get; set; }
        public Nullable<bool> estPrincipal { get; set; }
    
        public virtual Contenu Contenu { get; set; }
        public virtual Seance Seance { get; set; }
    }
}