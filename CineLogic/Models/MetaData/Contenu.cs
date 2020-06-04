using CineLogic.Models.Libraries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CineLogic.Models
{
    [MetadataType(typeof(ContenuMetaData))]
    public partial class Contenu : IContenu
    {
    }
    public class ContenuMetaData
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = ValidationLibrary.ERR_REQUIS)]
        public string Titre { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = ValidationLibrary.ERR_REQUIS)]
        public string Description { get; set; }
        [Required(ErrorMessage = ValidationLibrary.ERR_REQUIS)]
        [Range(1895, 2020, ErrorMessage = "L'année de production doit être entre {1} and {2}.")]
        [Display(Name = NameLibrary.CONTENU_DISP_Annee)]
        public int Annee { get; set; }
        [Display(Name =NameLibrary.CONTENU_DISP_RuntimeMins)]
        [Range(1, 240, ErrorMessage = "La duré d’un film ne peu être que entre {1} and {2} min.")]
        public int RuntimeMins { get; set; }
        [Display(Name = NameLibrary.CONTENU_DISP_Rating)]
        [Range(0, 10, ErrorMessage = "Le Classement ne peu être que entre {1} and {2}.")]
        public Nullable<decimal> Rating { get; set; }
        [Range(0, 2000000000, ErrorMessage = "Les votes ne peuvent être que entre {1} and {2}.")]
        public Nullable<int> Votes { get; set; }
        [Range(0, 9999, ErrorMessage = "Le revene ne peu être que entre {1} and {2}.")]
        public Nullable<decimal> Revenue { get; set; }
        [Range(0, 100, ErrorMessage = "Le MetaScore ne peu être que entre {1} and {2}.")]
        public Nullable<int> MetaScore { get; set; }
        [Display(Name =NameLibrary.CONTENU_DISP_Type)]
        public string typage { get; set; }
    }
}