using CineLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using CineLogic.Models;

namespace CineLogic.Controllers
{
    public class HomeController : Controller
    {
        private CineDBEntities db = new CineDBEntities();
        public ActionResult Accueil()
        {
            return View(db.Contenus.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "À propos de cette projet.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacter nous.";

            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Tapez votre login";

            return View();
        }

        [HttpPost]
        public ActionResult LoginAutoriser(User userModel)
        {

            using (CineDBEntities db = new CineDBEntities())
            {

                var userDetails = db.Users.Where(x => x.Login == userModel.Login && x.Motdepasse == userModel.Motdepasse).FirstOrDefault();

                if (userDetails == null)
                {
                    return RedirectToAction("Login", "Home", new { Erreur = "Nom d'utilisateur ou mot de passe n'existe pas" });
                }
                else
                {
                    Session["login"] = userDetails.Login;
 

                    return userDetails.Type.Equals("admin")
                        ? RedirectToAction("Index", "Admin")
                        : RedirectToAction("Index", "Client");
                }
            }
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "Login, Password")] LoginViewModel loginUsers)
        {
            if (loginUsers.Login == "admin")
            {
                Session["login"] = loginUsers.Login;
                return RedirectToAction("Index", "Admin");
            }
   
            else
            {
                return View();
            }
        }

        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Login, Password, NewPassword, NewPasswordConfirm")] PasswordChangeViewModel userPass, string Type)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {

                };

                using (CineDBEntities db = new CineDBEntities())
                {
                    try
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                        return RedirectToAction("Connect", "Home");
                    }
                    catch
                    {
                        return RedirectToAction("Register", "Home", new { Erreur = "Incapable de registrer l'utilisateur." });
                    }
                }
            }
            else
            {
                return View();
            }
        }
    }
}