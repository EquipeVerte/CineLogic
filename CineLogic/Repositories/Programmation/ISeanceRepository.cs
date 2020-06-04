using CineLogic.Models;
using System.Collections.Generic;

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

        void AddContenu(SeanceContenu contenu);

        void AddPromo(SeancePromo promo);

        string GetContentType(string contenuTitre);

        int SaveChanges();

        void Annuler();

        void Dispose();
    } 
}
