using System;
using System.Linq;
using System.Text.RegularExpressions;
using EntechTestService.Contracts.Internal.Repositories;

namespace EntechTestService.Services.Validation
{
    public class StoreNameValidator
    {
        private static readonly Regex StoreNameRegex = new Regex("^\\w+$", RegexOptions.Compiled);

        private readonly IStoreRepository storeRepository;

        public StoreNameValidator(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        public bool IsStoreNameCorrect(string storeName)
        {
            return StoreNameRegex.IsMatch(storeName);
        }

        /// <summary>
        /// Returns null if storeName is unique, existing store ID otherwise
        /// </summary>
        public bool IsStoreNameUnique(string storeName, out int? duplicateStoreId)
        {
            var duplicateStores = storeRepository.FindStores(storeData => storeData.Name.Equals(storeName, StringComparison.InvariantCultureIgnoreCase));
            if (duplicateStores.Any())
            {
                duplicateStoreId = duplicateStores.Single().Id;
                return false;
            }

            duplicateStoreId = null;
            return true;
        }
    }
}
