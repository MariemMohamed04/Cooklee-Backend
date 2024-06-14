using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
    public class CartItemDto
    {
        [Required]
        public int Id { get; set; }
        public string MealName { get; set; }
        public string PictureUrl { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
