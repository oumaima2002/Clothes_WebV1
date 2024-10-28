using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projet.Data;


namespace projet.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _productService;

        public IndexModel(ProductService productService)
        {
            _productService = productService;
        }

        //proprite de binding pour associe l'input avec fonctiond recherche
        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public char Genre { get; set; }

        public IList<Product> Products { get; set; }

        public void OnGet()
        {
            var products = _productService.GetProducts();
            //faire une Linq query pour la filter les donnes souhaite
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                products = products.Where(p => p.Name.ToLower().Contains(SearchQuery.ToLower())).ToList();
            }

            if (Genre != '\0')//car \0 c'est selectionner rien
            {
                products = products.Where(p => p.Genre == Genre).ToList();//filtrer selon le genre associe si souhaite par le client
            }

            Products = products;
        }

        //Ajouter un item dans la cart
        public IActionResult OnGetAddToCart(int productId)
        {
            //recuperer le produit souhaite de la session ou cree une session sous nom cart en cas ou il n'existe pas avant
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();

            //Utilisateur en cliqnant sur buy donc il faut augmenter la quantite de ce produit souhaité
            var cartItem = cart.FirstOrDefault(item => item.Product.Id == productId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                //Si nouveau in ilialise par 1 
                var product = _productService.GetProductById(productId);
                cart.Add(new CartItem { Product = product, Quantity = 1 });
            }
            //creer session
            HttpContext.Session.SetObjectAsJson("cart", cart);

            return RedirectToPage("/Index"); //refresh de la page
        }
    }
}
