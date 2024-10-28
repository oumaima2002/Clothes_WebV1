using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projet.Data;

namespace projet.Pages
{
    public class CartModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public List<CartItem> CartItems { get; set; }
        public string Message { get; set; } // propriete Message pour afficher si Cart est vide

        public CartModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            // Get les informations de session
            CartItems = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();

            // Set message if cart is empty
            if (CartItems.Count == 0)
            {
                Message = "Your cart is empty.";
            }
        }
         //Fonction de Modifier Quantite de produit dans cart
        public IActionResult OnPostUpdateQuantity(int productId, int change)
        {
            // Prendre les infos stockes dans session si trouvé on le recupere sinon on cree une session
            var cart = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();

            //retourner le premier produit selectioner par le client afin d'appliquer sur lui la fonction de update "augmenter sa quantite ou pas"
            var cartItem = cart.FirstOrDefault(item => item.Product.Id == productId);
            if (cartItem != null)//Si item a une quantite >0 c'est a dire objet item existe on peut modifiee sa quantite
            {
                cartItem.Quantity += change;//change c'est deja inialisé par 1 ou -1 selon la fonction clique "Dans partie Cart.cshtml "ligne 23-24""
                if (cartItem.Quantity <= 0)
                {
                    cart.Remove(cartItem);
                    Message = "Item removed from cart.";
                }
            }

            // Modifier la session avec les nouveaux valeur
            _httpContextAccessor.HttpContext.Session.SetObjectAsJson("cart", cart);

            // rederiger au meme page c'est adire refresh
            return RedirectToPage(); // comme appel de OnGet
        }
    }
}
