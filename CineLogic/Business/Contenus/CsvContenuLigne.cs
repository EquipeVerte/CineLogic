using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Business.Contenus
{
    public class CsvContenuLigne
    {

        public string Rank { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public int Year { get; set; }
        [Name("Runtime (Minutes)")]
        public int Runtime { get; set; }
        public Nullable<decimal> Rating { get; set; }
        public Nullable<int> Votes { get; set; }
        [Name("Revenue (Millions)")]
        public Nullable<decimal> Revenue { get; set; }
        public Nullable<int> Metascore { get; set; }

    }
}