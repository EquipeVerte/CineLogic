using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Models.Programmation
{
    public class SeanceViewModel
    {
        public int SeanceID { get; set; }
        public string Titre { get; set; }
        public System.DateTime HeureDebut { get; set; }
        public System.DateTime HeureFin { get; set; }
        public int SalleID { get; set; }
        public string ContenuTitre { get; set; }
    }
}