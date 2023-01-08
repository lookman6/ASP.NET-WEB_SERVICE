using ShopMania.Models.Dtos;

namespace ShopMania.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDto>> GetItems(int userId);
        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItemDto> DeleteItem(int id);
        Task<CartItemDto> UpdateQty(CartItemUpdateQtyDto cartItemQtyUpdateDto);

        event Action<int> OnShoppingCartChanged;
        void RaiseEventOnShoppingCartChanged(int totalQty);

    }
}
