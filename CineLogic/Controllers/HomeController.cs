﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CineLogic.Models.Hashing;
using System.Web.UI.WebControls;
using CineLogic.Controllers.Attributes;

namespace CineLogic.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

        [SessionActiveOnly]
        public ActionResult Admin()
        {
            return View();
        }

        /*
        public ActionResult User()
        {
            return View();
        }
        */

        [RedirectIfSessionActive]
        public ActionResult Login()
        {
            ViewBag.Message = "Tapez votre login";

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel userModel)
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
                    Session["type"] = user.Type;

                    return user.Type.Equals("admin")
                        ? RedirectToAction("Admin", "Home")
                        : RedirectToAction("User", "Home");
                }
                else
                {
                    ViewBag.Error = "Vérifier l'identifiant ou mot de passe";
                    return View();
                }
            }
        }

        public ActionResult SessionDisconnect()
        {
            Session.Abandon();
            return RedirectToAction("Accueil", "Home");
        }
    }

}