using CineLogic.Models.Libraries;
using System.ComponentModel.DataAnnotations;

namespace CineLogic.Models
{
    [MetadataType(typeof(ResponsableMetaData))]
    public partial class Responsable
    {
    }

    public class ResponsableMetaData
    {
        public int ResponsableID { get; set; }
        [Required(ErrorMessage = ValidationLibrary.ERR_REQUIS)]
        [Display(Name = "Nom Complet")]
        public string Nom { get; set; }
        [Required(ErrorMessage = ValidationLibrary.ERR_REQUIS)]
        [Display(Name = "Numéro de téléphone")]
        public string NumTel { get; set; }
        [Required(ErrorMessage = ValidationLibrary.ERR_REQUIS)]
        [Display(Name = "Adresse courriel")]
        public string Email { get; set; }
    }
}