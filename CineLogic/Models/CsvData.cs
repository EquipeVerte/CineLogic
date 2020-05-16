using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Models
{
    public class CsvData
    {
        #region dictionnaires
        private Dictionary<string, int> _titles = new Dictionary<string, int>();
        public List<string> Titles { get => _titles.Keys.ToList<string>(); }

        private Dictionary<string, int> _genres = new Dictionary<string, int>();
        public List<string> Genres { get => _genres.Keys.ToList<string>(); }

        private Dictionary<string, int> _descriptions = new Dictionary<string, int>();
        public List<string> Descrisption { get => _descriptions.Keys.ToList<string>(); }

        private Dictionary<string, int> _directors = new Dictionary<string, int>();
        public List<string> Directors { get => _directors.Keys.ToList<string>(); }

        private Dictionary<string, int> _actors = new Dictionary<string, int>();
        public List<string> Actors { get => _actors.Keys.ToList<string>(); }

        private Dictionary<string, int> _years = new Dictionary<string, int>();
        public List<string> Years { get => _years.Keys.ToList<string>(); }

        private Dictionary<string, int> _runtimes = new Dictionary<string, int>();
        public List<string> Runtimes { get => _runtimes.Keys.ToList<string>(); }

        private Dictionary<string, int> _ratings = new Dictionary<string, int>();
        public List<string> Ratings { get => _ratings.Keys.ToList<string>(); }

        private Dictionary<string, int> _votes = new Dictionary<string, int>();
        public List<string> Votes { get => _votes.Keys.ToList<string>(); }

        private Dictionary<string, int> _revenues = new Dictionary<string, int>();
        public List<string> Revenues { get => _revenues.Keys.ToList<string>(); }

        private Dictionary<string, int> _metascores = new Dictionary<string, int>();
        public List<string> Metascores { get => _metascores.Keys.ToList<string>(); }
        #endregion


        // Tableau des films
        public Dictionary<string, int> CsvTable { get; }

        #region des ajouts
        public void AjouterTitle(string title)
        {
            _titles[title] = 1;
        }
        public void AjouterGenre(string genre)
        {
            _genres[genre] = 1;
        }
        public void AjouterDescription(string description)
        {
            _descriptions[description] = 1;
        }
        public void AjouterDirector(string director)
        {
            _directors[director] = 1;
        }
        public void AjouterActor(string actor)
        {
            _actors[actor] = 1;
        }
        public void AjouterYear(string year)
        {
            _years[year] = 1;
        }
        public void AjouterRuntime(string runtime)
        {
            _runtimes[runtime] = 1;
        }
        public void AjouterRating(string rating)
        {
            _ratings[rating] = 1;
        }
        public void AjouterVote(string vote)
        {
            _votes[vote] = 1;
        }
        public void AjouterRevenu(string revenu)
        {
            _revenues[revenu] = 1;
        }
        public void AjouterMetascore(string metascore)
        {
            _metascores[metascore] = 1;
        }
        #endregion


        public CsvData()
        {
            CsvTable = new Dictionary<string, int>();

        }
    }
}