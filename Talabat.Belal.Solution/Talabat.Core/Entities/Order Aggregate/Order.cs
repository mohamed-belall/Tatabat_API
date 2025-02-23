using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public   class Order : BaseEntity
    {
        // here,  Accessible Empty Parameterless Constructor must be Exist
        // we create parameterless constructor for EF Core  to make migration
        public Order()
        {

        }

        public Order(string buyerEmail, Address shippingAddress, decimal subTotal, int deliveryMethodId, DeliveryMethod deliveryMethod, ICollection<OrderItem> items)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            SubTotal = subTotal;
            DeliveryMethodId = deliveryMethodId;
            DeliveryMethod = deliveryMethod;
            Items = items;
        }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }


        public decimal SubTotal { get; set; }


        #region way for derivative attribute
        //[NotMapped] // derivative attribute not stored in DB and get from make formula on another attribute
        //public decimal Total => SubTotal + DeliveryMethod.Coast; 

        // another way
        //[NotMapped] 
        //public decimal Total { get { return SubTotal + DeliveryMethod.Coast; } }

        // another way
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;
        #endregion

        public string PaymentIntentId { get; set; } = string.Empty;

        //====================================== relation ==========================================

        public int DeliveryMethodId { get; set; } // Foreign Key
        public DeliveryMethod DeliveryMethod { get; set; } // Navigational Property [ONE]

        //----------------------------------------------
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>(); // Navigational Property [Many]

    }
}
