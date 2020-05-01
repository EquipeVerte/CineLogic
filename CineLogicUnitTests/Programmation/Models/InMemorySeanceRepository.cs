

using CineLogic.Models;
using CineLogic.Models.Programmation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineLogicUnitTests.Programmation.Models
{
    class InMemorySeanceRepository : ISeanceRepository
    {
        private List<Seance> db = new List<Seance>();

        public void CreateSeance(Seance seance)
        {
            db.Add(seance);
        }

        public void DeleteSeance(int id)
        {
            Seance seance = db.Where(s => s.SeanceID == id).FirstOrDefault();

            db.Remove(seance);
        }

        public IEnumerable<Seance> GetSeancesBySalle(int salleID)
        {
            return db.Where(s => s.SalleID == salleID);
        }

        public IEnumerable<Seance> GetAllSeances()
        {
            return db;
        }

        public Seance GetSeance(int id)
        {
            return db.Where(s => s.SeanceID == id).FirstOrDefault();
        }

        public int SaveChanges()
        {
            return 1;
        }

        public void UpdateSeance(Seance seance)
        {
            db[db.IndexOf(db.Where(s => s.SeanceID == seance.SeanceID).FirstOrDefault())] = seance; 
        }

        public void Dispose()
        {
            return;
        }

        public IEnumerable<CinemaSelectionItem> GetCinemas()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalleSelectionItem> GetSalles(int cinemaID)
        {
            throw new NotImplementedException();
        }

        public bool FindSeanceConflicts(Seance seance)
        {
            throw new NotImplementedException();
        }
    }
}
