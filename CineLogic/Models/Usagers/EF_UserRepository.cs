using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Models.Usagers
{
    public class EF_UserRepository
    {
        private CineDBEntities db = new CineDBEntities();

        public void CreateSeance(Seance seance)
        {
            db.Seances.Add(seance);
        }

        public void UpdateSeance(Seance seance)
        {
            //db.Entry(seance).State = EntityState.Modified;
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }
    }
}