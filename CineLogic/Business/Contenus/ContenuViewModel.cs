using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Business.Contenus
{
    public class ContenuViewModel : IComparable
    {
        //  Idea on how to pass data to view.
        //  Ce viewmodel va régrouper tous les types de contenu.

        public string ContenuTitre { get; set; }
        public int SeanceID { get; set; }
        public int RuntimeMins { get; set; }
        public string Typage { get; set; }
        public int indexOrdre { get; set; }
        public bool estPrincipal { get; set; }

        //  Customisé pour ordonné les séances.
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            ContenuViewModel otherCVM = obj as ContenuViewModel;

            if (otherCVM != null)
            {
                return this.indexOrdre.CompareTo(otherCVM.indexOrdre);
            }
            else
            {
                throw new ArgumentException("L'objet n'est pas un ContenuViewModel.");
            }
        }
    }
}