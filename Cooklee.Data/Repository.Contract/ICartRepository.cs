using Cooklee.Data.Entities.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Repository.Contract
{
    public interface ICartRepository
    {
        Task<ClientCart?> GetCartAsync(string cartId);
        Task<ClientCart?> UpdateCartAsync(ClientCart cart);
        Task<bool> DeleteCartAsync(string cartId);
        Task<ClientCart?> AddCartItem(string cartId, CartItem item);
        Task<ClientCart?> UpdateCartItemAsync(string cartId, CartItem cartItem);
        Task<ClientCart?> DeleteCartItemAsync(string cartId, CartItem cartItem);
        Task<ClientCart?> UpdateItemQuentity(string cartId, CartItem cartItem);
    }
}
