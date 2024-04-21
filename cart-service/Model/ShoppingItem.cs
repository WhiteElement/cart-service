using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace cart_service.Model;

public class ShoppingItem
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    
    // foreign key
    public int ShoppingListId { get; set; }

    // reference navigation
    [JsonIgnore]
    public ShoppingList ShoppingList { get; set; } = null!;


}