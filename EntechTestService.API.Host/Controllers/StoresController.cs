using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EntechTestService.API.Host.Models;
using EntechTestService.Contracts.Internal.Repositories;
using EntechTestService.Contracts.Internal.Model;

namespace EntechTestService.API.Host.Controllers
{
    public class StoresController : ApiController
    {
        private readonly IStoreRepository repository;

        public StoresController(IStoreRepository repository)
        {
            this.repository = repository;
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

        // GET: api/Stores
        public IEnumerable<StoreViewModel> Get()
        {
            return repository.GetAllStores().Select(entity => ToViewModel(entity.Id, entity.Data));
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
        public int Post([FromBody]StoreModel value)
        {
            var createdDate = DateTime.Now;
            return repository.CreateStore(new StoreData
            {
                CreatedAt = createdDate,
                LastUpdatedAt = createdDate,
                Name = value.Name,
                Email = value.Email,
                Phone = value.Phone,
            });
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
