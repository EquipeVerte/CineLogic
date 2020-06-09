using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CineLogic.Models
{
    public class CinemaViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CinemaViewModel()
        {
            this.Salles = new HashSet<Salle>();
        }

        [Required]
        public int CinemaID { get; set; }
        [Required(ErrorMessage = "Le nom du cinéma est requis.")]
        [MinLength(1, ErrorMessage = "La longueur minimale du nom est 1 caractère.")]
        [MaxLength(100, ErrorMessage = "La longueur maximale du nom est 100 caractères.")]
        [DisplayName("Nom")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "L'adresse du cinéma est requis.")]
        [MinLength(1, ErrorMessage = "La longueur minimale de l'adresse est 1 caractère.")]
        [MaxLength(200, ErrorMessage = "La longueur maximale de l'adresse est 200 caractères.")]
        [DisplayName("Adresse")]
        public string Adresse { get; set; }
        [DisplayName("En exploitation")]
        public bool EnExploitation { get; set; }
        [Required]
        [DisplayName("Nom du responsable")]
        public int ResponsableID { get; set; }
        public string Programmateur { get; set; }

        public virtual Responsable Responsable { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Salle> Salles { get; set; }
    }
}