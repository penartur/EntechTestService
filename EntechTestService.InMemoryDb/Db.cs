using EntechTestService.Contracts.Internal.Repositories;

namespace EntechTestService.InMemoryDb
{
    public class Db
    {
        public readonly IStoreRepository StoreRepository = new StoreRepository();
    }
}
