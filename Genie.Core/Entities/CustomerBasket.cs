namespace Genie.Core.Entities;

public class CustomerBasket
{
    public CustomerBasket()
    {
    }

    public CustomerBasket(string id)
    {
        Id = id;
    }

    public String Id { get; set; }
    public List<BasketItem> items { get; set; } = new List<BasketItem>();
}
