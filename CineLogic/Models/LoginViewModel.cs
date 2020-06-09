using CineLogic.Models.Libraries;
using System.ComponentModel.DataAnnotations;


namespace CineLogic.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Idenifiant")]
        [Required(ErrorMessage = ValidationLibrary.ERR_REQUIS)]
        public string Login { get; set; }
        [Display(Name = "Mot de passe")]
        [Required(ErrorMessage = ValidationLibrary.ERR_REQUIS)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}