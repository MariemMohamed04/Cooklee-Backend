using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
    public class ReviewDto
    {
        public string? Comment { get; set; }

        [Range(1, 5)]
        public int Rate { get; set; } = 1;
        public int ClientId { get; set; }
        public int MealId { get; set; }
    }
}
