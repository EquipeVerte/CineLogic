using CineLogic.Business.Contenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Business.Programmation
{
    public class SeanceEditionViewModel : SeanceViewModel
    {
        public List<ContenuViewModel> Contenus { get; set; }
        public string PrincipalFilm { get; set; }
    }
}