using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CineLogic.Business.Programmation
{
    public class SeanceViewModel
    {
        [Required]
        public int SeanceID { get; set; }
        [Required]
        public string Titre { get; set; }
        [Required]
        public System.DateTime HeureDebut { get; set; }
        [Required]
        public System.DateTime HeureFin { get; set; }
        [Required]
        public int SalleID { get; set; }
        public string ContenuTitre { get; set; }

        public bool Validate(ISeanceService seanceService)
        {
            if (HeureFin <= HeureDebut)
            {
                return false;
            }
            if (seanceService.GetSeancesBySalle(SalleID).Any(s => s.HeureDebut > HeureDebut && s.HeureDebut < HeureFin || s.HeureFin < HeureFin && s.HeureFin > HeureDebut))
            {
                return false;
            }

            return true;
        }
    }
}
}