using System.ComponentModel.DataAnnotations;
using Talabat.API.Helper.CustomAttribute;

namespace Talabat.API.Dtos
{
    public class OrderDTO
    {
        [Required]
        public string BuyerEmail { get; set; }

        [Required]
     
        public string BasketId   { get; set; }
         
        [Required]
        
        public int DelivaryMethodId { get; set; }

        public AddressDTO ShippingAddress { get; set; }
    }
}
