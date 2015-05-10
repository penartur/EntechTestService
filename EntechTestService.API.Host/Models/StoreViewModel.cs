using System;

namespace EntechTestService.API.Host.Models
{
    public class StoreViewModel : StoreModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
