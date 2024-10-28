namespace projet.Data
{
    //CartItem of the chosen product by the client with the quantity indicated 
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
