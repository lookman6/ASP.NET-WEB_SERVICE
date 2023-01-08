using Microsoft.EntityFrameworkCore;
using ShopMania.API.Data;
using ShopMania.API.Entities;
using ShopMania.API.Repositories.Contracts;
using ShopMania.Models.Dtos;

namespace ShopMania.API.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopManiaDbContext shopManiaDbContext;

        public ShoppingCartRepository(ShopManiaDbContext shopManiaDbContext)
        {
            this.shopManiaDbContext = shopManiaDbContext;
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await this.shopManiaDbContext.CartItems.AnyAsync(c => c.CartId == cartId &&
                                                                     c.ProductId == productId);

        }
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var item = await(from product in this.shopManiaDbContext.Products
                                 where product.Id == cartItemToAddDto.ProductId
                                 select new CartItem
                                 {
                                     CartId = cartItemToAddDto.CartId,
                                     ProductId = product.Id,
                                     Qty = cartItemToAddDto.Qty
                                 }).SingleOrDefaultAsync();

                if (item != null)
                {
                    var result = await this.shopManiaDbContext.CartItems.AddAsync(item);
                    await this.shopManiaDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }

            return null;
        }

        public async Task<CartItem> DeleteItem(int id)
        {
            var item = await this.shopManiaDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                this.shopManiaDbContext.CartItems.Remove(item);
                await this.shopManiaDbContext.SaveChangesAsync();
            }

            return item;
        }

        public async Task<CartItem> GetItem(int id)
        {
            return await(from cart in this.shopManiaDbContext.Carts
                         join cartItem in this.shopManiaDbContext.CartItems
                         on cart.Id equals cartItem.CartId
                         where cartItem.Id == id
                         select new CartItem
                         {
                             Id = cartItem.Id,
                             ProductId = cartItem.ProductId,
                             Qty = cartItem.Qty,
                             CartId = cartItem.CartId
                         }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await(from cart in this.shopManiaDbContext.Carts
                         join cartItem in this.shopManiaDbContext.CartItems
                         on cart.Id equals cartItem.CartId
                         where cart.UserId == userId
                         select new CartItem
                         {
                             Id = cartItem.Id,
                             ProductId = cartItem.ProductId,
                             Qty = cartItem.Qty,
                             CartId = cartItem.CartId
                         }).ToListAsync();
        }

        public async Task<CartItem> UpdateQty(int id, CartItemUpdateQtyDto cartItemQtyUpdateDto)
        {
            var item = await this.shopManiaDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                item.Qty = cartItemQtyUpdateDto.Qty;
                await this.shopManiaDbContext.SaveChangesAsync();
                return item;
            }

            return null;
        }
    }
}
