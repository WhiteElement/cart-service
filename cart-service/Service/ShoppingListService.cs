using System.Net;
using cart_service.Auxillary;
using cart_service.Model;
using Microsoft.EntityFrameworkCore;

namespace cart_service.Service;

public class ShoppingListService
{
    public async Task<List<ShoppingList>> GetAll()
    {
        await using var dbContext = new DbContext.MyDbContext();
        return dbContext.ShoppingLists
            .Include(list => list.Items)
            .ToList();
    }

    public async Task<BraunResultWrapper<ShoppingList>> GetList(int shoppingListId)
    {
        var result = new BraunResultWrapper<ShoppingList>();
        
        await using var dbContext = new DbContext.MyDbContext();
        var queryResult = dbContext.ShoppingLists
            .Include(list => list.Items)
            .FirstOrDefault(list => list.Id == shoppingListId);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (queryResult == null)
            return result.AddErrorAndReturn($"No ShoppingList with Id {shoppingListId} found", HttpStatusCode.NotFound);

        result.Data = queryResult;
        return result;
    }

    public async Task<BraunResultWrapper<ShoppingList>> AddNew(ShoppingList list)
    {
        var result = new BraunResultWrapper<ShoppingList>();

        if (string.IsNullOrEmpty(list.Name))
            return result.AddErrorAndReturn("No name for ShoppingList provided", HttpStatusCode.UnprocessableContent);

        if (list.Id != null)
            return result.AddErrorAndReturn("ShoppingList is not allowed to have an Id", HttpStatusCode.Forbidden);
            
        await using var dbContext = new DbContext.MyDbContext();

        if (dbContext.ShoppingLists.Any(shlist => shlist.Name == list.Name))
            return result.AddErrorAndReturn($"ShoppingList with Name {list.Name} already exists", HttpStatusCode.Forbidden);
        
        list.LastEdited = DateTime.Now;
        dbContext.ShoppingLists.Add(list);

        await dbContext.SaveChangesAsync();
        
        result.Data = list;
        return result;
    }

    public async Task<BraunResultWrapper<ShoppingList>> RenameOne(ShoppingList list)
    {
        var result = new BraunResultWrapper<ShoppingList>();

        if (list.Id == null)
            return result.AddErrorAndReturn("No Id provided for ShoppingList to change the name of", HttpStatusCode.Conflict);

        if (string.IsNullOrEmpty(list.Name))
            return result.AddErrorAndReturn("No Name for Renaming provided", HttpStatusCode.Conflict);
        
        await using var dbContext = new DbContext.MyDbContext();

        var affectedRow = await dbContext.ShoppingLists.Where(i => i.Id == list.Id)
            .ExecuteUpdateAsync(k => k.SetProperty(j => j.Name, list.Name));

        if (affectedRow == 0)
            return result.AddErrorAndReturn($"No ShoppingList with Id {list.Id} found", HttpStatusCode.NotFound);

        result.Data = list;
        return result;
    }

    public async Task<BraunResultWrapper<ShoppingList>> DeleteItem(int? id)
    {
        var result = new BraunResultWrapper<ShoppingList>();

        await using var dbContext = new DbContext.MyDbContext();
        var itemToDelete = await dbContext.ShoppingLists.Where(i => i.Id == id).ExecuteDeleteAsync();
        
        if (itemToDelete == 0)
            return result.AddErrorAndReturn($"No ShoppingList with Id:{id} found", HttpStatusCode.NotFound);

        await dbContext.SaveChangesAsync();
        return result;
    }
}