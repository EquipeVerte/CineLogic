using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineLogic.Models.Programmation
{
    //  Interface pour définir un repository des séances.
    public interface ISeanceRepository
    {
        void CreateSeance(Seance seance);

        void DeleteSeance(int id);

        Seance GetSeance(int id);

        IEnumerable<Seance> GetAllSeances();

        IEnumerable<Seance> GetSeancesBySalle(int salleID);

        void UpdateSeance(Seance seance);

        int SaveChanges();

        bool FindSeanceConflicts(Seance seance);

        void Dispose();

        // Ces méthodes seront remplacé par des méthodes dans autres repositories.
        IEnumerable<CinemaSelectionItem> GetCinemas();

        IEnumerable<SalleSelectionItem> GetSalles(int cinemaID);
    } 
}
