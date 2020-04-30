using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASPNET_MVC_Bootstrap4_Template.Models.Programmation
{
    public class EFSeanceRepository : ISeanceRepository
    {
        private CineDBEntities db = new CineDBEntities();

        public void CreateSeance(Seance seance)
        {
            db.Seances.Add(seance);
        }

        public void DeleteSeance(int id)
        {
            Seance seance = db.Seances.Find(id);

            db.Seances.Remove(seance);
        }

        public IEnumerable<Seance> GeatSeancesBySalle(int salleID)
        {
            return db.Seances.Where(s => s.SalleID == salleID);
        }

        public IEnumerable<Seance> GetAllSeances()
        {
            return db.Seances;
        }

        public Seance GetSeance(int id)
        {
            return db.Seances.Find(id);
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }

        public void UpdateSeance(Seance seance)
        {
            db.Entry(seance).State = EntityState.Modified;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}