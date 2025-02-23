using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class DeliveryMethod : BaseEntity
    {
        // we create parameterless constructor for EF Core  to make migration
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string description, decimal coast, string deliveryTime)
        {
            ShortName = shortName;
            Description = description;
            Cost = coast;
            DeliveryTime = deliveryTime;
        }

        public string ShortName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string DeliveryTime { get; set; }


    }
}
