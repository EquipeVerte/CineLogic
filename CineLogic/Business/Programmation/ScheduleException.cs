using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Business.Programmation
{
    public class ScheduleException : Exception
    {
        public ScheduleException() : base("Les heures de début et fin ne sont pas valides. Vérifier que l'heure de début est avant l'heure de fin et qu'il n'y a pas de conflits pour la salle.")
        {
        }
    }
}