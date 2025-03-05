﻿using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.API.Dtos
{
    public class OrderToReturnDTO
    {

        public int Id { get; set; }
        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } 

        public string Status { get; set; }

        public Address ShippingAddress { get; set; }


        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

     

        public string PaymentIntentId { get; set; } = string.Empty;

      
      
        public string DeliveryMethod { get; set; } 
        public decimal DeliveryMethodCost { get; set; }

     
        public ICollection<OrderItemDTO> Items { get; set; } = new HashSet<OrderItemDTO>(); 
    }
}
