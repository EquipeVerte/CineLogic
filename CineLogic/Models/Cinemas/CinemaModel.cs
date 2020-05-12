using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq.Expressions;
using CineLogic.Models;

namespace CineLogic.Models
{
    public class CinemaModel
    {
        public int CinemaID { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public bool EnExploitation { get; set; }
        public int ResponsableID { get; set; }
        public string Programmateur { get; set; }
    }

    public class ModelServices : IDisposable
    {
        private readonly CineDBEntities entities = new CineDBEntities();

        public bool SaveCinema(string nom, string adresse, bool enExploitation, int responsableID, string programmateur)
        {
            try
            {
                Cinema cin = new Cinema();
                cin.Nom = nom;
                cin.Adresse = adresse;
                cin.EnExploitation = enExploitation;
                cin.ResponsableID = responsableID;
                cin.Programmateur = programmateur;

                entities.Cinemas.Add(cin);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCinema(int id, string nom, string adresse, bool enExploitation, int responsableID, string programmateur)
        {
            try
            {
                var cin = (from tbl in entities.Cinemas
                           where tbl.CinemaID == id
                           select tbl).FirstOrDefault();
                cin.Nom = nom;
                cin.Adresse = adresse;
                cin.EnExploitation = enExploitation;
                cin.ResponsableID = responsableID;
                cin.Programmateur = programmateur;

                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool DeleteCinema(int id)
        {
            try
            {
                var cin = (from tbl in entities.Cinemas
                           where tbl.CinemaID == id
                           select tbl).FirstOrDefault();

                entities.Cinemas.Remove(cin);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int CountStudent()
        {
            return entities.Cinemas.Count();
        }

        public void Dispose()
        {
            entities.Dispose();
        }

    }

    // Un lien d'un tableau au model?

    public class PagedCinemaModel
    {
        public int TotalRows { get; set; }
        public IEnumerable<Cinema> cinema { get; set; }
        public int PageSize { get; set; }
    }


}