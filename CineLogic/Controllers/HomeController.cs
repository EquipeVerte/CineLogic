using CineLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using CineLogic.Models.Hashing;
using System.Web.UI.WebControls;

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

        public ActionResult User()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Tapez votre login";

            return View();
        }

        [HttpPost]
        public ActionResult LoginAutoriser(LoginViewModel userModel)
        {

            using (CineDBEntities db = new CineDBEntities())
            {
                var user = db.Users.Find(userModel.Login);

                if (user == null)
                {
                    return RedirectToAction("Login", "Home", new { Erreur = "Nom d'utilisateur ou mot de passe n'existe pas" });
                }
                Hasher hasher = new Hasher();

                Hash hash = new Hash(user.Salt, user.HashIterations, user.PasswordHash);

                if (hasher.IsMatched(userModel.Password, hash))
                {
                    Session["login"] = user.Login;

                    return user.Type.Equals("admin")
                        ? RedirectToAction("Admin", "Home")
                        : RedirectToAction("User", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "Home", new { Erreur = "Nom d'utilisateur ou mot de passe n'existe pas" });
                }

                /*
                var userDetails = db.Users.Where(x => x.Login == userModel.Login && x.Motdepasse == userModel.Motdepasse).FirstOrDefault();

                if (userDetails == null)
                {
                    return RedirectToAction("Login", "Home", new { Erreur = "Nom d'utilisateur ou mot de passe n'existe pas" });
                }
                else
                {
                    Session["login"] = userDetails.Login;
 

                    return userDetails.Type.Equals("admin")
                        ? RedirectToAction("Admin", "Home")
                        : RedirectToAction("User", "Home");
                }
                */
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
        public ActionResult SessionDisconnect()
        {
            Session.Abandon();
            return RedirectToAction("Accueil", "Home");
        }
    }

}