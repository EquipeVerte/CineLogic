using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CineLogic.Models;
using CineLogic.Business.Utilisateurs;
using CineLogic.Business;
using System.Linq;
using Moq;
using System.Data.Entity;

namespace CineLogic.Utilisateurs.Tests
{
    [TestClass]
    public class UsersServiceTests
    {
        private IUserService service;

        [TestInitialize]
        public void TestInit()
        {
            var list = new List<User>
            {
                new User()
                {
                  Login = "AdminTest",
                  PasswordHash = null,
                  Salt = null,
                  HashIterations =1000,
                  NomComplet = "AdminTest",
                  Type = "AdminTest"
                 },
                new User()
                {
                  Login = "AdminTest1",
                  PasswordHash = null,
                  Salt = null,
                  HashIterations =1000,
                  NomComplet = "AdminTest1",
                  Type = "AdminTest1"
                },
                new User()
                {
                  Login = "ProgTest",
                  PasswordHash = null,
                  Salt = null,
                  HashIterations =1000,
                  NomComplet = "ProgTest",
                  Type = "ProgTest"
                },
                new User()
        {
                  Login = "ProgTest1",
                  PasswordHash = null,
                  Salt = null,
                  HashIterations =1000,
                  NomComplet = "ProgTest1",
                  Type = "ProgTest1"
                }
    };

            var data = list.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<User>())).Callback<User>((s) => list.Add(s));
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(x => list.FirstOrDefault(d => d.Login == (string)x[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<User>())).Callback<User>((s) => list.Remove(s));

            var mockContext = new Mock<CineDBEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            service = new UserService(mockContext.Object);
        }

        [TestMethod]
        public void GetAllUser_Successful()
        {
            //  Arrange.
            //  Act.

            //  Assert.
            Assert.IsTrue(service.GetAllUser().Count == 4);
            Assert.IsTrue(service.GetAllUser()[0].Login == "AdminTest");
        }

        [TestMethod]
        public void CreateUser_ValidUser_Successful()
        {
            //  Arrange.
            User usager = new User()
            {
                Login = "ProgTest2",
                PasswordHash = null,
                Salt = null,
                HashIterations = 1000,
                NomComplet = "ProgTest2",
                Type = "ProgTest2"
            };

            //  Act.
            service.CreateUser(usager);

            //  Assert.
            Assert.IsTrue(service.GetAllUser()[4].Login == "ProgTest2");
        }

        [TestMethod]
        public void CreateUser_ValidUser_UnSuccessful()
        {
            //  Arrange.
            User usager = new User()
            {
                Login = "ProgTest2",
                PasswordHash = null,
                Salt = null,
                HashIterations = 1000,
                NomComplet = "ProgTest2",
                Type = "ProgTest2"
            };

            //  Act.
            service.CreateUser(usager);

            //  Assert.
            Assert.IsFalse(service.GetAllUser()[3].Login == "ProgTest");
        }

        [TestMethod]
        public void EditUser_ValidUser_Succesful()
        {
            //  Arrange.
            User usager = new User()
            {
                Login = "AdminTest",
                PasswordHash = null,
                Salt = null,
                HashIterations = 1000,
                NomComplet = "AdminTestChanged",
                Type = "AdminTest"
            };

            //  Act.
            service.UpdateUser(usager);

            //  Assert.
            Assert.AreEqual(usager.NomComplet, service.GetUserByLogin(usager).NomComplet);
        }


        [TestMethod]
        public void DeleteUser_ValidLogin_Succesful()
        {
            //  Arrange.
            User usager = new User()
            {
                Login = "ProgTest",
                PasswordHash = null,
                Salt = null,
                HashIterations = 1000,
                NomComplet = "ProgTest",
                Type = "ProgTest"
            };

            //  Act.
            service.DeleteUser(usager);

            //  Assert.
            Assert.IsTrue(service.GetAllUser().Count.Equals(3));
        }

        [TestMethod]
        public void DeleteUser_InValidLogin_Succesful()
        {
            //  Arrange.
            User usager = new User()
            {
                Login = "Toto",
                PasswordHash = null,
                Salt = null,
                HashIterations = 1000,
                NomComplet = "Toto",
                Type = "ProgTest"
            };

            //  Act.
            service.DeleteUser(usager);

            //  Assert.
            Assert.IsTrue(service.GetAllUser().Count.Equals(4));
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
