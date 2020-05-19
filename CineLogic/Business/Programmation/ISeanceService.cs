using CineLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineLogic.Business.Programmation
{
    public interface ISeanceService
    {
        IEnumerable<SeanceViewModel> GetSeancesBySalle(int salleID);

        SeanceViewModel GetSeance(int id);

        SeanceViewModel CreateSeance(SeanceViewModel seance);

        SeanceViewModel UpdateSeance(SeanceViewModel seance);

        void UpdateSeanceTimes(SeanceViewModel seance);

        void DeleteSeance(int id);

        void SaveChanges();

        void Annuler();

        void Dispose();
    }
}
