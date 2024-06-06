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
        public enum Status { Request, Wait, Done }
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}