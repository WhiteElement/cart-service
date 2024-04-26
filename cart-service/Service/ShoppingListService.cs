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

    public async Task<BraunResultWrapper<ShoppingList>> GetList(int shoppingListId)
    {
        var result = new BraunResultWrapper<ShoppingList>();
        
        await using var dbContext = new DbContext.DbContext();

        var queryResult = dbContext.ShoppingLists.FirstOrDefault(list => list.Id == shoppingListId)!;

        if (queryResult == null)
        {
            result.AddError($"No ShoppingList with Id {shoppingListId} found", HttpStatusCode.NotFound);
            return result;
        }

        result.Data = queryResult;
        return result;
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