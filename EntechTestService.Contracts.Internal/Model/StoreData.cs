using System;

namespace EntechTestService.Contracts.Internal.Model
{
    public class StoreData
    {
        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
