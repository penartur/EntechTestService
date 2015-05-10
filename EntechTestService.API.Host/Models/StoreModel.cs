using System.ComponentModel.DataAnnotations;

namespace EntechTestService.API.Host.Models
{
    public class StoreModel
    {
        public string Name { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }
    }
}
