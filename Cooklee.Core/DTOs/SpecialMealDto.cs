﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
    public class SpecialMealDto
    {
        public string S_MealName { get; set; }
        public string Description { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public Status MealStatus { get; set; }
        public int ClientId { get; set; }
        public int ChefPageId { get; set; }

        public enum Status
        {
            Request,
            Wait,
            Done
        }
    }
}
