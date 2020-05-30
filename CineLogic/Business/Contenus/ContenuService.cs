using CineLogic.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Windows;

namespace CineLogic.Business.Contenus
{
    public class ContenuService
    {
        private CineDBEntities db = new CineDBEntities();

        public string Titles { get; set; }
        public string Genres { get; set; }
        public string Descrisption { get; set; }
        public string Directors { get; set; }
        public string Actors { get; set; }
        public string Years { get; set; }
        public string Runtimes { get; set; }
        public string Ratings { get; set; }
        public string Votes { get; set; }
        public string Revenues { get; set; }
        public string Metascores { get; set; }

        public int filmExistant = 0;
        public int filmAjouter = 0;

        public int acteurExistant = 0;
        public int acteurAjouter = 0;

        public int directeurExistant = 0;
        public int directeurAjouter = 0;
        public int compteur = 0;

        public void ParsserColmn(CsvContenuLigne csvContenuLigne) //objet de Csvcontenulignes 
        {
            Contenu contenu;

            if (db.Contenus.Find(csvContenuLigne.Title) != null) //update des données
            {
                contenu = db.Contenus.Find(csvContenuLigne.Title);
                //Incrémenter le compteur des films trouvés
                filmExistant++;

                //col[2] des genres
                string[] gen = csvContenuLigne.Genre.Split(',');
                foreach (string g in gen)
                {
                    if (db.Genres.Find(g.Trim()) == null)
                    {
                        db.Genres.Add(new Genre() { Nom = g.Trim() });
                    }
                    contenu.Genres.Add(db.Genres.Find(g.Trim()));
                }
                //col[3] description
                contenu.Description = csvContenuLigne.Description;
                //col[4] des directeurs
                string[] dir = csvContenuLigne.Director.Split(',');
                foreach (string d in dir)
                {
                    if (db.Directeurs.Find(d.Trim()) == null)
                    {
                        db.Directeurs.Add(new Directeur() { Nom = d.Trim() });
                        //incrementer le nbr des directeurs ajouté
                        directeurAjouter++;
                    }
                    else
                    {
                        directeurExistant++;
                    }
                    contenu.Directeurs.Add(db.Directeurs.Find(d.Trim()));
                }

                //col[5] des acteurs
                string[] act = csvContenuLigne.Actors.Split(',');
                foreach (string a in act)
                {
                    if (db.Acteurs.Find(a.Trim()) == null)
                    {
                        db.Acteurs.Add(new Acteur() { Nom = a.Trim() });
                        //incrementer le nbr des acteurs ajouté
                        acteurAjouter++;
                    }
                    else
                    {
                        acteurExistant++;
                    }
                    contenu.Acteurs.Add(db.Acteurs.Find(a.Trim()));
                }
                //col[6 ...]
                try { contenu.Annee = csvContenuLigne.Year; } catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                try { contenu.RuntimeMins = csvContenuLigne.Runtime; } catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                try { contenu.Rating = csvContenuLigne.Rating; } catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                try { contenu.Votes = csvContenuLigne.Votes; } catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                try { contenu.Revenue = csvContenuLigne.Revenue; } catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                try { contenu.MetaScore = csvContenuLigne.Metascore; } catch (Exception ex) { MessageBox.Show(ex.ToString()); }


                //db.SaveChanges();
            }
            else
            {
                contenu = new Contenu();
                contenu.Titre = csvContenuLigne.Title;
                filmAjouter++;


                string[] gen = csvContenuLigne.Genre.Split(',');
                foreach (string g in gen)
                {
                    if (db.Genres.Find(g.Trim()) == null)
                    {
                        db.Genres.Add(new Genre() { Nom = g.Trim() });
                    }
                    contenu.Genres.Add(db.Genres.Find(g.Trim()));
                }
                contenu.Description = csvContenuLigne.Description;
                //contenu.Directeurs = col[4];
                string[] dir = csvContenuLigne.Director.Split(',');
                foreach (string d in dir)
                {
                    if (db.Directeurs.Find(d.Trim()) == null)
                    {
                        db.Directeurs.Add(new Directeur() { Nom = d.Trim() });
                        //incrementer le nbr des acteurs ajouté
                        directeurAjouter++;
                    }
                    else
                    {
                        directeurExistant++;
                    }
                    contenu.Directeurs.Add(db.Directeurs.Find(d.Trim()));
                }
                //contenu.Acteurs = col[5];
                string[] act = csvContenuLigne.Actors.Split(',');
                foreach (string a in act)
                {
                    if (db.Acteurs.Find(a.Trim()) == null)
                    {
                        db.Acteurs.Add(new Acteur() { Nom = a.Trim() });
                        //incrementer le nbr des acteurs ajouté
                        acteurAjouter++;
                    }
                    else
                    {
                        acteurExistant++;
                    }
                    contenu.Acteurs.Add(db.Acteurs.Find(a.Trim()));
                }
                try
                {
                    contenu.Annee = csvContenuLigne.Year;
                }
                catch (Exception ex) { MessageBox.Show(contenu.Annee + "  " + contenu.Titre + "  ////  " + ex.ToString()); }
                try
                {
                    contenu.RuntimeMins = csvContenuLigne.Runtime;
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                try
                {
                    contenu.Rating = csvContenuLigne.Rating;

                }
                catch (Exception ex) { MessageBox.Show(contenu.Rating + "  ////   " + ex.ToString()); }
                try
                {
                    contenu.Votes = csvContenuLigne.Votes;
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                try
                {
                    contenu.Revenue = csvContenuLigne.Revenue;
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                try
                {
                    contenu.MetaScore = csvContenuLigne.Metascore;
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }

                db.Contenus.Add(contenu);
            }
            db.SaveChanges();
        }
    }
}