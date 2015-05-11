using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntechTestService.Contracts.Internal.Model;
using EntechTestService.Contracts.Internal.Repositories;

namespace EntechTestService.Services.Validation.UnitTests
{
    [TestClass]
    public class StoreNameValidatorTest
    {
        private class FakeRepository : IStoreRepository
        {
            private readonly string duplicateName;

            private readonly int idOfDuplicateStore;

            public FakeRepository(string duplicateName, int idOfDuplicateStore)
            {
                this.duplicateName = duplicateName;
                this.idOfDuplicateStore = idOfDuplicateStore;
            }

            public int CreateStore(StoreData newProductData)
            {
                throw new NotImplementedException();
            }

            public void DeleteStore(int id)
            {
                throw new NotImplementedException();
            }

            public ICollection<IdentifiedDataEntity<StoreData>> FindStores(Expression<Predicate<StoreData>> filter)
            {
                if (filter.Compile()(new StoreData { Name = duplicateName }))
                {
                    return new[] { new IdentifiedDataEntity<StoreData>(idOfDuplicateStore, new StoreData { Name = duplicateName }) };
                }

                return new IdentifiedDataEntity<StoreData>[0];
            }

            public StoreData GetStore(int id)
            {
                throw new NotImplementedException();
            }

            public void UpdateStore(int id, StoreData newProductData)
            {
                throw new NotImplementedException();
            }
        }

        private readonly Random random;

        private readonly int idOfDuplicateStore;

        private readonly string duplicateStoreName;

        private readonly StoreNameValidator storeNameValidator;

        public StoreNameValidatorTest()
        {
            random = new Random();
            idOfDuplicateStore = random.Next(1, 1000000);
            duplicateStoreName = string.Format("store_{0}_{1}", random.Next(1, 1000000), random.Next(1, 1000000));
            storeNameValidator = new StoreNameValidator(new FakeRepository(duplicateStoreName, idOfDuplicateStore));
        }

        [TestMethod]
        public void TestSimpleNameCorrect()
        {
            Assert.IsTrue(storeNameValidator.IsStoreNameCorrect("TestStore"));
        }

        [TestMethod]
        public void TestSimpleNameWithUnderscoresAndNumbersCorrect()
        {
            Assert.IsTrue(storeNameValidator.IsStoreNameCorrect("Test_Store_123"));
        }

        [TestMethod]
        public void TestStoreNameWithSpacesIncorrect()
        {
            Assert.IsFalse(storeNameValidator.IsStoreNameCorrect("Test store"));
        }

        [TestMethod]
        public void TestStoreNameUnique()
        {
            int? validatorOutId;
            Assert.IsTrue(storeNameValidator.IsStoreNameUnique("TestStore", out validatorOutId));
        }

        [TestMethod]
        public void TestStoreNameDuplicate()
        {
            int? validatorOutId;
            Assert.IsFalse(storeNameValidator.IsStoreNameUnique(duplicateStoreName, out validatorOutId));
            Assert.AreEqual(idOfDuplicateStore, validatorOutId);
        }
    }
}
