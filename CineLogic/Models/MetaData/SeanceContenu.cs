using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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