
using Microsoft.EntityFrameworkCore;

namespace PastryShop.Models;

public class ShoppingCart : IShoppingCart
{
    private readonly PastryShopDbContext _pastryShopDbContext;
    public string? ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = [];

    private ShoppingCart(PastryShopDbContext pastryShopDbContext )
    {
        _pastryShopDbContext = pastryShopDbContext;
    }


    //This method is called in the program.cs to provide a scoped instance of the shopping cart
    public static ShoppingCart GetCart(IServiceProvider services)
    {
        //Get session through the DI system; session gives ability to store information about a returning user
        ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

        PastryShopDbContext context = services.GetService<PastryShopDbContext>() ?? throw new Exception("Error Initializing");

        //If the user is returning, the cartId will be retrieved from the session instance, but a new guid will be created the first time a user visits
        
        string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

        session?.SetString("CartId",cartId);

        return new ShoppingCart(context)
        {
            ShoppingCartId = cartId
        };
    }


    public void AddToCart(Pie pie)
    {
        //Check the shopping cart items in the database if for that ShoppingCartId, there is an existing pie with the id of the pie we are trying to insert
        ShoppingCartItem? shoppingCartItem = _pastryShopDbContext.ShoppingCartItems.SingleOrDefault(s => s.Pie.PieId == pie.PieId &&
        s.ShoppingCartId == ShoppingCartId);

        //If there no pie with the id on the shopping cart, create a new shoppingcartitem and add it to the shoppingcart
        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem
            {
                ShoppingCartId = ShoppingCartId,
                Pie = pie,
                Amount = 1
            };
            _pastryShopDbContext.ShoppingCartItems.Add(shoppingCartItem);
        }
        else//if there is an existing pie with that id in the shopping cart, we just increase the number of pie
        {
            shoppingCartItem.Amount += 1;
        }
        _pastryShopDbContext.SaveChanges();
    }

    public int RemoveFromCart(Pie pie)
    {
        ShoppingCartItem? shoppingCartItem = _pastryShopDbContext.ShoppingCartItems.SingleOrDefault(s => s.Pie.PieId == pie.PieId &&
        s.ShoppingCartId == ShoppingCartId);
         
        int itemCount = 0;

        if (shoppingCartItem is not null)
        {            
            if (shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount -= 1;
                itemCount = shoppingCartItem.Amount;
            }
            else
            {
                _pastryShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
            }            
        }
        _pastryShopDbContext.SaveChanges();

        return itemCount;
    }

    public void ClearCart()
    {
        var cartItems = _pastryShopDbContext.
            ShoppingCartItems.
            Where(cart => cart.ShoppingCartId == ShoppingCartId);

        _pastryShopDbContext.ShoppingCartItems.RemoveRange(cartItems);

        _pastryShopDbContext.SaveChanges();
    }

    public decimal GetShoppingCartTotal()
    {
        decimal total = _pastryShopDbContext.
            ShoppingCartItems.
            Where(c => c.ShoppingCartId == ShoppingCartId).
            Select(c => c.Pie.Price * c.Amount).
            Sum();

        return total;
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ??=
            _pastryShopDbContext.
            ShoppingCartItems.
            Where(c => c.ShoppingCartId == ShoppingCartId).
            Include(s => s.Pie).
            ToList();
    }


}
