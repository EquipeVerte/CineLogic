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
using Moq;
using System.Data.Entity;
using CineLogic.Repositories;

namespace CineLogic.Controllers.Programmation.Tests
{
    [TestClass()]
    public class SeancesControllerTests
    {
        private EFSeanceRepository repository;

        [TestInitialize]
        public void TestInit()
        {
            var list = new List<Seance>
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
                }
            };

            var data = list.AsQueryable();

            var mockSet = new Mock<DbSet<Seance>>();
            mockSet.As<IQueryable<Seance>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Seance>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Seance>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Seance>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<Seance>())).Callback<Seance>((s) => list.Add(s));

            var mockContext = new Mock<CineDBEntities>();
            mockContext.Setup(c => c.Seances).Returns(mockSet.Object);

            repository = new EFSeanceRepository(mockContext.Object);
        }

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
            Seance testSeance = new Seance()
            {
                Titre = "Valid Test Séance",
                HeureDebut = new DateTime(2020, 1, 1, 12, 0, 0),
                HeureFin = new DateTime(2020, 1, 1, 15, 0, 0),
                SalleID = 1,
                ContenuTitre = "Test Film"
            };

            repository.CreateSeance(testSeance);

            var seances = repository.GetAllSeances().ToList();

            Assert.AreEqual(5, seances.Count);
        }
    }
}