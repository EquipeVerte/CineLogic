using CineLogic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CineLogic.Business.Utilisateurs
{
    public class UserService : IUserService
    {
        private readonly CineDBEntities db;

        private DbContextTransaction transaction;

        private DbContextTransaction GetTransaction()
        {
            if (transaction == null)
            {
                transaction = db.Database.BeginTransaction();
                return transaction;
            }
            else
            {
                return transaction;
            }
        }

        public UserService()
        {
            db = new CineDBEntities();

            transaction = db.Database.BeginTransaction();
        }

        public UserService(CineDBEntities contexteDB)
        {
            db = contexteDB;
        }

        public List<User> GetAllUser()
        {
            return db.Users.ToList();
        }

        public User GetUserByLogin(User login)
        {
            return db.Users.Find(login.Login);
        }

        public void CreateUser(User usager)
        {
            db.Users.Add(usager);

            db.SaveChanges();
        }

        public void UpdateUser(User usager)
        {
            User userToUpdate = db.Users.Find(usager.Login);

            userToUpdate.PasswordHash = usager.PasswordHash;
            userToUpdate.NomComplet = usager.NomComplet;
            db.SaveChanges();
        }

        public void DeleteUser(User usager)
        {
            User user = db.Users.Find(usager.Login);
            db.Users.Remove(user);
            db.SaveChanges();
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
            catch (Exception ex)
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