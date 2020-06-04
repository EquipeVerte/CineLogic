using CineLogic.Models.Libraries;
using System.ComponentModel.DataAnnotations;

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