using Cooklee.Data.Entities.Favourite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Repository.Contract
{
    public interface IFavouriteRepository
    {
        Task<ClientFavourite?> GetFavouriteAsync(string favouriteId);
        Task<ClientFavourite?> UpdateFavouriteAsync(ClientFavourite favourite);
        Task<bool> DeleteFavouriteAsync(string favouriteId);
        Task<ClientFavourite?> AddFavouriteItem(string favouriteId, FavouriteItem favouriteItem);
        Task<ClientFavourite?> UpdateFavouriteItemAsync(string favouriteId, FavouriteItem favouriteItem);
        Task<ClientFavourite?> DeleteFavouriteItemAsync(string favouriteId, FavouriteItem favouriteItem);
    }
}
