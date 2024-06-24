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
        [Required]
        public string MealName { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least one item!!")]
        public int Quantity { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0!!!")]
        public float Price { get; set; }
    }
}
