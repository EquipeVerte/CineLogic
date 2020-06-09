using CineLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineLogic.Business.Utilisateurs
{
    public interface IUserService
    {
        List<User> GetAllUser();

        User GetUserByLogin(User login);

        void CreateUser(User usager);

        void UpdateUser(User usager);

        void DeleteUser(User usager);

        int SaveChanges();

        void Annuler();

        void Dispose();
    }
}
