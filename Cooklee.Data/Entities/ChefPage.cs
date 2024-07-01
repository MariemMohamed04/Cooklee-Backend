using Cooklee.Data.Entities;
using Cooklee.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Entities
{
    public enum ChefPaymentMethod
    {
        [EnumMember(Value = "Cash")]
        Cash,

        [EnumMember(Value = "Mobile Wallet")]
        MobileWallet,

        [EnumMember(Value = "Card")]
        Card,
    }


    public class ChefPage : BaseEntity
	{
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string? IdImgURL { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WalletNumber { get; set; }
        public string? FullAddress { get; set; }
        public ChefPaymentMethod? paymentMethod { get; set; } = ChefPaymentMethod.Cash;

        [DefaultValue(false)]
        public bool IsActive { get; set; }


        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public Client Client { get; set; }

   

        //public string AppUserId { get; set; }
        //public AppUser AppUser { get; set; }

        public ICollection<Meal> Meals { get; set; }
        public List<SpecialMeal> SpecialMeals { get; set; }
        public ChefPage()
        {
            Meals = new List<Meal>();
        }

    }
}
