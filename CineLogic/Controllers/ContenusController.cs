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
using Newtonsoft.Json;
using Microsoft.Win32;
using System.IO;
using CineLogic.Controllers.Attributes;
using CineLogic.Business.Contenus;
using System.Collections;
using CsvHelper;
using System.Globalization;

namespace CineLogic.Controllers
{
    [SessionActiveOnly]
    public class ContenusController : Controller
    {
        private CineDBEntities db = new CineDBEntities();


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
        //[DontAllowAccess]
        public ActionResult CreateCsv()
        {
            return View("CreateCsv");
        }

        //Charger le fichier csv
        //private CsvData csvData = new CsvData();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCsv(HttpPostedFileBase csvFile)
        {
            int filmExistant = 0;
            int filmAjouter = 0;

            int acteurExistant = 0;
            int acteurAjouter = 0;

            int directeurExistant = 0;
            int directeurAjouter = 0;


            //if (ModelState.IsValid)
            //{
            //    db.Contenus.Add(contenu);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //List<string> lignes;


            if (csvFile != null)
            {
                if (Path.GetExtension(csvFile.FileName) != ".csv")
                {
                    ViewBag.ErreurTypeFichier = "Le type du fichier est incorecte ! ";
                }
                else
                {
                    StreamReader streamReader = new StreamReader(csvFile.InputStream);
                    CsvReader csvReader = new CsvReader(streamReader, new CultureInfo("en-US"));

                    List<CsvContenuLigne> csvContenuLignes = csvReader.GetRecords<CsvContenuLigne>().ToList();

                    //lignes = new List<string>();
                    //while (!streamReader.EndOfStream)
                    //{
                    //    lignes.Add(streamReader.ReadLine());
                    //}

                    //lignes.RemoveAt(0);


                    ContenuService contenuService = new ContenuService();
                    foreach (CsvContenuLigne csvContenuLigne in csvContenuLignes)
                    {
                        contenuService.ParsserColmn(csvContenuLigne);
                    }


                    filmAjouter += contenuService.filmAjouter;
                    ViewBag.FilmAjouter = filmAjouter;
                    directeurAjouter += contenuService.directeurAjouter;
                    ViewBag.DirecteurAjouter = directeurAjouter;
                    acteurAjouter += contenuService.acteurAjouter;
                    ViewBag.ActeurAjouter = acteurAjouter;
                    //****Existant
                    filmExistant += contenuService.filmExistant;
                    //directeurExistant++;
                    ViewBag.FilmExistant = filmExistant;
                    directeurExistant += contenuService.directeurExistant;
                    ViewBag.DirecteurExistant = directeurExistant;
                    acteurExistant += contenuService.acteurExistant;
                    ViewBag.ActeurExistant = acteurExistant;


                    //foreach (string ligne in lignes)
                    //{
                    //    string[] colones = ligne.Split(';');
                    //    ContenuService contenuService = new ContenuService();
                    //    contenuService.ParsserColmn(colones);


                    //    Contenu contenu = db.Contenus.Find(colones[01]);

                    //    if (contenu == null)
                    //    {
                    //        filmAjouter++;
                    //        ViewBag.FilmAjouter = filmAjouter;
                    //        directeurAjouter += contenuService.directeurAjouter;
                    //        ViewBag.DirecteurAjouter = directeurAjouter;
                    //        acteurAjouter += contenuService.acteurAjouter;
                    //        ViewBag.ActeurAjouter = acteurAjouter;

                    //    }
                    //    else
                    //    {
                    //        filmExistant++;
                    //        //directeurExistant++;
                    //        ViewBag.FilmExistant = filmExistant;
                    //    }
                    //}
                }        
            }
            else
            {
                ViewBag.CheminVide = "Veillez sélectionner un fichier CSV !";
            }
            return View();
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

        //Ajouter un genre dans un contenu
        [HttpPost]
        public ActionResult AjouterGenre(string titre, string nomGenre)
        {
            Contenu contenu = db.Contenus.Find(titre);
            if (nomGenre != null)
            {
                if (nomGenre.Trim().Length != 0)
                {
                    Genre genre = db.Genres.Find(nomGenre);
                    if (genre == null)
                    {
                        genre = new Genre();
                        genre.Nom = nomGenre;
                        db.Genres.Add(genre);
                    }
                    contenu.Genres.Add(genre);
                    db.SaveChanges();
                }
            }
            return Redirect(Url.Action("Edit", "Contenus", new { id = titre }));

        }
        //Supprimer un genre d'un contenu
        [HttpGet]
        public ActionResult SupprimerGenre(string contenuId, string GenreId)
        {
            Contenu contenu = db.Contenus.Find(contenuId);
            Genre genre = db.Genres.Find(GenreId);
            if (genre == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contenu.Genres.Remove(genre);
            db.SaveChanges();
            return Redirect(Url.Action("Edit", "Contenus", new { id = contenuId }));
        }
        //  Ajax get contenus
        [HttpGet]
        public ContentResult Contenus(string filter, string types, int nbResults)
        {
            CineDBEntities db = new CineDBEntities();

            string[] typesArray = types.Split(',');

            //List<ContenuSelectionViewModel> contents = standardContents.Concat(promoContents).ToList();

            IQueryable<ContenuSelectionViewModel> contenus = from c in db.Contenus where c.Titre.Contains(filter) && typesArray.Contains(c.typage) select new ContenuSelectionViewModel() { Titre = c.Titre, Type = c.typage, Runtime = c.RuntimeMins };

            if (typesArray.Contains(ContenuTypeLibrary.CONT_TYPE_PROMO))
            {
                contenus = contenus.Concat((from c in db.ContenuPromoes where c.Titre.Contains(filter) select new ContenuSelectionViewModel() { Titre = c.Titre, Type = ContenuTypeLibrary.CONT_TYPE_PROMO, Runtime = c.RuntimeMins }));
            }

            return Content(
                JsonConvert.SerializeObject(
                    contenus.Take(nbResults)),
                    "application/json");
        }

    }
}
