using Microsoft.VisualStudio.TestTools.UnitTesting;
using CineLogic.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineLogic.Models.Programmation;
using CineLogicUnitTests.Programmation.Models;
using CineLogic.Models;
using System.Web.Mvc;

namespace CineLogic.Controllers.Programmation.Tests
{
    [TestClass()]
    public class SeancesControllerTests
    {
        private Seance GetSeance_Valid()
        {
            return new Seance()
            {
                Titre = "Valid Test Séance",
                HeureDebut = new DateTime(2020, 1, 1, 12, 0, 0),
                HeureFin = new DateTime(2020, 1, 1, 15, 0, 0),
                SalleID = 1,
                ContenuTitre = "Test Film"
            };
        }

        private Seance GetSeance_Invalid_Heures()
        {
            return new Seance()
            {
                Titre = "Invalid Test Seance",
                HeureDebut = new DateTime(2020, 1, 1, 15, 0, 0),
                HeureFin = new DateTime(2020, 1, 1, 15, 0, 0),
                SalleID = 1
            };
        }

        private List<Seance> GetSeances_Valid()
        {
            return new List<Seance>()
            {
                new Seance()
                {
                    SeanceID = 1,
                    Titre = "Test Séance 1",
                    HeureDebut = new DateTime(2020, 1, 1, 12, 0, 0),
                    HeureFin = new DateTime(2020, 1, 1, 15, 0, 0),
                    SalleID = 1,
                    ContenuTitre = "Test Film 1"
                },
                new Seance()
                {
                    SeanceID = 2,
                    Titre = "Test Séance 2",
                    HeureDebut = new DateTime(2020, 1, 1, 15, 0, 0),
                    HeureFin = new DateTime(2020, 1, 1, 18, 0, 0),
                    SalleID = 1,
                    ContenuTitre = "Test Film 1"
                },
                new Seance()
                {
                    SeanceID = 3,
                    Titre = "Test Séance 3",
                    HeureDebut = new DateTime(2020, 1, 1, 18, 0, 0),
                    HeureFin = new DateTime(2020, 1, 1, 21, 0, 0),
                    SalleID = 1,
                    ContenuTitre = "Test Film 2"
                },
                new Seance()
                {
                    SeanceID = 4,
                    Titre = "Test Séance 4",
                    HeureDebut = new DateTime(2020, 1, 1, 12, 0, 0),
                    HeureFin = new DateTime(2020, 1, 1, 15, 0, 0),
                    SalleID = 2,
                    ContenuTitre = "Test Film 2"
                },
            };
        }

        private SeancesController GetSeanceController()
        {
            return new SeancesController(new InMemorySeanceRepository());
        }

        private SeancesController GetSeancesControllerWithData()
        {
            InMemorySeanceRepository repo = new InMemorySeanceRepository(GetSeances_Valid());

            return new SeancesController(repo);
        }

        private T GetValueFromJsonResult<T> (JsonResult jsonResult, string propName)
        {
            var property =
                jsonResult.Data.GetType().GetProperty(propName);

            if(property == null)
            {
                throw new ArgumentException("Property not found", propName);
            }

            return (T)property.GetValue(jsonResult.Data, null);
        }

        [TestMethod()]
        public void Create_Post_PutsValidModelIntoRepository()
        {
            var controller = GetSeanceController();
            ActionResult actionResult = controller.Create(GetSeance_Valid());

            Assert.IsInstanceOfType(actionResult, typeof(JsonResult));
            bool success = GetValueFromJsonResult<bool>(actionResult as JsonResult, "success");
            Assert.IsTrue(success);
        }
        
        [TestMethod()]
        public void Create_Post_RejectsOperationIfModelIsNotValid()
        {
            var controller = GetSeanceController();
            ActionResult actionResult = controller.Create(GetSeance_Invalid_Heures());

            bool success = GetValueFromJsonResult<bool>(actionResult as JsonResult, "success");
            Assert.IsFalse(success);
        }
    }
}