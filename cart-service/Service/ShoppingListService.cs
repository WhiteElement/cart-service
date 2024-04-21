using cart_service.Model;

namespace cart_service.Service;

public class ShoppingListService
{
    public async Task<List<ShoppingList>> GetAll()
    {
        await using var dbContext = new DbContext.DbContext();

        return dbContext.ShoppingLists.ToList();
    }

    public async Task<ShoppingList> GetList(int shoppingListId)
    {
        await using var dbContext = new DbContext.DbContext();

        return dbContext.ShoppingLists.FirstOrDefault(list => list.Id == shoppingListId)!;
    }

    public async Task<ShoppingList> AddNew(ShoppingList list)
    {
        if (string.IsNullOrEmpty(list.Name))
            throw new HttpRequestException("No Name for Shoppinglist specified");
        
        await using var dbContext = new DbContext.DbContext();

        list.LastEdited = DateTime.Now;
        dbContext.ShoppingLists.Add(list);

        await dbContext.SaveChangesAsync();
        return list;
    }
}