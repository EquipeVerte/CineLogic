using Microsoft.VisualStudio.TestTools.UnitTesting;
using CineLogic.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineLogic.Models;
using System.Web.Mvc;
using Moq;
using System.Data.Entity;
using CineLogic.Repositories;
using CineLogic.Business.Programmation;
using CineLogic.Business;

namespace CineLogic.Programmation.Tests
{
    [TestClass()]
    public class SeancesServiceTests
    {
        private ISeanceService service;

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
                    SalleID = 1
                },
                new Seance()
                {
                    SeanceID = 2,
                    Titre = "Test Séance 2",
                    HeureDebut = new DateTime(2020, 1, 1, 15, 0, 0),
                    HeureFin = new DateTime(2020, 1, 1, 18, 0, 0),
                    SalleID = 1
                },
                new Seance()
                {
                    SeanceID = 3,
                    Titre = "Test Séance 3",
                    HeureDebut = new DateTime(2020, 1, 1, 18, 0, 0),
                    HeureFin = new DateTime(2020, 1, 1, 21, 0, 0),
                    SalleID = 1
                },
                new Seance()
                {
                    SeanceID = 4,
                    Titre = "Test Séance 4",
                    HeureDebut = new DateTime(2020, 1, 1, 12, 0, 0),
                    HeureFin = new DateTime(2020, 1, 1, 15, 0, 0),
                    SalleID = 2
                }
            };

            var data = list.AsQueryable();

            var mockSet = new Mock<DbSet<Seance>>();
            mockSet.As<IQueryable<Seance>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Seance>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Seance>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Seance>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<Seance>())).Callback<Seance>((s) => list.Add(s));
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(x => list.FirstOrDefault(d => d.SeanceID == (int)x[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Seance>())).Callback<Seance>((s) => list.Remove(s));

            var mockContext = new Mock<CineDBEntities>();
            mockContext.Setup(c => c.Seances).Returns(mockSet.Object);

            service = new SeanceService(new EFSeanceRepository(mockContext.Object));
        }
      
        [TestMethod]
        public void CreateSeance_ValidSeance_Successful()
        {
            //  Arrange.
            SeanceViewModel svm = new SeanceViewModel()
            {
                Titre = "Test Séance",
                HeureDebut = new DateTime(2020, 1, 2, 12, 00, 00),
                HeureFin = new DateTime(2020, 1, 2, 15, 00, 00),
                SalleID = 1
            };

            //  Act.
            service.CreateSeance(svm);

            //  Assert.
            Assert.AreEqual(4, service.GetSeancesBySalle(1).ToList().Count);
        }

        [TestMethod]
        public void CreateSeance_ConflictedSeance_ThrowsException()
        {
            //  Arrange.
            SeanceViewModel svm = new SeanceViewModel()
            {
                Titre = "Test Séance 1",
                HeureDebut = new DateTime(2020, 1, 1, 12, 0, 0),
                HeureFin = new DateTime(2020, 1, 1, 15, 0, 0),
                SalleID = 1
            };

            //  Act.
            //  Assert.
            Assert.ThrowsException<ScheduleException>(() => service.CreateSeance(svm));
        }

        [TestMethod]
        public void CreateSeance_InvalidTimes_ThrowsException()
        {
            //  Arrange.
            SeanceViewModel svm = new SeanceViewModel()
            {
                Titre = "Test Séance 1",
                HeureDebut = new DateTime(2020, 1, 1, 12, 0, 0),
                HeureFin = new DateTime(2020, 1, 1, 12, 0, 0),
                SalleID = 1
            };

            //  Act.
            //  Assert.
            Assert.ThrowsException<ScheduleException>(() => service.CreateSeance(svm));
        }

        [TestMethod]
        public void EditSeance_ValidSeance_Succesful()
        {
            //  Arrange.
            SeanceViewModel svm = new SeanceViewModel()
            {
                SeanceID = 1,
                Titre = "Test Séance Changed",
                HeureDebut = new DateTime(2020, 1, 1, 12, 0, 0),
                HeureFin = new DateTime(2020, 1, 1, 15, 0, 0),
                SalleID = 1
            };

            //  Act.
            service.UpdateSeance(svm);

            //  Assert.
            Assert.AreEqual(svm.Titre, service.GetSeance(svm.SeanceID).Titre);
        }

        [TestMethod]
        public void EditSeance_ConflictedSeance_ThrowsException()
        {
            //  Arrange.
            SeanceViewModel svm = new SeanceViewModel()
            {
                SeanceID = 1,
                Titre = "Test Séance Changed",
                HeureDebut = new DateTime(2020, 1, 1, 15, 0, 0),
                HeureFin = new DateTime(2020, 1, 1, 18, 0, 0),
                SalleID = 1
            };

            //  Act.
            //  Assert.
            Assert.ThrowsException<ScheduleException>(() => service.UpdateSeance(svm));
        }

        [TestMethod]
        public void EditSeance_InvalidTimes_ThrowsException()
        {
            //  Arrange.
            SeanceViewModel svm = new SeanceViewModel()
            {
                SeanceID = 1,
                Titre = "Test Séance Changed",
                HeureDebut = new DateTime(2020, 1, 1, 12, 0, 0),
                HeureFin = new DateTime(2020, 1, 1, 9, 0, 0),
                SalleID = 1
            };

            //  Act.
            //  Assert.
            Assert.ThrowsException<ScheduleException>(() => service.UpdateSeance(svm));
        }

        [TestMethod]
        public void DeleteSeance_ValidID_Succesful()
        {
            //  Arrange.
            int seanceID = 1;

            //  Act.
            service.DeleteSeance(seanceID);

            //  Assert.
            Assert.ThrowsException<NotFoundException>(() => service.GetSeance(seanceID));
        }

        [TestMethod]
        public void DeleteSeance_NotFound_ThrowsException()
        {
            //  Arrange.
            int seanceID = 10;

            //  Act.
            //  Assert.
            Assert.ThrowsException<NotFoundException>(() => service.DeleteSeance(seanceID));
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            if (service != null)
            {
                service.Dispose();
            }
        }
    }
}