using CineLogic.Business.Contenus;
using CineLogic.Models.Libraries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CineLogic.Models
{
    [MetadataType(typeof(PromoMetaData))]
    public partial class ContenuPromo : IContenu
    {
        public string typage { get => ContenuTypeLibrary.CONT_TYPE_PROMO; set { } }
    }

    public class PromoMetaData
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = ValidationLibrary.ERR_REQUIS)]
        public string Titre { get; set; }
        [Display(Name = NameLibrary.CONTENU_DISP_RuntimeMins)]
        [Range(1, 240, ErrorMessage = "La duré d’un film ne peu être que entre {1} and {2} min.")]
        public int RuntimeMins { get; set; }
    }
   
}