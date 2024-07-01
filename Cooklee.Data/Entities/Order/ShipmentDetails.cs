using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Entities.Order
{
    public class ShipmentDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        public string Apartment { get; set; }
        public string Floor { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ShippingMethod { get; set; }
        public string PostalCode { get; set; }
    }
}
