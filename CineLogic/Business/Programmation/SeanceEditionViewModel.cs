using CineLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Business.Programmation
{
    public class SeanceEditionViewModel : SeanceViewModel
    {
        public List<Contenu> Contenus { get; set; }
    }
}