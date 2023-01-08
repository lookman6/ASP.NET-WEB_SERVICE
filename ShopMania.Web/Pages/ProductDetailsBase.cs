using Microsoft.AspNetCore.Components;
using ShopMania.Models.Dtos;
using ShopMania.Web.Services;
using ShopMania.Web.Services.Contracts;

namespace ShopMania.Web.Pages
{
    public class ProductDetailsBase:ComponentBase
    {
        [Parameter]
        public int id { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public ProductDto Product { get; set; }

        public string ErrorMessage { get; set; }

        private List<CartItemDto> ShoppingCartItems { get; set; }

       /* [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }*/

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Product = await ProductService.GetItem(id);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }

        protected async Task AddToCart_Click(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var cartItemDto = await ShoppingCartService.AddItem(cartItemToAddDto);

                if (cartItemDto != null)
                {
                    ShoppingCartItems.Add(cartItemDto);
                    //await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);
                }

                NavigationManager.NavigateTo("/ShoppingCart");
            }
            catch (Exception)
            {

                //Log Exception
            }
        }
    }
}
