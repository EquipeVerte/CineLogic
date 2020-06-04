using System.ComponentModel.DataAnnotations;

namespace CineLogic.Models
{
    [MetadataType(typeof(SeanceContenuMetaData))]
    public partial class SeanceContenu : ISeanceContenu
    {
        public IContenu GetContenu()
        {
            return Contenu;
        }
    }

    public class SeanceContenuMetaData
    {

    }
}