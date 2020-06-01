using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CineLogic.Models
{
    public class SalleViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalleViewModel()
        {
            this.Seances = new HashSet<Seance>();
        }

        [Required]
        public int SalleID { get; set; }
        [Required(ErrorMessage = "Le nom de la salle est requis.")]
        [MinLength(1, ErrorMessage = "La longueur minimale du nom est 1 caractère.")]
        [MaxLength(100, ErrorMessage = "La longueur maximale du nom est 100 caractères.")]
        [DisplayName("Nom")]
        public string Nom { get; set; }
        [DisplayName("Type d'écran")]
        public string TypeEcran { get; set; }
        [DisplayName("Système de son")]
        public string SystemSon { get; set; }
        [DisplayName("En exploitation")]
        public bool EnExploitation { get; set; }
        [Required]
        public int CinemaID { get; set; }

        public virtual Cinema Cinema { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seance> Seances { get; set; }
    }
}