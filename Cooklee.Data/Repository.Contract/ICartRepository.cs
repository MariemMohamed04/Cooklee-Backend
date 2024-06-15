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
        Task<CustomerCart?> GetCartAsync(string cartId);
        Task<CustomerCart?> UpdateCartAsync(CustomerCart cart);
        Task<bool> DeleteCartAsync(string cartId);
        Task<CustomerCart?> AddCartItem(string cartId, CartItem item);
        Task<CustomerCart?> UpdateCartItemAsync(string cartId, CartItem cartItem);
        Task<CustomerCart?> DeleteCartItemAsync(string cartId, CartItem cartItem);
        Task<CustomerCart?> UpdateItemQuentity(string cartId, CartItem cartItem);
    }
}
