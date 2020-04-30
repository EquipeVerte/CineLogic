using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_MVC_Bootstrap4_Template.Models.Programmation
{
    public interface ISeanceRepository
    {
        void CreateSeance(Seance seance);

        void DeleteSeance(int id);

        Seance GetSeance(int id);

        IEnumerable<Seance> GetAllSeances();

        IEnumerable<Seance> GeatSeancesBySalle(int salleID);

        void UpdateSeance(Seance seance);

        int SaveChanges();

        void Dispose();
    } 
}
