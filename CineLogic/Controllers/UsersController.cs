using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CineLogic.Controllers.Attributes;
using CineLogic.Models;
using CineLogic.Models.Hashing;
using CineLogic.Models.Libraries;

namespace CineLogic.Controllers
{
    [SessionActiveOnly]
    public class UsersController : Controller
    {
        private CineDBEntities db = new CineDBEntities();

        // GET: Users
        [AdminOnly]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                id = (string)Session[SessionTypes.login];
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [AdminOnly]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Login,Password,NomComplet,Type")] UserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Login = userVM.Login;
                user.NomComplet = userVM.NomComplet;
                user.Type = userVM.Type;

                Hasher hasher = new Hasher();

                Hash hash = hasher.GenerateHash(userVM.Password);

                user.PasswordHash = hash.HashedString;
                user.Salt = hash.Salt;
                user.HashIterations = hash.Iterations;

                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationError in ex.EntityValidationErrors)
                    {
                        Console.WriteLine("Object: {0} ", validationError.Entry.Entity.ToString());
                        //Response.Write("Object: " + validationError.Entry.Entity.ToString());
                        //Response.Write(" ");
                        foreach (var err in validationError.ValidationErrors)
                        {
                            Console.WriteLine("Property: {0} Error: {1}", err.PropertyName, err.ErrorMessage);
                            //Response.Write(err.ErrorMessage + " ");
                        }
                    }
                }
                return RedirectToAction("Index");
            }

            return View(userVM);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            //  Extraire les données nécessaires pour faire l'édition.
            UserViewModel userVM = new UserViewModel();
            userVM.Login = user.Login;
            userVM.NomComplet = user.NomComplet;
            userVM.Type = user.Type;

            return View(userVM);
        }

        // POST: Users/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Login,Password,NomComplet,Type")] UserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                //  Chercher le user.
                User user = db.Users.Find(userVM.Login);

                //  Changer les données pour l'utilisateur.
                user.Login = userVM.Login;
                user.NomComplet = userVM.NomComplet;
                user.Type = userVM.Type;

                //  Hasher le nouveau mot de passe.
                Hasher hasher = new Hasher();
                Hash hash = hasher.GenerateHash(userVM.Password);

                user.PasswordHash = hash.HashedString;
                user.Salt = hash.Salt;
                user.HashIterations = hash.Iterations;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userVM);
        }

        // GET: Users/Delete/5
        [AdminOnly]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [AdminOnly]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
