using cart_service.Model;

namespace cart_service.Service;

public class ShoppingItemService
{
    public async Task<List<ShoppingItem>> GetAll()
    {
        await using var dbContext = new DbContext.DbContext();
        return dbContext.ShoppingItems.ToList();
    }

    public async Task<ShoppingItem> AddRandomItem()
    {
        await using var dbContext = new DbContext.DbContext();

        var randomItem = new ShoppingItem()
        {
            Name = Guid.NewGuid().ToString()
        };

        dbContext.ShoppingItems.Add(randomItem);
        await dbContext.SaveChangesAsync();

        return randomItem;
    }

    public async Task<ShoppingItem> AddItemToList(int shoppingListId, ShoppingItem shoppingItem)
    {
        await using var dbContext = new DbContext.DbContext();
        var shoppingList = dbContext.ShoppingLists.FirstOrDefault(list => list.Id == shoppingListId);

        if (shoppingList == null)
            throw new KeyNotFoundException(
                $"Cannot add Item to List because there is no List with Id({shoppingListId}");

        if (string.IsNullOrEmpty(shoppingItem.Name))
            throw new ArgumentNullException($"No name for item specified");

        
        shoppingList.Items.Add(shoppingItem);
        await dbContext.SaveChangesAsync();

        return shoppingItem;
    }
}