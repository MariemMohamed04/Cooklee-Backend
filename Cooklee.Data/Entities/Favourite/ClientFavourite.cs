using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Entities.Favourite
{
    public class ClientFavourite
    {
        public string Id { get; set; }
        public List<FavouriteItem> Items { get; set; }
        public ClientFavourite(string id) 
        { 
            Id = id;
            Items = new List<FavouriteItem>();
        }
    }
}
