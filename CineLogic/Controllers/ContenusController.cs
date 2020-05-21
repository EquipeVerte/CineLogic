﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using CineLogic.Models;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.IO;
using CineLogic.Controllers.Attributes;

namespace CineLogic.Controllers
{
    [SessionActiveOnly]
    public class ContenusController : Controller
    {
        private CineDBEntities db = new CineDBEntities();
        private CsvData csvData;

        // GET: Contenus
        public ActionResult Index()
        {
            return View(db.Contenus.ToList());
        }

        // GET: Contenus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contenu contenu = db.Contenus.Find(id);
            if (contenu == null)
            {
                return HttpNotFound();
            }
            return View(contenu);
        }

        // GET: Contenus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contenus/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Titre,Description,Annee,RuntimeMins,Rating,Votes,Revenue,MetaScore")] Contenu contenu)
        {
            if (ModelState.IsValid)
            {
                db.Contenus.Add(contenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contenu);
        }


        //Get CreateCsv
        [DontAllowAccess]
        public ActionResult CreateCsv()
        {
            return View();
        }

        //Charger le fichier csv
        [DontAllowAccess]
        private ActionResult ChargerCsv(HttpPostedFileBase postedFile)
        {
            csvData = new CsvData();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            List<string> lignes;
            if (openFileDialog.ShowDialog() == true)
            {
                lignes = new List<string>(System.IO.File.ReadAllLines(openFileDialog.FileName));
                lignes.RemoveAt(0);

                foreach (string ligne in lignes)
                {
                    string[] colones = ligne.Split(';');
                    csvData.AjouterTitle(colones[01]);
                    csvData.AjouterGenre(colones[02]);
                    csvData.AjouterDescription(colones[03]);
                    csvData.AjouterDirector(colones[04]);
                    csvData.AjouterActor(colones[05]);
                    csvData.AjouterYear(colones[06]);
                    csvData.AjouterRuntime(colones[07]);
                    csvData.AjouterRating(colones[08]);
                    csvData.AjouterVote(colones[09]);
                    csvData.AjouterRevenu(colones[10]);
                    csvData.AjouterMetascore(colones[11]);


                    //csvView.Text += colones[1] + " | " + colones[2] + "\n";
                    //text1.Text = colones[3];
                }

            }

            return View();
        }


        [DontAllowAccess]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCsv([Bind(Include = "Titre,Description,Annee,RuntimeMins,Rating,Votes,Revenue,MetaScore")] Contenu contenu)
        {
            if (ModelState.IsValid)
            {
                db.Contenus.Add(contenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contenu);
        }

        // GET: Contenus/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contenu contenu = db.Contenus.Find(id);
            if (contenu == null)
            {
                return HttpNotFound();
            }
            return View(contenu);
        }

        // POST: Contenus/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Titre,Description,Annee,RuntimeMins,Rating,Votes,Revenue,MetaScore")] Contenu contenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contenu);
        }

        // GET: Contenus/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contenu contenu = db.Contenus.Find(id);
            if (contenu == null)
            {
                return HttpNotFound();
            }
            return View(contenu);
        }

        // POST: Contenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id, string DirecteurId, string ActeurId)
        {
            Contenu contenu = db.Contenus.Find(id);
            ////**********Supprimer directeurs*****
            contenu.Acteurs.Clear();
            contenu.Directeurs.Clear();
            contenu.Genres.Clear();

            ////*******************
            db.Contenus.Remove(contenu);
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
        //Ajouter un acteur dans un contenu
        [HttpPost]
        public ActionResult AjouterActeur(string titre, string nomActeur)
        {
            Contenu contenu = db.Contenus.Find(titre);
            if (nomActeur != null)
            {
                if (nomActeur.Trim().Length != 0)
                {
                    Acteur acteur = db.Acteurs.Find(nomActeur);
                    if (acteur == null)
                    {
                        acteur = new Acteur();
                        acteur.Nom = nomActeur;
                        db.Acteurs.Add(acteur);
                    }
                    contenu.Acteurs.Add(acteur);
                    db.SaveChanges();
                }
            }
            return Redirect(Url.Action("Edit", "Contenus", new { id = titre }));
        }
        //Supprimer un acteur d'un contenu
        [HttpGet]
        public ActionResult SupprimerActeur(string contenuId, string ActeurId)
        {
            Contenu contenu = db.Contenus.Find(contenuId);
            Acteur acteur = db.Acteurs.Find(ActeurId);
            if (acteur == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contenu.Acteurs.Remove(acteur);
            db.SaveChanges();
            return Redirect(Url.Action("Edit", "Contenus", new { id = contenuId }));
        }

        //Ajouter un directeur dans un contenu
        [HttpPost]
        public ActionResult AjouterDirecteur(string titre, string nomDirecteur)
        {
            Contenu contenu = db.Contenus.Find(titre);
            if (nomDirecteur != null)
            {
                if (nomDirecteur.Trim().Length != 0)
                {
                    Directeur directeur = db.Directeurs.Find(nomDirecteur);
                    if (directeur == null)
                    {
                        directeur = new Directeur();
                        directeur.Nom = nomDirecteur;
                        db.Directeurs.Add(directeur);
                    }
                    contenu.Directeurs.Add(directeur);
                    db.SaveChanges();
                }
            }
            return Redirect(Url.Action("Edit", "Contenus", new { id = titre }));

        }
        //Supprimer un directeur d'un contenu
        [HttpGet]
        public ActionResult SupprimerDirecteur(string contenuId, string DirecteurId)
        {
            Contenu contenu = db.Contenus.Find(contenuId);
            Directeur directeur = db.Directeurs.Find(DirecteurId);
            if (directeur == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contenu.Directeurs.Remove(directeur);
            db.SaveChanges();
            return Redirect(Url.Action("Edit", "Contenus", new { id = contenuId }));
        }

        //  Ajax get contenus
        [HttpGet]
        public ContentResult Contenus(string filter)
        {
            CineDBEntities db = new CineDBEntities();

            return Content(JsonConvert.SerializeObject((from c in db.Contenus where c.Titre.Contains(filter) select c.Titre)), "application/json");
        }
    }
}
