using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
    public class OrderDto
    {
        [Required]
        public string ClientEmail { get; set; }

        [Required]
        public string CartId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
