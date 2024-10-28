namespace projet.Data
{
    //our main model for our website is product 
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int Categorie { get; set; }
        public char Genre { get; set; }
    }
}
