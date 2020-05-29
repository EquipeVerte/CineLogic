using CineLogic.Business.Contenus;
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

    }
}