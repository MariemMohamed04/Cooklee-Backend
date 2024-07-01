using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
    public class ClientFavouriteDto
    {
        [Required]
        public string Id { get; set; }
        public List<FavouriteItemDto> Items { get; set; }
    }
}
