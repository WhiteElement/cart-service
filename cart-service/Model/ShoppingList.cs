using System.ComponentModel.DataAnnotations;

namespace cart_service.Model;

public class ShoppingList
{
    [Key]
    public int? Id { get; set; }

    public ICollection<ShoppingItem> Items { get; set; }
    public string? Name { get; set; }
    public DateTime? LastEdited { get; set; }

    public ShoppingList()
    {
        Items = new List<ShoppingItem>();
    }
}