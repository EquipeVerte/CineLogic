//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
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
