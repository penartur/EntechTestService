using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using EntechTestService.API.Host.Models;
using EntechTestService.Contracts.Internal.Repositories;
using EntechTestService.Contracts.Internal.Model;
using EntechTestService.Services.Validation;

namespace EntechTestService.API.Host.Controllers
{
    public class StoresController : ApiController
    {
        private readonly IStoreRepository repository;

        private readonly EmailValidator emailValidator;
        private readonly PhoneValidator phoneValidator;
        private readonly StoreNameValidator storeNameValidator;

        public StoresController(IStoreRepository repository)
        {
            this.repository = repository;

            emailValidator = new EmailValidator();
            phoneValidator = new PhoneValidator();
            storeNameValidator = new StoreNameValidator(repository);
        }

        private StoreViewModel ToViewModel(int id, StoreData data)
        {
            return new StoreViewModel
            {
                Id = id,
                CreatedAt = data.CreatedAt,
                LastUpdatedAt = data.LastUpdatedAt,
                Name = data.Name,
                Email = data.Email,
                Phone = data.Phone,
            };
        }

        private void AddEmailValidationInfo(ModelStateDictionary model, StoreModel store)
        {
            if (string.IsNullOrEmpty(store.Email))
            {
                model.AddModelError("Email", "Email field should not be empty");
                return;
            }

            if (!emailValidator.IsEmailCorrect(store.Email))
            {
                model.AddModelError("Email", "Email is in incorrect format");
                return;
            }
        }

        private void AddPhoneValidationInfo(ModelStateDictionary model, StoreModel store)
        {
            if (string.IsNullOrEmpty(store.Phone))
            {
                model.AddModelError("Phone", "Phone field should not be empty");
                return;
            }

            if (!phoneValidator.IsPhoneCorrect(store.Phone))
            {
                model.AddModelError("Phone", "Phone is in incorrect format");
                return;
            }
        }

        private void AddStoreNameValidationInfo(ModelStateDictionary model, StoreModel store)
        {
            if (string.IsNullOrEmpty(store.Name))
            {
                model.AddModelError("Name", "Name field should not be empty");
                return;
            }

            if (!storeNameValidator.IsStoreNameCorrect(store.Name))
            {
                model.AddModelError("Name", "Name is in incorrect format");
                return;
            }

            int? duplicateStoreId;
            if (!storeNameValidator.IsStoreNameUnique(store.Name, out duplicateStoreId))
            {
                model.AddModelError("Name", string.Format("Store name is duplicate; existing store id: '{0}'", duplicateStoreId));
            }
        }


        /// <summary>
        /// Returns null if there is no errors
        /// </summary>
        private ModelStateDictionary ValidateStore(StoreModel store)
        {
            var result = new ModelStateDictionary();
            AddEmailValidationInfo(result, store);
            AddPhoneValidationInfo(result, store);
            AddStoreNameValidationInfo(result, store);
            if (result.Any())
            {
                return result;
            }

            return null;
        }

        // GET: api/Stores
        public IEnumerable<StoreViewModel> Get()
        {
            return repository.FindStores(_ => true).Select(entity => ToViewModel(entity.Id, entity.Data));
        }

        // GET: api/Stores/5
        public IHttpActionResult Get(int id)
        {
            var data = repository.GetStore(id);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(ToViewModel(id, data));
        }

        // POST: api/Products
        public IHttpActionResult Post([FromBody]StoreModel value)
        {
            var validationResult = ValidateStore(value);
            if (validationResult != null)
            {
                return BadRequest(validationResult);
            }

            var createdDate = DateTime.Now;
            return Ok(repository.CreateStore(new StoreData
            {
                CreatedAt = createdDate,
                LastUpdatedAt = createdDate,
                Name = value.Name,
                Email = value.Email,
                Phone = value.Phone,
            }));
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]StoreModel value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
