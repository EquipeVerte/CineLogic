using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Models.Cinemas
{
    public interface ICinemaRepository
    {
        void CreateCinema(Cinema cinema);

        void DeleteCinema(int id);

        Cinema GetCinema(int id);

        IEnumerable<Cinema> GetAllCinemas();

        IEnumerable<Cinema> GetSallesByCinema(int CinemaID);

        void UpdateCinema(Cinema cinema);

        int SaveChanges();

        void Dispose();
    }
}