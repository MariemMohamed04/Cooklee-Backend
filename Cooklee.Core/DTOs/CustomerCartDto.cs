using Cooklee.Data.Entities.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
    public class CustomerCartDto
    {
        public int Id { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
