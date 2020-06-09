using CineLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Business.Utilisateurs
{
    public class UserService : IUserService
    {
        private readonly CineDBEntities db;

        public UserService(CineDBEntities contexteDB)
        {
            db = contexteDB;
        }

        public List<User> GetAllUser()
        {
           return db.Users.ToList();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}