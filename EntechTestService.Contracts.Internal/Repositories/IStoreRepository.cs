using EntechTestService.Contracts.Internal.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EntechTestService.Contracts.Internal.Repositories
{
    public interface IStoreRepository
    {
        ICollection<IdentifiedDataEntity<StoreData>> FindStores(Expression<Predicate<StoreData>> filter);

        StoreData GetStore(int id);

        int CreateStore(StoreData newProductData);

        void UpdateStore(int id, StoreData newProductData);

        void DeleteStore(int id);
    }
}
