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
        [Required(ErrorMessage = "Le titre du séance est requis.")]
        [MinLength(3, ErrorMessage = "La longeur minimum du titre est 3.")]
        [MaxLength(20, ErrorMessage = "La longeur maximum du titre est 20.")]
        public string Titre { get; set; }
        [Required(ErrorMessage = "L'heure du début est requis.")]
        public System.DateTime HeureDebut { get; set; }
        [Required(ErrorMessage = "L'heure du fin est requis.")]
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
            //List<SeanceViewModel> conflicts = seanceService.GetSeancesBySalle(SalleID).Any(s => s.HeureDebut >= HeureDebut && s.HeureDebut < HeureFin || s.HeureFin <= HeureFin && s.HeureFin > HeureDebut).ToList();
            //List<SeanceViewModel> conflicts = seanceService.GetSeancesBySalle(SalleID).Where(s => (s.HeureDebut > HeureDebut && s.HeureDebut < HeureFin || s.HeureFin < HeureFin && s.HeureFin > HeureDebut)).ToList();
            if (seanceService.GetSeancesBySalle(SalleID).Any(s => s.SeanceID != SeanceID && (s.HeureDebut >= HeureDebut && s.HeureDebut < HeureFin || s.HeureFin <= HeureFin && s.HeureFin > HeureDebut)))
            {
                return false;
            }

            return true;
        }
    }
}