using AutoMapper;
using CineLogic.Models;
using CineLogic.Models.Programmation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CineLogic.Repositories
{
    //  Le repository des séances qui utilise la base de données.
    public class EFSeanceRepository : ISeanceRepository
    {
        private CineDBEntities db;

        private DbContextTransaction transaction;

        private DbContextTransaction GetTransaction()
        {
            if(transaction == null)
            {
                transaction = db.Database.BeginTransaction();
                return transaction;
            }
            else
            {
                return transaction;
            }
        }

        public EFSeanceRepository()
        {
            db = new CineDBEntities();

            transaction = db.Database.BeginTransaction();
        }

        public EFSeanceRepository(CineDBEntities db)
        {
            this.db = db;
        }

        public void CreateSeance(Seance seance)
        {
            db.Seances.Add(seance);

            db.SaveChanges();
        }

        public void DeleteSeance(Seance seance)
        {
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

        public void UpdateSeance(Seance seance)
        {
            Seance seanceToUpdate = db.Seances.Find(seance.SeanceID);

            seanceToUpdate.HeureDebut = seance.HeureDebut;
            seanceToUpdate.HeureFin = seance.HeureFin;
            seanceToUpdate.Titre = seance.Titre;
            seanceToUpdate.ContenuTitre = seance.ContenuTitre;
        }

        public int SaveChanges()
        {
            try
            {
                db.SaveChanges();
                GetTransaction().Commit();
                GetTransaction().Dispose();
                transaction = null;
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        public void Annuler()
        {
            GetTransaction().Rollback();
            GetTransaction().Dispose();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}