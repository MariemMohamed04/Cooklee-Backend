using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Entities
{
    public class SpecialMeal : BaseEntity
    {
        public string S_MealName { get; set; }
        public string Description { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public Status? MealStatus { get; set; } =
        Status.Request;
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public int? ChefId { get; set; }
        public ChefPage? Chef { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsTaken { get; set; }

        public enum Status
        {
            Request,
            Wait,
            Done
        }
    }
}