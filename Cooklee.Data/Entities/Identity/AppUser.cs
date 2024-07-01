using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        //public string Fname { get; set; }
        //public string Lname { get; set; }
        public string? DisplayName { get; set; }
        public Address? Address { get; set; }    
        public int? ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
