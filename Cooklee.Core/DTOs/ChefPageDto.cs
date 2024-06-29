using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
    public class ChefPageDto
    {
        public int Id { get; set; }
        public string? DisplayName { get; set; }

        public string Email { get; set; }
        public string? IdImgURL { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WalletNumber { get; set; }
        public string? FullAddress { get; set; }
        public ChefPaymentMethod? paymentMethod { get; set; } = ChefPaymentMethod.Cash;

        [DefaultValue(false)]
        public bool IsActive { get; set; }
    }
}
