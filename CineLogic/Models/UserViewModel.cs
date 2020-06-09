using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CineLogic.Models
{
    public class UserViewModel
    {
        [Required]
        [DisplayName("Login")]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Mot de passe")]
        public string Password { get; set; }
        [DisplayName("Nom complet")]
        [Required]
        public string NomComplet { get; set; }
        public string Type { get; set; }
    }
}