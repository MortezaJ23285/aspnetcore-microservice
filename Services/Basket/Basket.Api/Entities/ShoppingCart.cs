namespace Basket.Api.Entities;

public class ShoppingCart
{
    public ShoppingCart()
    {
    }

    public ShoppingCart(string username)
    {
        Username = username;
    }

    public string Username { get; set; }

    public List<ShoppingCartItem> Items { get; set; }

    public decimal TotalPrice => Items?.Sum(item => item.Price * item.Quantity) ?? 0;
}