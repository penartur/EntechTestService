using EntechTestService.Contracts.Internal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntechTestService.Contracts.Internal.Repositories
{
    public interface IStoreRepository
    {
        ICollection<IdentifiedDataEntity<StoreData>> GetAllStores();

        StoreData GetStore(int id);

        int CreateStore(StoreData newProductData);

        void UpdateStore(int id, StoreData newProductData);

        void DeleteStore(int id);
    }
}
