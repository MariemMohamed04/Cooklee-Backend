using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Entities.Order
{
    public class Order : BaseEntity
    {
        public Order()
        {
            //
        }

        public Order(string clientEmail, ShipmentDetails shipmentDetails, ICollection<OrderItem> items, float subTotal)
        {
            ClientEmail = clientEmail;
            ShipmentDetails = shipmentDetails;
            Items = items;
            SubTotal = subTotal;
        }

        public string ClientEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public ShipmentDetails ShipmentDetails { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public float SubTotal { get; set; }
        public float GetTotal() => SubTotal;
    }
}
