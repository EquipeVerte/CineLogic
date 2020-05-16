using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using CineLogic.Models;
using Microsoft.Win32;
using System.IO;

namespace CineLogic.Controllers
{
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
        public ActionResult CreateCsv()
        {
            return View();
        }

        //Charger le fichier csv
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
                    try { csvData.AjouterTitle(colones[01]); } catch { MessageBox.Show("Erreur de chargement du titre"); }
                    try { csvData.AjouterGenre(colones[02]); } catch { MessageBox.Show("Erreur de chargement du genre"); }
                    try { csvData.AjouterDescription(colones[03]); } catch { MessageBox.Show("Erreur de chargement de la description"); }
                    try { csvData.AjouterDirector(colones[04]); } catch { MessageBox.Show("Erreur de chargement du directeur"); }
                    try { csvData.AjouterActor(colones[05]); } catch { MessageBox.Show("Erreur de chargement des acteurs"); }
                    try { csvData.AjouterYear(colones[06]); } catch { MessageBox.Show("Erreur de chargement de l'année"); }
                    try { csvData.AjouterRuntime(colones[07]); } catch { MessageBox.Show("Erreur de chargement du runtime"); }
                    try { csvData.AjouterRating(colones[08]); } catch { MessageBox.Show("Erreur de chargement du rating"); }
                    try { csvData.AjouterVote(colones[09]); } catch { MessageBox.Show("Erreur de chargement des votes"); }
                    try { csvData.AjouterRevenu(colones[10]); } catch { MessageBox.Show("Erreur de chargement du revenu"); }
                    try { csvData.AjouterMetascore(colones[11]); } catch { MessageBox.Show("Erreur de chargement du metascore"); }


                    //csvView.Text += colones[1] + " | " + colones[2] + "\n";
                    //text1.Text = colones[3];
                }

            }

            return View();
        }



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
            if (nomActeur != null || nomActeur != "")
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
            if (nomDirecteur != null || nomDirecteur != "")
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
    }
}
