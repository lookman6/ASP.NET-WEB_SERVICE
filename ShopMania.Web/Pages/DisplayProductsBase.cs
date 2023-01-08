using Microsoft.AspNetCore.Components;
using ShopMania.Models.Dtos;

namespace ShopMania.Web.Pages
{
    public class DisplayProductsBase:ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
