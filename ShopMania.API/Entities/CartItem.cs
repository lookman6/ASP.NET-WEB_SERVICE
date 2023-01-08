namespace ShopMania.API.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        public int CartId { get; set; } //foreign key beetween Cart and CartItem

        public int ProductId { get; set; }

        public int Qty { get; set; } //Quantity of the item in the shopping cart
    }
}
