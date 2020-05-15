using CineLogic.Models.Libraries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CineLogic.Models
{
    [MetadataType(typeof(ContenuMetaData))]
    public partial class Contenu
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
        [DisplayFormat(DataFormatString = "{0:yyyy}")]
        [Range(1895, 2020, ErrorMessage = "L'année de production doit être entre {1} and {2}.")]
        public int Annee { get; set; }
        [Display(Name =NameLibrary.CONTENU_DISP_RuntimeMins)]
        public int RuntimeMins { get; set; }
        [Display(Name = NameLibrary.CONTENU_DISP_Rating)]
        public Nullable<decimal> Rating { get; set; }
        public Nullable<int> Votes { get; set; }
        public Nullable<decimal> Revenue { get; set; }
        public Nullable<int> MetaScore { get; set; }
    }
}