using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CineLogic.Models;
using CineLogic.Business.Utilisateurs;
using System.Linq;
using Moq;
using System.Data.Entity;

namespace CineLogicUnitTests
{
    /// <summary>
    /// Description résumée pour UnitTest2
    /// </summary>
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
        public void TestMethod1()
        {
            Assert.IsTrue(service.GetAllUser().Count == 4);
            Assert.IsTrue(service.GetAllUser()[0].Login == "AdminTest");
        }
    }
}
