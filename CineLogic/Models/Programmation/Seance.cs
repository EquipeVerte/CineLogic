using CineLogic.Business;
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
        public int SeanceID { get; set; }

        [Display(Name = "Titre de la séance")]
        [StringLength(20)]
        public string Titre { get; set; }

        [Display(Name = "Heure de début")]
        public DateTime HeureDebut;

        [Display(Name = "Heure de fin")]
        public System.DateTime HeureFin { get; set; }

        public int SalleID { get; set; }

        [Display(Name = "Titre de la film")]
        public string ContenuTitre { get; set; }

        
    }
}