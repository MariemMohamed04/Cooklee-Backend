using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Entities.Cart
{
    public class ClientCart
    {
        public string Id { get; set; }
        public List<CartItem> Items { get; set; }
        public ClientCart(string id)
        {
            Id = id;
            Items = new List<CartItem>();
        }
    }
}
