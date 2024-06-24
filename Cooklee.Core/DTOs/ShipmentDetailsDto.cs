using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
    public class ShipmentDetailsDto
    {

        public ShipmentDetailsDto()
        {
            City = "Alexandria";
            Country = "Egypt";
            State = "Alexandria Governorate";
            ShippingMethod = "PKG";
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]

        public string Building { get; set; }
        [Required]

        public string Apartment { get; set; }
        [Required]

        public string Floor { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ShippingMethod { get; set; }
        [Required]

        public string PostalCode { get; set; }


    }
}
