using projet.Data;
using System.Collections.Generic;
using System.Linq;

public class ProductService
{
    private readonly ProductDbContext _context;

    public ProductService(ProductDbContext context)
    {
        _context = context;
    }

    // Method to get a product by its ID
    public Product GetProductById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }

    // Other existing methods
    public IList<Product> GetProducts()
    {
        return _context.Products.ToList();
    }
}
