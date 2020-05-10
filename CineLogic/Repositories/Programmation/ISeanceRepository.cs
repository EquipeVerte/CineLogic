using CineLogic.Models;
using CineLogic.Models.Programmation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineLogic.Repositories
{
    //  Interface pour définir un repository des séances.
    public interface ISeanceRepository
    {
        void CreateSeance(Seance seance);

        void DeleteSeance(Seance seance);

        Seance GetSeance(int id);

        IEnumerable<Seance> GetAllSeances();

        IEnumerable<Seance> GetSeancesBySalle(int salleID);

        void UpdateSeance(Seance seance);

        int SaveChanges();

        void Dispose();
    } 
}
