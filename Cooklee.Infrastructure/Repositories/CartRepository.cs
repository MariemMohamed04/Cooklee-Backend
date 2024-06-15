﻿using Cooklee.Data.Entities.Cart;
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
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _db;
        public CartRepository(IConnectionMultiplexer redis) 
        {
            _db = redis.GetDatabase();
        }

        public async Task<CustomerCart?> GetCartAsync(string cartId)
        {
            var cart = await _db.StringGetAsync(cartId);
            return cart.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerCart>(cart);
        }

        public async Task<CustomerCart?> UpdateCartAsync(CustomerCart cart)
        {
            string serializedCart = JsonSerializer.Serialize(cart);
            bool createdOrUpdated = await _db.StringSetAsync(cart.Id, serializedCart, TimeSpan.FromDays(30));
            return createdOrUpdated ? await GetCartAsync(cart.Id) : null;
        }

        public async Task<bool> DeleteCartAsync(string cartId)
        {
            return await _db.KeyDeleteAsync(cartId);
        }

        public async Task<CustomerCart?> AddCartItem(string cartId, CartItem item)
        {
            var cart = await GetCartAsync(cartId) ?? new CustomerCart(cartId);
            var existingItem = cart.Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                if (item.Quantity == 10)
                {
                    return null;
                }
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Items.Add(item);
            }
            return await UpdateCartAsync(cart);
        }

        public async Task<CustomerCart?> UpdateCartItemAsync(string cartId, CartItem cartItem)
        {
            var cart = await GetCartAsync(cartId);
            if (cart == null) return null;

            var itemToUpdate = cart.Items.FirstOrDefault(item => item.Id == cartItem.Id);
            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = cartItem.Quantity;
                return await UpdateCartAsync(cart);
            }
            return null;
        }

        public async Task<CustomerCart?> DeleteCartItemAsync(string cartId, CartItem cartItem)
        {
            var cart = await GetCartAsync(cartId);
            if (cart == null) return null;
            var itemToRemove = cart.Items.FirstOrDefault(item => item.Id == cartItem.Id);
            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
                return await UpdateCartAsync(cart);
            }
            return null;
        }

        public async Task<CustomerCart?> UpdateItemQuentity(string cartId, CartItem cartItem)
        {
            var cart = await GetCartAsync(cartId);
            if (cart == null) return null;
            var itemToUpdate = cart.Items.FirstOrDefault(i => i.Id == cartItem.Id);
            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = cartItem.Quantity;
                return await UpdateCartAsync(cart);
            }
            return null;
        }
    }
}
