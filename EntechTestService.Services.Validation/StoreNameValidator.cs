using System;
using EntechTestService.Contracts.Internal.Repositories;

namespace EntechTestService.Services.Validation
{
    public class StoreNameValidator
    {
        private readonly IStoreRepository storeRepository;

        public StoreNameValidator(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        public bool IsStoreNameCorrect(string storeName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns null if storeName is unique, existing store ID otherwise
        /// </summary>
        public bool IsStoreNameUnique(string storeName, out int? duplicateStoreId)
        {
            throw new NotImplementedException();
        }
    }
}
