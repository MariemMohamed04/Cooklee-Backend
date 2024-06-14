using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Entities.Cart
{
    public class CustomerCart
    {
        public int Id { get; set; }
        public List<CartItem> Items { get; set; }
        #region Extra
        //public int? DeliveryMethodId { get; set; }
        //public decimal ShippingPrice { get; set; }
        //public string? PaymentIntentId { get; set; }
        //public string? ClientSecret { get; set; } 
        #endregion

        public CustomerCart(int id)
        {
            Id = id;
            Items = new List<CartItem>();
        }
    }
}
