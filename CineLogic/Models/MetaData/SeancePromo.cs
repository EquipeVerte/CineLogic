using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CineLogic.Models
{
    [MetadataType(typeof(SeancePromoMetaData))]
    public partial class SeancePromo : ISeanceContenu
    {
        public string ContenuTitre { get => PromoTitre; set => PromoTitre = value; }
        public bool? estPrincipal { get => false ; }

        public IContenu GetContenu()
        {
            return ContenuPromo;
        }
    }

    public class SeancePromoMetaData
    {

    }
}