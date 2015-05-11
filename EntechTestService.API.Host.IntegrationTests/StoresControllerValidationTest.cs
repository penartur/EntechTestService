using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntechTestService.API.Host.Controllers;
using EntechTestService.InMemoryDb;
using EntechTestService.API.Host.Models;
using System.Web.Http.ModelBinding;

namespace EntechTestService.API.Host.IntegrationTests
{
    [TestClass]
    public class StoresControllerValidationTest
    {
        private readonly Random random;

        private readonly Db db;

        private readonly StoresController storesController;

        public StoresControllerValidationTest()
        {
            random = new Random();
            db = new Db();
            storesController = new StoresController(db.StoreRepository);
        }

        private string CreateTestName()
        {
            return string.Format("TestStore{0}", random.Next());
        }

        private string CreateTestEmail()
        {
            return string.Format("test.store@test-store-{0}.example.com", random.Next());
        }

        private string CreateTestPhone()
        {
            return string.Format("+000{0}", random.Next(1, 1000000));
        }

        private void AssertSpecificResultType<T>(IHttpActionResult result, Action<T> specificAction)
        {
            Assert.IsInstanceOfType(result, typeof(T));
            specificAction((T)result);
        }

        private void AssertContainsError(ModelStateDictionary state, string fieldName, params string[] keywords)
        {
            Assert.IsTrue(state.ContainsKey(fieldName), "There is no info for field '{0}'", fieldName);
            Assert.IsTrue(state[fieldName].Errors.Any(), "There is no errors for field '{0}'", fieldName);
            foreach (var keyword in keywords)
            {
                Assert.IsTrue(
                    state[fieldName].Errors.Any(error => error.ErrorMessage.Contains(keyword)),
                    "Error messages for field '{0}' do not contain keyword '{1}'");
            }
        }

        [TestMethod]
        public void TestCreateStoreSuccess()
        {
            AssertSpecificResultType<OkNegotiatedContentResult<int>>(
                storesController.Post(new StoreModel
                {
                    Name = CreateTestName(),
                    Email = CreateTestEmail(),
                    Phone = CreateTestPhone(),
                }),
                result => { });
        }

        [TestMethod]
        public void TestCreateStoreWithEmptyNameFail()
        {
            AssertSpecificResultType<InvalidModelStateResult>(
                storesController.Post(new StoreModel
                {
                    Name = string.Empty,
                    Email = CreateTestEmail(),
                    Phone = CreateTestPhone(),
                }),
                result =>
                {
                    AssertContainsError(result.ModelState, "Name", "empty");
                });
        }

        [TestMethod]
        public void TestCreateStoreWithEmptyEmailFail()
        {
            AssertSpecificResultType<InvalidModelStateResult>(
                storesController.Post(new StoreModel
                {
                    Name = CreateTestName(),
                    Email = string.Empty,
                    Phone = CreateTestPhone(),
                }),
                result =>
                {
                    AssertContainsError(result.ModelState, "Email", "empty");
                });
        }

        [TestMethod]
        public void TestCreateStoreWithEmptyPhoneFail()
        {
            AssertSpecificResultType<InvalidModelStateResult>(
                storesController.Post(new StoreModel
                {
                    Name = CreateTestName(),
                    Email = CreateTestEmail(),
                    Phone = string.Empty,
                }),
                result =>
                {
                    AssertContainsError(result.ModelState, "Phone", "empty");
                });
        }

        [TestMethod]
        public void TestCreateStoreWithIncorrectNameFail()
        {
            AssertSpecificResultType<InvalidModelStateResult>(
                storesController.Post(new StoreModel
                {
                    Name = "abc def",
                    Email = CreateTestEmail(),
                    Phone = CreateTestPhone(),
                }),
                result =>
                {
                    AssertContainsError(result.ModelState, "Name", "format");
                });
        }

        [TestMethod]
        public void TestCreateStoreWithIncorrectEmailFail()
        {
            AssertSpecificResultType<InvalidModelStateResult>(
                storesController.Post(new StoreModel
                {
                    Name = CreateTestName(),
                    Email = "abc@def",
                    Phone = CreateTestPhone(),
                }),
                result =>
                {
                    AssertContainsError(result.ModelState, "Email", "format");
                });
        }

        [TestMethod]
        public void TestCreateStoreWithIncorrectPhoneFail()
        {
            AssertSpecificResultType<InvalidModelStateResult>(
                storesController.Post(new StoreModel
                {
                    Name = CreateTestName(),
                    Email = CreateTestEmail(),
                    Phone = "1234567890",
                }),
                result =>
                {
                    AssertContainsError(result.ModelState, "Phone", "format");
                });
        }

        [TestMethod]
        public void TestCreateStoreWithDuplicateNameFail()
        {
            var name = CreateTestName();
            int? id = null;

            AssertSpecificResultType<OkNegotiatedContentResult<int>>(
                storesController.Post(new StoreModel
                {
                    Name = name,
                    Email = CreateTestEmail(),
                    Phone = CreateTestPhone(),
                }),
                result => {
                    id = result.Content;
                });

            AssertSpecificResultType<InvalidModelStateResult>(
                storesController.Post(new StoreModel
                {
                    Name = name,
                    Email = CreateTestEmail(),
                    Phone = CreateTestPhone(),
                }),
                result =>
                {
                    AssertContainsError(result.ModelState, "Name", "duplicate", string.Format("'{0}'", id.Value));
                });
        }
    }
}
