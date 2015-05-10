using EntechTestService.API.Host.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EntechTestService.API.Host.Controllers
{
    public class StoresController : ApiController
    {
        // GET: api/Stores
        public IEnumerable<StoreViewModel> Get()
        {
            return new StoreViewModel[0];
        }

        // GET: api/Stores/5
        public IHttpActionResult Get(int id)
        {
            return Ok(new StoreViewModel
            {
                Id = id,
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                Name = string.Format("Store{0}", id),
                Email = "test@example.com",
                Phone = "+1234567890",
            });
        }

        // POST: api/Products
        public void Post([FromBody]StoreModel value)
        {
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]StoreModel value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
