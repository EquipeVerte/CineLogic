using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CineLogic.Models
{
    [MetadataType(typeof(SeanceMetaData))]
    public partial class Seance
    {
        
    }

    public class SeanceMetaData
    {
        [Display(Name = "Heure de début")]
        public DateTime HeureDebut;
    }
}