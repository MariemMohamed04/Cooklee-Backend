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
        public int id { get; set; }

        [Required]
        public string ClientEmail { get; set; }

        [Required]
        public string CartId { get; set; }
        public ShipmentDetailsDto ShipmentDetails { get; set; }
    }
}
