using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EntechTestService.Contracts.Internal.Model;
using EntechTestService.Contracts.Internal.Repositories;

namespace EntechTestService.InMemoryDb
{
    internal class StoreRepository : IStoreRepository
    {
        private readonly Repository<StoreData> impl = new Repository<StoreData>();

        public StoreRepository()
        {
            impl.Insert(new StoreData
            {
                Name = "StoreA",
                Email = "test@storea.example.com",
                Phone = "+1234567890",
            });
            impl.Insert(new StoreData
            {
                Name = "StoreB",
                Email = "test@storeb.example.com",
                Phone = "+0987654321",
            });
        }

        public int CreateStore(StoreData newStoreData)
        {
            return impl.Insert(newStoreData);
        }

        public void DeleteStore(int id)
        {
            impl.Delete(id);
        }

        public ICollection<IdentifiedDataEntity<StoreData>> FindStores(Expression<Predicate<StoreData>> filter)
        {
            return impl.Find(filter.Compile());
        }

        public StoreData GetStore(int id)
        {
            return impl.Get(id);
        }

        public void UpdateStore(int id, StoreData newStoreData)
        {
            impl.Update(id, newStoreData);
        }
    }
}
