using CineLogic.Models.Libraries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CineLogic.Models
{
    [MetadataType(typeof(DirecteurMetadata))]
    public partial class Directeur
    {
    }
    public class DirecteurMetadata
    {
        [DataType(DataType.Text)]
        [Display(Name =NameLibrary.DIRECTEUR_DISP_Nom)]
        [Required(ErrorMessage = ValidationLibrary.ERR_REQUIS)]
        [StringLength(200, MinimumLength = 3, ErrorMessage = ValidationLibrary.ERR_REQUIS_NOM)]
        public string Nom { get; set; }
    }
}