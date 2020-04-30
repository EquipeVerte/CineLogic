using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CineLogic.Models.Programmation
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

        public IEnumerable<Seance> GetSeancesBySalle(int salleID)
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

        public IEnumerable<CinemaSelectionItem> GetCinemas()
        {
            List<CinemaSelectionItem> cinemas = new List<CinemaSelectionItem>();

            foreach(Cinema cinema in db.Cinemas)
            {
                CinemaSelectionItem csi = new CinemaSelectionItem()
                {
                    CinemaID = cinema.CinemaID,
                    Nom = cinema.Nom,
                };                

                cinemas.Add(csi);
            }

            return cinemas;
        }

        public IEnumerable<SalleSelectionItem> GetSalles(int CinemaID)
        {
            List<SalleSelectionItem> salles = new List<SalleSelectionItem>();

            foreach(Salle salle in db.Cinemas.Find(CinemaID).Salles)
            {
                salles.Add(new SalleSelectionItem()
                {
                    SalleID = salle.SalleID,
                    Nom = salle.Nom
                });
            }

            return salles;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}