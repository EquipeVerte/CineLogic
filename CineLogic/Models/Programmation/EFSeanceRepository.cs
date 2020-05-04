using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CineLogic.Models.Programmation
{
    //  Le repository des séances qui utilise la base de données.
    public class EFSeanceRepository : ISeanceRepository
    {
        private CineDBEntities db = new CineDBEntities();

        private IMapper mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Cinema, CinemaSelectionItem>();
            cfg.CreateMap<Salle, SalleSelectionItem>();
        }).CreateMapper();

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
            try
            {
                return db.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        public void UpdateSeance(Seance seance)
        {
            db.Entry(seance).State = EntityState.Modified;
        }

        public IEnumerable<CinemaSelectionItem> GetCinemas()
        {
            return mapper.Map<IEnumerable<Cinema>, IEnumerable<CinemaSelectionItem>>(db.Cinemas).ToList();
        }

        public IEnumerable<SalleSelectionItem> GetSalles(int cinemaID)
        {
            return mapper.Map<IEnumerable<Salle>, IEnumerable<SalleSelectionItem>>(db.Cinemas.Find(cinemaID).Salles).ToList();
        }

        public bool FindSeanceConflicts(Seance seance)
        {
            return db.Seances.Any(s => s.SalleID == seance.SalleID && (s.HeureDebut > seance.HeureDebut && s.HeureDebut < seance.HeureFin || s.HeureFin < seance.HeureFin && s.HeureFin > seance.HeureDebut));
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}