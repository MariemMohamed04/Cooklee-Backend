using Cooklee.Data.Entities.Cart;
using Cooklee.Data.Entities.Favourite;
using Cooklee.Data.Repository.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cooklee.Infrastructure.Repositories
{
    public class FavouriteRepository : IFavouriteRepository
    {
        private readonly IDatabase _db;
        public FavouriteRepository(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task<ClientFavourite?> GetFavouriteAsync(string favouriteId)
        {
            var favourite = await _db.StringGetAsync(favouriteId);
            return favourite.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ClientFavourite>(favourite);
        }

        public async Task<ClientFavourite?> UpdateFavouriteAsync(ClientFavourite favourite)
        {
            string serializedFavourite = JsonSerializer.Serialize(favourite);
            bool createdOrUpdated = await _db.StringSetAsync(favourite.Id, serializedFavourite, TimeSpan.FromDays(30));
            return createdOrUpdated ? await GetFavouriteAsync(favourite.Id) : null;
        }

        public async Task<bool> DeleteFavouriteAsync(string favouriteId)
        {
            return await _db.KeyDeleteAsync(favouriteId);
        }

        public async Task<ClientFavourite?> AddFavouriteItem(string favouriteId, FavouriteItem favouriteItem)
        {
            var favourite = await GetFavouriteAsync(favouriteId) ?? new ClientFavourite(favouriteId);
            var existingItem = favourite.Items.FirstOrDefault(i => i.Id == favouriteItem.Id);
            favourite.Items.Add(favouriteItem);
            return await UpdateFavouriteAsync(favourite);
        }

        public async Task<ClientFavourite?> UpdateFavouriteItemAsync(string favouriteId, FavouriteItem favouriteItem)
        {
            var favourite = await GetFavouriteAsync(favouriteId);
            if (favourite == null) return null;
            var itemToUpdate = favourite.Items.FirstOrDefault(item => item.Id == favouriteItem.Id);
            return await UpdateFavouriteAsync(favourite);
        }

        public async Task<ClientFavourite?> DeleteFavouriteItemAsync(string favouriteId, FavouriteItem favouriteItem)
        {
            var favourite = await GetFavouriteAsync(favouriteId);
            if (favourite == null) return null;
            var itemToRemove = favourite.Items.FirstOrDefault(item => item.Id == favouriteItem.Id);
            if (itemToRemove != null)
            {
                favourite.Items.Remove(itemToRemove);
                return await UpdateFavouriteAsync(favourite);
            }
            return null;
        }
    }
}
