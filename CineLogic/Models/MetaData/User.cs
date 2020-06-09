using CineLogic.Models.Libraries;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CineLogic.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
    }

    public class UserMetaData
    {
        [DisplayName("Identifiant")]
        [Required(ErrorMessage = ValidationLibrary.ERR_REQUIS)]
        public string Login { get; set; }
        [DisplayName("Nom complet")]
        [Required(ErrorMessage = ValidationLibrary.ERR_REQUIS)]
        public string NomComplet { get; set; }
        public string Type { get; set; }
    }
}