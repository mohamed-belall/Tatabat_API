using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.API.Dtos.Redis
{
    public class CustomerBasketDTO
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDTO> Items { get; set; }
    }
}
