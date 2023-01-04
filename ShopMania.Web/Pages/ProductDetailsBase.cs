using Microsoft.AspNetCore.Components;
using ShopMania.Models.Dtos;
using ShopMania.Web.Services.Contracts;

namespace ShopMania.Web.Pages
{
    public class ProductDetailsBase:ComponentBase
    {
        [Parameter]
        public int id { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        public ProductDto Product { get; set; }

        public string ErrorMessage { get; set; }

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
    }
}
