using Cooklee.Data.Entities.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
    public class ClientCartDto
    {
        [Required]
        public string Id { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
