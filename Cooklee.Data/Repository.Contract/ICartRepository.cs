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
        Task<CustomerCart?> GetCartAsync(int cartId);
        Task<CustomerCart?> UpdateCartAsync(CustomerCart cart);
        Task<bool> DeleteCartAsync(int cartId);
        Task<CustomerCart?> AddCartItem(int cartId, CartItem item);
        Task<CustomerCart?> UpdateCartItemAsync(int cartId, CartItem cartItem);
        Task<CustomerCart?> DeleteCartItemAsync(int cartId, CartItem cartItem);
        Task<CustomerCart?> UpdateItemQuentity(int cartId, CartItem cartItem);
    }
}
