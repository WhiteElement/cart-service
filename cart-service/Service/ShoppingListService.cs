using System.Net;
using cart_service.Auxillary;
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

    public async Task<BraunResultWrapper<ShoppingList>> AddNew(ShoppingList list)
    {
        var result = new BraunResultWrapper<ShoppingList>();

        if (string.IsNullOrEmpty(list.Name))
        {
            result.AddError("No name for ShoppingList provided", HttpStatusCode.UnprocessableContent);
            return result;
        }
        
        await using var dbContext = new DbContext.DbContext();

        list.LastEdited = DateTime.Now;
        dbContext.ShoppingLists.Add(list);

        await dbContext.SaveChangesAsync();
        result.Data = list;
        return result;
    }
}