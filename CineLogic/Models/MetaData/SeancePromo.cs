using System.ComponentModel.DataAnnotations;

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