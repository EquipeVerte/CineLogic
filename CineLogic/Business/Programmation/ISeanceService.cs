﻿using CineLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineLogic.Business.Programmation
{
    public interface ISeanceService
    {
        IEnumerable<SeanceViewModel> GetSeancesBySalle(int salleID);

        SeanceViewModel GetSeance(int id);

        SeanceViewModel CreateSeance(SeanceViewModel seance);

        SeanceViewModel UpdateSeance(SeanceViewModel seance);

        void UpdateSeanceContents(SeanceViewModel seance);

        void UpdateSeanceTimes(SeanceViewModel seance);

        void AdjustTimeToContent(int seanceID);

        void AddContentToSeance(int seanceID, string contenuTitre);

        void DeleteContentFromSeance(int seanceID, string contenuTitre);

        void DeleteSeance(int id);

        void SaveChanges();

        void Annuler();

        void Dispose();
    }
}
