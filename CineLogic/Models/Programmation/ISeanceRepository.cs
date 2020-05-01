using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineLogic.Models.Programmation
{
    public interface ISeanceRepository
    {
        void CreateSeance(Seance seance);

        void DeleteSeance(int id);

        Seance GetSeance(int id);

        IEnumerable<Seance> GetAllSeances();

        IEnumerable<Seance> GetSeancesBySalle(int salleID);

        void UpdateSeance(Seance seance);

        int SaveChanges();

        IEnumerable<CinemaSelectionItem> GetCinemas();

        IEnumerable<SalleSelectionItem> GetSalles(int cinemaID);

        bool FindSeanceConflicts(Seance seance);

        void Dispose();
    } 
}
