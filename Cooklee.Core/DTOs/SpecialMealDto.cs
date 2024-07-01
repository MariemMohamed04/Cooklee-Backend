using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
    public class SpecialMealDto
    {
        public int id { get; set; }
        public string S_MealName { get; set; }
        public string Description { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public Status? MealStatus { get; set; } = Status.Request;
        public string  UserId { get; set; }
        public string? Client { get; set; }
        //public int ChefPageId { get; set; }
        public string? ChefPage { get; set; }

        [DefaultValue("false")]
        public bool IsAccepeted { get; set; }        
        
        [DefaultValue("false")]
        public bool IsTaken { get; set; }

        public enum Status
        {
            Request,
            Wait,
            Done
        }
    }

}
