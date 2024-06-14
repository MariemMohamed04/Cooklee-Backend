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

        public Order(string clientEmail, OrderAddress shippingAddress, ICollection<OrderItem> items, float subTotal)
        {
            ClientEmail = clientEmail;
            ShippingAddress = shippingAddress;
            Items = items;
            SubTotal = subTotal;
        }

        public string ClientEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderAddress ShippingAddress { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public float SubTotal { get; set; }
        //public int? DeliveryMethodId { get; set; }
        public DeliveryMethod? DeliveryMethod { get; set; }
        public float GetTotal() => SubTotal + DeliveryMethod.Cost;
    }
}
